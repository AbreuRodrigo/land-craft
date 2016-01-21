using Parse;
using UnityEngine;
using System.Collections;

public class PlayerStats : ParseModel {
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
		set { 
			level = value;
		}
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
		this.playerName = parseObject.Get<string>("playerName");
		this.currentStage = parseObject.Get<int>("currentStage");
		this.crystals = parseObject.Get<int>("crystals");
		this.level = parseObject.Get<int>("level");
		this.xp = parseObject.Get<int>("xp");
		this.xpnl = parseObject.Get<int>("xpnl");
		this.sharedOnFacebook = parseObject.Get<string>("sharedOnFacebook");
		this.sharedOnTwitter = parseObject.Get<string>("sharedOnTwitter");
		this.sharedOnGooglePlus = parseObject.Get<string>("sharedOnGooglePlus");
		this.inventoryCharge = parseObject.Get<int>("inventoryCharge");
		this.inventorySlots = parseObject.Get<int>("inventorySlots");
		this.inventoryCapacity = parseObject.Get<int>("inventoryCapacity");
		this.score = parseObject.Get<long>("score");
		this.totalStars = parseObject.Get<int>("totalStars");
	}
}