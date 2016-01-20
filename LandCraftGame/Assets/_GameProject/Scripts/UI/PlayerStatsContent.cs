using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatsContent : MonoBehaviour {
	public Text playerName;
	public Text level;
	public Text xp;
	public Text xpnl;

	public void UpdatePlayerStatsContent(PlayerStats playerStats) {
		this.playerName.text = playerStats.playerName;
		this.level.text = "" + playerStats.level;
		this.xp.text = "" + playerStats.xp;
		this.xpnl.text = "" + playerStats.xpnl;
	}

	public void UpdatePlayerStatsContent(string playerName, string level, string xp, string xpnl) {
		this.playerName.text = playerName;
		this.level.text = level;
		this.xp.text = xp;
		this.xpnl.text = xpnl;
	}
}
