using UnityEngine;

public class PlayerStatsView {
	public string clientToken;
	public string playerName;
	public long score;

	public void Show() {
		Debug.Log(playerName + " - " + score);
	}
}
