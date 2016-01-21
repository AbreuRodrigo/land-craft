using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerExperienceManager : MonoBehaviour{
	public static PlayerExperienceManager Instance;

	public CoreController game;
		
	private Dictionary<LandType, XPByLandType> LandXpByLandType = new Dictionary<LandType, XPByLandType>() {
		{ LandType.Lawn, XPByLandType.LawnXP },
		{ LandType.Bush, XPByLandType.BushXP },
		{ LandType.Trees, XPByLandType.TreesXP },
		{ LandType.ManaTree, XPByLandType.ManaTreeXP }
	};

	void Awake() {
		if(Instance == null) {
			Instance = this;
		}
	}

	void Start() {
		InitializeComponents();
	}

	public void GiveXpToPlayer(LandType type) {
		game.UpdatePlayerXPLocally((long)LandXpByLandType[type]);
	}

	private void InitializeComponents() {
		if(game == null) {
			game = FindObjectOfType<CoreController>();
		}
	}
}