using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using Parse;

public class ParseTest : MonoBehaviour {
	//Labels
	private const string PLAYER_NAME_LABEL = "playerName";
	private const string CLIENT_TOKEN_LABEL = "clientToken";
	private const string CRYSTALS_LABEL = "crystals";
	private const string LEVEL_LABEL = "level";
	private const string XP_LABEL = "xp";
	private const string XPNL_LABEL = "xpnl";
	private const string CURRENT_STAGE_LABEL = "currentStage";

	private const string PLAYER_NAME = "RodrigoAbreu";

	private ParseObject playerStats;
	private string clientToken;

	void Start() {
		clientToken = SystemInfo.deviceUniqueIdentifier;

		//LoadScore();
		//playerStats.DeleteAsync();

		PlayerStats ps = new PlayerStats();
		//StageStats ss = new StageStats();

		//UpdateScore(score);
		//LoadStats();

		//SaveStats();
	}

	void SaveStats() {
		playerStats = new ParseObject("PlayerStats");
		playerStats[CLIENT_TOKEN_LABEL] = clientToken;
		playerStats[PLAYER_NAME_LABEL] = PLAYER_NAME;
		playerStats[CRYSTALS_LABEL] = 10;
		playerStats[LEVEL_LABEL] = 1;
		playerStats[XP_LABEL] = 0;
		playerStats[XPNL_LABEL] = 1000;
		playerStats[CURRENT_STAGE_LABEL] = 1;

		playerStats.SaveAsync();
	}

	void UpdateStats(ParseObject score) {
		score.SaveAsync().ContinueWith (t => {
			score["cheatMode"] = true;
			score["score"] = 400;
			score.SaveAsync();
		});
	}

	void LoadStats() {
		ParseQuery<ParseObject> query = ParseObject.GetQuery("PlayerStats");
		query.WhereEqualTo(CLIENT_TOKEN_LABEL, clientToken);
		query.FirstAsync().ContinueWith(t => {
			ParseObject obj = t.Result;
			Debug.Log(obj[PLAYER_NAME_LABEL]);
		});
	}
}