using DataLab;
using UnityEngine;
using System.Collections.Generic;

public class PlayerStatsManager : ServerObjectManager {
	private PlayerStats playerStats;

	public PlayerStats PlayerStats {
		get{ return playerStats; }
		set{ this.playerStats = value; }
	}

	void Start() {
		playerStats = new PlayerStats();
		LoadStats(playerStats);
	}

	protected override void LoadStatsResponse(DataLabObject dataObject) {
		if (dataObject != null) {
			playerStats.ConvertObjectToServerObject (dataObject);
		} else {
			playerStats.Save();
		}
	}
}