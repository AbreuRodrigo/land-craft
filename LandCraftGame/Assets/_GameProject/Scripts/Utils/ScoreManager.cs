using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	private const string HIGHSCORE_LABEL = "Highscore";
	private const long BASE_HIGHSCORE = 500L;

	public ScoreBehaviour score;
	public ScoreBehaviour highscore;

	public Transform morePointFxParent;
	public GameObject morePointsPrefab;

	private int scoreByLandType;
	private long tempScoreValue = 0L;

	public GameFreeModeController game;

	void Start() {
		if(game == null) {
			game = FindObjectOfType<GameFreeModeController>();
		}

		highscore.AddPoints(RetrieveHighscore());
	}

	public void InstantiateMorePoints(LandBehaviour land) {
		scoreByLandType = ScoreByLand(land);

		if(morePointsPrefab != null && morePointFxParent != null) {
			GameObject morePointsObj = 
				(GameObject)Instantiate(morePointsPrefab, land.transform.position, morePointsPrefab.transform.rotation);
			
			if(morePointsObj != null) {
				MorePointsFX morePointsFX = morePointsObj.GetComponent<MorePointsFX>();
				
				if(morePointsFX != null) {
					morePointsFX.transform.parent = morePointFxParent;
					morePointsFX.Value = scoreByLandType;
				}
				
				if(score != null) {
					score.AddPoints(scoreByLandType);
					ValidateSaveNewHighScore();
				}
			}
		}
	}

	private int ScoreByLand(LandBehaviour land) {
		int value = 0;

		switch(land.type) {
			case LandType.Lawn:
				value = (int)ScoreByLandType.LawnScore;
			break;
			case LandType.Bush:
				value = (int)ScoreByLandType.BushScore;
			break;
			case LandType.Trees:
				value = (int)ScoreByLandType.TreesScore;
			break;
			case LandType.ManaTree:
				value = (int)ScoreByLandType.LifeTreeScore;
			break;
		}

		return value;
	}

	private void ValidateSaveNewHighScore() {
		if(score.LongValue > highscore.LongValue) {
			highscore.OverwriteValue(score.LongValue);
			game.UpdatePlayerScoreOnServer(highscore.LongValue);
		}
	}
	
	private long RetrieveHighscore() {
		if (game != null && game.PlayerStatsManager != null && game.PlayerStatsManager.PlayerStats != null) {
			tempScoreValue = game.PlayerStatsManager.PlayerStats.score;
		} 

		if(tempScoreValue < BASE_HIGHSCORE) {
			tempScoreValue = BASE_HIGHSCORE;
		}

		return tempScoreValue;
	}
}