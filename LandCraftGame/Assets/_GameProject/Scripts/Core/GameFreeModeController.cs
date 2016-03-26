using UnityEngine;
using System.Collections;

public class GameFreeModeController : CoreController {
	public ScoreManager scoreManager;
	public ResourceFXManager resourceFxManager;

	void Awake() {
		base.Awake();
	}
	
	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			LoadGameLobby();
		}
	}

	void OnDisable() {
		base.OnDisable();
	}

	protected override void InitializeComponents() { }

	/*public void TestStageGameOver() {
		if (!WorldBehaviour.Instance.StillHasEmptyCells()) {
			StartGameOverProcess();
		}
	}*/

	private void SaveProgress() {
		ProgressManager.SaveData();
	}

	public void LandUpgradeLogicsFreeMode(LandBehaviour land) {
		if(scoreManager != null && StateManager != null && StateManager.IsGameFreeMode) {
			if(!LandType.Waste.Equals(land.type)) {
				scoreManager.InstantiateMorePoints(land);
			}
		}
	}

	public void HarvestTargetLand(Vector3 origin, LandType landType) {
		if(resourceFxManager != null && origin != null) {
			resourceFxManager.InstantiateMoreResource(origin, landType);
		}
	}

	/*public void StartGameOverProcess() {
		StateManager.ChangeToGameOverState();
		
		GameCamera.TurnOnBlackAndWhite();
		
		GUIManagerGameFreeMode.Instance.ShowGameOverMessage();
		
		WorldBehaviour.Instance.FoldMyView();
		WorldBehaviour.Instance.FoldOtherView();
		
		InputManager.Instance.Selector.Hide();
		
		GUIManagerGameFreeMode.Instance.ShowGamePlayToStageSelectionBtn(1f);
		GUIManagerGameFreeMode.Instance.ShowPlayAgainBtn(1.5f);
	}*/
}