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

	public void ChangeToGamePlayState() {
		ChangeState(GameState.GamePlay);
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

	public bool IsGamePlayState {
		get {
			return IsThisState(GameState.GamePlay);
		}
	}
}