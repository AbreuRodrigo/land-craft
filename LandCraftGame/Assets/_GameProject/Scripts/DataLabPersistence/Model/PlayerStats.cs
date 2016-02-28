using UnityEngine;
using System.Collections;

public class PlayerStats : ServerObject {
	private const int XPBASE = 500;
	private const int DEVIATION = 100;

	public string playerName = "PlayerName";
	public int currentStage = 1;
	public int crystals = 0;
	public int level = 1;
	public long xp = 0;
	public long xpnl = XPBASE;
	public string sharedOnFacebook = "";//DateFormat;
	public string sharedOnTwitter = "";//DateFormat;
	public string sharedOnGooglePlus = "";//DateFormat;
	public int inventoryCharge = 0;
	public int inventorySlots = 8;
	public int inventoryCapacity = 10;
	public long score = 0;
	public int totalStars = 0;

	public string PlayerName {
		get { return playerName; }
		set { playerName = value; }
	}

	public int CurrentStage {
		get { return currentStage; }
		set { currentStage = value; }
	}

	public int Crystals {
		get { return crystals; }
		set { crystals = value; }
	}

	public int Level {
		get { return level; }
		set { level = value; }
	}

	public long XP {
		get { return xp; }
		set { HandlePlayerEvolution(value); }
	}

	public long XPNL {
		get { return (long)((Level * XPBASE) + ((Level - 1) * DEVIATION)); }
	}

	private void HandlePlayerEvolution(long moreXp) {
		xp = moreXp;

		if(xp >= XPNL) {
			Level++;
			xpnl = XPNL;
			EventManager.Instance.ShowLevelUpEvent();
		}
	}

	protected override void OnBegin() {
	}

	protected override void OnLoaded() {
		this.playerName = dataObject.GetString("playerName");
		this.currentStage = dataObject.GetInt("currentStage");
		this.crystals = dataObject.GetInt("crystals");
		this.level = dataObject.GetInt("level");
		this.xp = dataObject.GetLong("xp");
		this.xpnl = dataObject.GetLong("xpnl");
		this.sharedOnFacebook = dataObject.GetString("sharedOnFacebook");
		this.sharedOnTwitter = dataObject.GetString("sharedOnTwitter");
		this.sharedOnGooglePlus = dataObject.GetString("sharedOnGooglePlus");
		this.inventoryCharge = dataObject.GetInt("inventoryCharge");
		this.inventorySlots = dataObject.GetInt("inventorySlots");
		this.inventoryCapacity = dataObject.GetInt("inventoryCapacity");
		this.score = dataObject.GetLong("score");
		this.totalStars = dataObject.GetInt("totalStars");
	}
}