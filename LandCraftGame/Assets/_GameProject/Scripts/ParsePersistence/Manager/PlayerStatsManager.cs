using Parse;
using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PlayerStatsManager : ParseManager {
	private PlayerStats playerStats;

	public PlayerStats PlayerStats {
		get{ return playerStats; }
		set{ this.playerStats = value; }
	}

	void Start() {
		playerStats = new PlayerStats();
	}

	public void ListPlayerStatsOrderByScore() {
		ParseQuery<ParseObject> query = ParseObject.GetQuery(playerStats.ClassName);
		query = query.WhereGreaterThan("score", 0);
		query = query.OrderByDescending("score");
		query.FindAsync().ContinueWith(TaskToRun);
	}

	private void TaskToRun(Task<IEnumerable<ParseObject>> t) {
		IEnumerable<ParseObject> relatedObjects = t.Result;
		
		foreach(ParseObject po in relatedObjects) {
			PlayerStatsView psv = new PlayerStatsView();
			psv.clientToken = po.Get<string>("clientToken");
			psv.playerName = po.Get<string>("playerName");
			psv.score = po.Get<long>("score");

			psv.Show();
		}
	}
}