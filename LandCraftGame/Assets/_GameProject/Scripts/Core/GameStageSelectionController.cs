using UnityEngine;
using System.Collections;

public class GameStageSelectionController : CoreController {
	public StageIcon[] stages;

	public void Awake() {
		base.Awake();
	}

	void Start() {
		LoadStagesStates();
	}
   
	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			LoadGameLobby();
		}
	}

	private void LoadStagesStates() {
		StageIcon stageIcon = null;

		if(stages != null && ProgressManager != null && ProgressManager.Data != null && 
		   ProgressManager.Data.stages != null && ProgressManager.Data.stages.Length > 0) {
			for(int i = 0; i < stages.Length; i++) {
				stageIcon = stages[i];
				stageIcon.SetMyClickAction(LoadClickedLevel);
				stageIcon.StageSetup(ProgressManager.Data.stages[i]);
			}
		}
	}

	private void LoadClickedLevel(int index) {
		//GUIManagerStageSelection.Instance.DoStageSelectionPress(index);
		currentStage = index;
	}

	protected override void InitializeComponents() { }
}