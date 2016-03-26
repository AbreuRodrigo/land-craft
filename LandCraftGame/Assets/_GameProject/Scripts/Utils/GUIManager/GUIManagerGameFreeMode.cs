using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManagerGameFreeMode : GUIManagerBasePlay {
	public static GUIManagerGameFreeMode Instance;

	public GameStateMessage gameStateMessage;

	[Header("Buttons")]
	public Button gameLobbyBtn;
	public Button playAgainBtn;

	void Awake() {
		if(Instance == null) {
			Instance = this;	
		}
	}

	void Start() {
		base.Start();	
	}

	protected override void StartMyStateUIEvent(GameState state) {	
		game.GameCamera.VisualizeMyView();
		game.ChangeToMyGameView();
	}

	public void ShowGameOverMessage() {
		gameStateMessage.gameObject.SetActive(true);
		gameStateMessage.myImage.enabled = true;
				
		if(gameStateMessage != null) {
			gameStateMessage.RunGameOverMessage();
		}
		
		ShowScreenFilter();
	}

	public void ShowGamePlayToStageSelectionBtn(float time) {
		StartCoroutine(ScheduleAction(time, gameLobbyBtn));
	}

	public void ShowPlayAgainBtn(float time) {
		StartCoroutine(ScheduleAction(time, playAgainBtn));
	}

	public void ShowResourceCounterByType(LandType type) {
		if(type.Equals(LandType.ManaTree)) {
			//ShowManaResourceCounter();
		}
		if(type.Equals(LandType.Trees)) {
			//ShowWoodResourceCounter();
		}
	}
}