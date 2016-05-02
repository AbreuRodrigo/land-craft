using DataLab;
using UnityEngine;
using System.Collections.Generic;

public class PlayerStatsManager : ServerObjectManager {
	private PlayerStats playerStats;

	public bool HasLoaded { get; set; }

	private bool hasTriedLoading;

	public PlayerStats PlayerStats {
		get{ return playerStats; }
		set{ this.playerStats = value; }
	}

	void Start() {
		TryLoadingData ();
	}

	protected override void LoadResponse(DataLabObject dataObject) {
		if (dataObject != null) {
			if(dataObject.HasErrorMessage()) {
				HasLoaded = false;
				return;
			}
			playerStats.ConvertObjectToServerObject (dataObject);
			HasLoaded = true;
		} else {
			playerStats.Save ();
		}
	}

	private void TryLoadingData() {
		if (!HasLoaded) {
			playerStats = new PlayerStats ();
			LoadStats(playerStats);
		}
	}

	protected override void LoadResponseList(List<DataLabObject> dataObjectList) { }
}