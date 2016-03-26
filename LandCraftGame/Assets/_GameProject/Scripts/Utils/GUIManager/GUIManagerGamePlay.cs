using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManagerGamePlay : GUIManagerBasePlay {
	public static GUIManagerGamePlay Instance;

	public GameStateMessage gameStateMessage;

	[Header("Buttons")]
	public Button gamePlayGoalToggleBtn;
	public Button gamePlayStageSelectionBtn;
	public Button playAgainBtn;

	[Header("Images")]
	public Image yourGoalText;
	public Image indicationArrow;

	[Header("Texts")]
	public Text stageTimer;
	//public Text stepManager;
	//public Text currentSteps;
	//public Text maxSteps;

	void Awake() {
		if(Instance == null) {
			Instance = this;
		}
	}

	void Start () {
		base.Start ();	
	}

	protected override void StartMyStateUIEvent(GameState state) {	
		if (state == GameState.GameStageGoal) {
			StartUIEventForGameStageGoalState();	
		}else if(state == GameState.GamePlay) {
			StartUIEventForGamePlayState();
		}
	}

	public void HideToGamePlayButton() {
		if(gamePlayGoalToggleBtn != null) {
			gamePlayGoalToggleBtn.gameObject.GetComponent<Animator>().Play("Hide");
		}
	}
	
	public void ShowToGamePlayButton() {
		if(gamePlayGoalToggleBtn != null) {
			gamePlayGoalToggleBtn.gameObject.GetComponent<Animator>().Play("Show");
		}
	}

	public void UpdateLevelStep(int givenSteps) {
		//if(currentSteps != null) {
		//	currentSteps.text = "" + WorldBehaviour.Instance.GamePlay.CurrentSteps;
		//}
		//if(stepManager != null) {
		//	stepManager.GetComponent<Animator>().Play("Pulse");
		//}
	}

	public void ShowStageClearMessage() {
		if(gameStateMessage != null) {
			gameStateMessage.gameObject.SetActive(true);
			gameStateMessage.myImage.enabled = true;
		}
		
		HideStageTimer();
		HideToGamePlayButton();
		
		if(gameStateMessage != null) {
			gameStateMessage.RunStageClearMessage();
		}
		
		ShowScreenFilter();
	}

	public void ShowGameOverMessage() {
		gameStateMessage.gameObject.SetActive(true);
		gameStateMessage.myImage.enabled = true;
		
		HideStageTimer();
		HideToGamePlayButton();
		HideYourGoalText();
		
		//if(stepManager != null) {
		//	stepManager.GetComponent<Animator>().Play("Hidden");
		//}
		
		if(gameStateMessage != null) {
			gameStateMessage.RunGameOverMessage();
		}
		
		ShowScreenFilter();
	}
			
	public void ShowGamePlayToStageSelectionBtn(float time) {
		StartCoroutine(ScheduleAction(time, gamePlayStageSelectionBtn));
	}
	
	public void ShowPlayAgainBtn(float time) {
		StartCoroutine(ScheduleAction(time, playAgainBtn));
	}

	public void DoToGamePlayButtonPress() {
		if(!WorldBehaviour.Instance.HasLandEventsOnGoing()) {
			if(game.StateManager.IsGameStageGoalState) {
				game.State = GameState.GamePlay;		
			} else if(game.StateManager.IsGamePlayState) {
				game.State = GameState.GameStageGoal;
			}
			
			StartMyStateUIEvent(game.State);
		} else {
			SoundManager.Instance.PlayDenied();
		}
	}

	private void HideStageTimer() {
		if(stageTimer != null) {
			stageTimer.GetComponent<StageTimerBehaviour>().Hide();
		}
	}
	
	private void HideYourGoalText() {
		if(yourGoalText != null) {
			yourGoalText.GetComponent<Animator>().Play("UITextHideDown");
		}
	}

	void StartUIEventForGameStageGoalState() {
		if(!game.StageIsOnGoing && stageTimer != null) {
			stageTimer.GetComponent<Animator>().Play("StageTimerHideUp");
		}
		
		if(yourGoalText != null) {
			yourGoalText.GetComponent<Animator> ().Play ("UITextShowUp");
		}
		
		//if(stepManager != null) {
		//	stepManager.GetComponent<Animator> ().Play ("Hidden");
		//}
		
		game.GameCamera.VisualizeOtherView();
		game.ChangeToOtherGameView();
		
		SoundManager.Instance.PlayAttraction();
	}
	
	void StartUIEventForGamePlayState() {
		//if(maxSteps != null) {
		//	maxSteps.text = "/" + WorldBehaviour.Instance.GamePlay.MaxSteps;
		//}
		//if(currentSteps != null) {
		//	currentSteps.text = "" + WorldBehaviour.Instance.GamePlay.CurrentSteps;
		//}
		
		if(!game.StageIsOnGoing) {
			game.StageIsOnGoing = true;
			
			if(indicationArrow != null) {
				indicationArrow.gameObject.SetActive(false);
			}
			
			if(stageTimer != null) {
				stageTimer.GetComponent<StageTimerBehaviour>().Show();
			}
		}
		
		if(yourGoalText != null) {
			yourGoalText.GetComponent<Animator>().Play("UITextHideDown");
		}
		
		//if(stepManager != null) {
		//	stepManager.GetComponent<Animator>().Play("Visible");
		//}
		
		game.GameCamera.VisualizeMyView();
		game.ChangeToMyGameView();
		
		SoundManager.Instance.PlayAttraction();
	}

	IEnumerator WaitAndRunAction() {
		yield return new WaitForSeconds(3);
		
		gamePlayStageSelectionBtn.gameObject.SetActive(true);
		gamePlayStageSelectionBtn.GetComponent<Animator>().Play("Show");
	}
}