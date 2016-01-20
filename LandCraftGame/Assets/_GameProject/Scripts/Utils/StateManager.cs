using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateManager {
	private static StateManager instance;
	public static StateManager Instance {
		get {
			if(instance == null) {
				instance = new StateManager();
			}
			return instance;
		} 
	}

	public GameState state;

	private StateManager() {}

	public void ChangeState(GameState newState) {
		state = newState;
	}
	
	public void ChangeToInitializingState() {
		ChangeState(GameState.Initializing);
	}
	
	public void ChangeToGameMenuState() {
		ChangeState(GameState.GameMenu);
	}
	
	public void ChangeToStageSelectionState() {
		ChangeState(GameState.StageSelection);
	}
	
	public void ChangeToGamePlayState() {
		ChangeState(GameState.GamePlay);
	}

	public void ChangeToGameFreeMode() {
		ChangeState(GameState.GameFreeMode);
	}
	
	public void ChangeToPauseState() {
		ChangeState(GameState.Pause);
	}
	
	public void ChangeToGameOverState() {
		ChangeState(GameState.GameOver);
	}

	public void ChangeToStageClearedState() {
		ChangeState(GameState.StageCleared);
	}
		
	public bool IsThisState(GameState s) {
		return state == s;
	}

	public bool IsInitializingState {
		get { 
			return IsThisState(GameState.Initializing);
		}
	}

	public bool IsGameMenuState {
		get {
			return IsThisState(GameState.GameMenu);
		}
	}

	public bool IsStageSelectionState {
		get {
			return IsThisState(GameState.StageSelection);
		}
	}
	
	public bool IsGamePlayState {
		get {
			return IsThisState(GameState.GamePlay);
		}
	}

	public bool IsGameFreeMode {
		get {
			return IsThisState(GameState.GameFreeMode);
		}
	}

	public bool IsPauseState {
		get {
			return IsThisState(GameState.Pause);
		}
	}

	public bool IsGameStageGoalState {
		get {
			return IsThisState(GameState.GameStageGoal);
		}
	}
	
	public bool IsGameOverState {
		get {
			return IsThisState(GameState.GameOver);
		}
	}

	public bool IsStageClearedState {
		get {
			return IsThisState(GameState.StageCleared);
		}
	}
}