using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamePlayController : CoreController {
	public GameObject starsExplosion;
	public GameObject brightExplosion;

	public StageClearFXBehaviour stageClearFX;

	private int maxSteps;
	public int MaxSteps {
		get { return maxSteps; }
	}

	private int givenSteps;
	public int CurrentSteps {
		get { return givenSteps; }
	}

	void Awake() {
		base.Awake();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			LoadGameLobby();
		}
	}

	protected override void InitializeComponents() { }

	public void DefineLevelSteps(int steps) {
		this.maxSteps = steps;
		this.givenSteps = 0;
	}

	public void StageStapForward() {
		givenSteps++;
		GUIManagerGamePlay.Instance.UpdateLevelStep(givenSteps);
	}

	public int CurrentStageTimer() {
		return (WorldBehaviour.Instance.GridSize * 5);
	}

	public void TestStageGoalCriterias(GridBehaviour myGrid, GridBehaviour goalGrid) {
		if (goalGrid != null && myGrid != null && goalGrid.cells != null && myGrid.cells != null) {
			for (int i = 0; i < goalGrid.cells.Length; i++) {
				if (goalGrid.cells[i].Value != myGrid.cells[i].Value) {
					TestStageGameOver();
					return;
				}
			}

			StartStageClearedProcess();
		}
	}

	public void TestStageGameOver() {
		if (!StateManager.IsStageClearedState && 
		    (givenSteps > maxSteps || !WorldBehaviour.Instance.StillHasEmptyCells())) {
			StartGameOverProcess();
		}
	}

	private void StartStageClearedProcess() {
		StateManager.ChangeToStageClearedState();

		GUIManagerGamePlay.Instance.ShowStageClearMessage();

		WorldBehaviour.Instance.FoldMyView();
		WorldBehaviour.Instance.FoldOtherView();

		InputManager.Instance.Selector.Hide();

		StarsParticleExplosion();

		if (stageClearFX != null) {
			stageClearFX.gameObject.SetActive(true);
			stageClearFX.Run();

			if(ProgressManager != null && ProgressManager.Data != null) {
				Stage currentStage = stageClearFX.Stage;

				OpenNextStage(currentStage.index + 1);

				ProgressManager.AddStageToData(currentStage.index, currentStage);
				ProgressManager.AdvanceToNextStage();

				SaveProgress();
			}
		}

		GUIManagerGamePlay.Instance.ShowGamePlayToStageSelectionBtn(1f);
		GUIManagerGamePlay.Instance.ShowPlayAgainBtn(1.5f);
	}

	private void OpenNextStage(int index) {
		if(ProgressManager.IsStageLocked(index)) {
			Stage nextStage = new Stage();
			nextStage.index = index;
			nextStage.isOpen = true;
			ProgressManager.AddStageToData(nextStage.index, nextStage);
		}
	}

	private void SaveProgress() {
		ProgressManager.SaveData();
	}
	
	public void StartGameOverProcess() {
		StateManager.ChangeToGameOverState();

		GameCamera.TurnOnBlackAndWhite();

		GUIManagerGamePlay.Instance.ShowGameOverMessage();

		WorldBehaviour.Instance.FoldMyView();
		WorldBehaviour.Instance.FoldOtherView();

		InputManager.Instance.Selector.Hide();

		GUIManagerGamePlay.Instance.ShowGamePlayToStageSelectionBtn(1f);
		GUIManagerGamePlay.Instance.ShowPlayAgainBtn(1.5f);
	}

	private void StarsParticleExplosion() {
		if (starsExplosion != null) {
			StartCoroutine(StartParticleWithDelay(starsExplosion, 1f));
		}
	}

	private void BrightParticleExplosion() {
		if (brightExplosion != null) {
			StartCoroutine(StartParticleWithDelay(brightExplosion, 1f));
		}
	}

	private void InstantiateParticleExplosion(GameObject particlePrefab) {
		if (particlePrefab != null) {
			GameObject particle = 
				(GameObject) GameObject.Instantiate(
					particlePrefab, 
					WorldBehaviour.Instance.MyView.transform.position, 
					particlePrefab.transform.rotation
				);
		}
	}

	IEnumerator StartParticleWithDelay(GameObject particleExplosion, float delay) {
		yield return new WaitForSeconds(delay);

		InstantiateParticleExplosion(particleExplosion);
	}
}