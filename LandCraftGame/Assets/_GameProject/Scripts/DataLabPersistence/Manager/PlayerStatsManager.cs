using DataLab;
using UnityEngine;
using System.Collections.Generic;

public class PlayerStatsManager : ServerObjectManager {
	private PlayerStats playerStats;

	public bool HasLoaded { get; set; }

	public PlayerStats PlayerStats {
		get{ return playerStats; }
		set{ this.playerStats = value; }
	}

	void Start() {
		if (!HasLoaded) {
			playerStats = new PlayerStats ();
			LoadStats(playerStats);
		}
	}

	protected override void LoadResponse(DataLabObject dataObject) {
		if (dataObject != null) {
			playerStats.ConvertObjectToServerObject(dataObject);
			playerStats.Online = true;
		}

		playerStats.Save();

		HasLoaded = true;
	}

	protected override void LoadResponseList(List<DataLabObject> dataObjectList) { }
}