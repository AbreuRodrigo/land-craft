using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageTimerBehaviour : MonoBehaviour {
	private GamePlayController game;

	public Animator myAnimator;

	public Text stageTimer;

	private DateTime startTime;
	private TimeSpan counter;
	private bool isTimerActive;
	private bool justStartedTimer;

	public int RemainingTimeInSeconds {
		get {
			return counter.Seconds;
		}
	}

	void Start() {
		if(game == null) {
			game = (GamePlayController)GameObject.FindObjectOfType<CoreController>();
		}
		if(myAnimator == null) {
			myAnimator = GameObject.FindObjectOfType<Animator>();
		}
	}

	void Update() {
		if (isTimerActive) {
			if (!justStartedTimer) {
				justStartedTimer = true;
				startTime = DateTime.Now;
			}

			UpdateStageTimer();
		} else {
			SetupStageTimerText();
		}
	}

	public void ActivateTimer() {
		if(game.StateManager.IsGamePlayState) {
			isTimerActive = true;
		}
	}

	public void DeactivateTimer() {
		isTimerActive = false;
	}
		
	private void UpdateStageTimer() {
		counter = TimeManager.TimeSpanFromNowToStart(startTime, game.CurrentStageTimer());

		if ((counter.Minutes + counter.Seconds) >= 0) {
			stageTimer.text = TimeManager.TimeSpanAsString(counter);
		} else if(game.StateManager.IsThisState(GameState.GamePlay)) {
			DeactivateTimer();

			game.StartGameOverProcess();
		}
	}

	private void SetupStageTimerText() {
		counter = TimeManager.TimeSpanFromNowToStart(DateTime.Now, game.CurrentStageTimer());
		stageTimer.text = TimeManager.TimeSpanAsString(counter);
	}

	public void Show() {
		myAnimator.Play("StageTimerShowDown");
	}

	public void Hide() {
		myAnimator.Play("StageTimerHideUp");
	}
}