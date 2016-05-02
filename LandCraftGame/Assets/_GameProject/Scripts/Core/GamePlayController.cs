using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GamePlayController : CoreController {
	public ScoreManager scoreManager;
	public ResourceFXManager resourceFxManager;

	void Awake() {
		base.Awake();
	}

	void Start() {
		ConvertGridSetupIntoLandView();
	}
	
	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			LoadGameLobby();
		}
	}

	public void LandUpgradeLogicsGamePlay(LandBehaviour land) {
		if(scoreManager != null && StateManager != null && StateManager.IsGamePlayState) {
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

	protected override void InitializeComponents() { }


	private void ConvertGridSetupIntoLandView() {
		int landValue = 0;

		if(PlayerGridSetupStats != null) {
			for(int i = 0; i < PlayerGridSetupStats.Length; i++) {
				landValue = PlayerGridSetupStats[i];

				if(landValue > 0) {
					CellBehaviour cell = WorldBehaviour.Instance.MyGridCells[i];
					WorldBehaviour.Instance.LandInstantiator.InstantiateLand((LandType)landValue, cell.transform.position, cell, false, null);
					cell.Deactivate();
				}
			}
		}
	}
}