using Parse;
using UnityEngine;
using System.Collections;

public class PlayerStats : ParseModel {
	public string playerName = "PlayerName";
	public int currentStage = 1;
	public int crystals = 0;
	public int level = 1;
	public long xp = 0;
	public long xpnl = 1000;
	public string sharedOnFacebook = "";//DateFormat;
	public string sharedOnTwitter = "";//DateFormat;
	public string sharedOnGooglePlus = "";//DateFormat;
	public int inventoryCharge = 0;
	public int inventorySlots = 8;
	public int inventoryCapacity = 10;
	public long score = 0;
	public int totalStars = 0;

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