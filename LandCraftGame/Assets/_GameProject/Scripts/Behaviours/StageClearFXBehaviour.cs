using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageClearFXBehaviour : MonoBehaviour {

	public GameObject star1Img;
	public GameObject star2Img;
	public GameObject star3Img;
	public GameObject stageCounterTxt;
	public GameObject stageClearStarContainer;
	public GameObject totalTimerTxt;
	public GameObject stageClearSunburtsFX;

	private GamePlayController game;
	private StageTimerBehaviour stageTimer;

	private Stage stageResult;
	public Stage Stage {
		get { return stageResult; }
	}

	public void Run() {
		game = GameObject.FindObjectOfType<GamePlayController>();
		stageTimer = GameObject.FindObjectOfType<StageTimerBehaviour>();

		int currentStage = PlayerPrefsManager.ClickedStage;
		int totalStageTime = game.CurrentStageTimer();
		int totalSpentTime = totalStageTime - stageTimer.RemainingTimeInSeconds;

		float percentTimeSpent = ((float)totalSpentTime / (float)totalStageTime) *  100.0f;
		int starsCounter;

		SetStageCounter((int)currentStage);

		ShowStageClearContainer();

		StarSunburstFX();

		if(percentTimeSpent <= 30) {
			starsCounter = 3;
			ClearStageWithThreeStars();
		}else if(percentTimeSpent <= 60) {
			starsCounter = 2;
			ClearStageWithTwoStars();
		}else {
			starsCounter = 1;
			ClearStageWithOneStar();
		}

		SetTotalSpentTime((int)totalSpentTime);

		stageResult = new Stage();
		stageResult.index = currentStage - 1;
		stageResult.id = game.HashManager.IntToHash(stageResult.index);
		stageResult.isClear = true;
		stageResult.isLocked = false;
		stageResult.isOpen = false;
		stageResult.stars = starsCounter;
		stageResult.steps = game.CurrentSteps;
		stageResult.time = totalSpentTime;
	}

	private void ShowStageClearContainer() {
		if (stageClearStarContainer != null) {
			stageClearStarContainer.gameObject.SetActive(true);
			stageClearStarContainer.GetComponent<Animator>().Play("Show");
		}
	}

	private void ClearStageWithOneStar() {
		StartCoroutine(StageClearStarAnimation(1f, star1Img));
	}

	private void ClearStageWithTwoStars() {
		StartCoroutine(StageClearStarAnimation(1f, star1Img));
		StartCoroutine(StageClearStarAnimation(1.5f, star2Img));
	}

	private void ClearStageWithThreeStars() {
		StartCoroutine(StageClearStarAnimation(1f, star1Img));
		StartCoroutine(StageClearStarAnimation(1.5f, star2Img));
		StartCoroutine(StageClearStarAnimation(2f, star3Img));
	}

	private void StarSunburstFX() {
		if (stageClearSunburtsFX != null) {
			stageClearSunburtsFX.gameObject.SetActive(true);
			stageClearSunburtsFX.GetComponent<Animator>().Play("Show");
		}
	}

	private void SetStageCounter(int CurrentStage) {
		if(game != null) {
			stageCounterTxt.gameObject.SetActive(true);
			stageCounterTxt.GetComponent<Text>().text = "" + CurrentStage;
			stageCounterTxt.GetComponent<Animator>().Play("Show");
		}
	}

	private void SetTotalSpentTime(int TotalSpentTime) {
		if(stageTimer != null) {
			totalTimerTxt.SetActive(true);
			totalTimerTxt.GetComponent<Text>().text = "00:" + (TotalSpentTime < 10 ? ("0" + TotalSpentTime) : "" + TotalSpentTime);
			totalTimerTxt.GetComponent<Animator>().Play("Show");
		}
	}

	IEnumerator StageClearStarAnimation(float delay, GameObject star) {
		yield return new WaitForSeconds(delay);

		if (star != null) {
			star.SetActive(true);
			star.GetComponent<Animator>().Play("Show");
		}

		StopCoroutine("StageClearStarAnimation");
	}
}