using UnityEngine;
using UnityEngine.Advertisements;
using System;
using System.Collections.Generic;

public class GameLobbyController : CoreController {
	private DateTime now;
	private DateTime parsed;

	void Awake() {
		base.Awake();
	}

	void Start() {
		if(PlayerStatsManager != null) {
			RetrieveCrystalsFromServer();
			ValidateSharedOnFacebook();
			ValidateSharedOnTwitter();
			ValidateSharedOnGooglePlus();
			ValidatePlayerName();
			ValidatePlayerLevel();

			PlayerStatsManager.PlayerStats.Online = true;
			PlayerStatsManager.PlayerStats.Save();
		}
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			ConfirmManager.Instance.HandleConfirmExitModal();
		}
	}

	protected override void InitializeComponents() { }

	private void RetrieveCrystalsFromServer() {
		if(PlayerStatsManager != null) {
			GUIManagerGameLobby.Instance.SetCrystals(PlayerStatsManager.PlayerStats.Crystals);
		}
	}

	private void ValidateSharedOnFacebook() {
		string result = PlayerStatsManager.PlayerStats.sharedOnFacebook;

		if (result == null || result == "" || !HasSharedToday(result)) {
			GUIManagerGameLobby.Instance.ShowFacebookHalo();
		} else {
			GUIManagerGameLobby.Instance.HideFacebookHalo();
		}
	}

	private void ValidateSharedOnTwitter() {
		string result = PlayerStatsManager.PlayerStats.sharedOnTwitter;
		
		if (result == null || result == "" || !HasSharedToday(result)) {
			GUIManagerGameLobby.Instance.ShowTwittweHalo();
		} else {
			GUIManagerGameLobby.Instance.HideTwitterHalo();
		}
	}

	private void ValidateSharedOnGooglePlus() {
		string result = PlayerStatsManager.PlayerStats.sharedOnGooglePlus;
		
		if (result == null || result == "" || !HasSharedToday(result)) {
			GUIManagerGameLobby.Instance.ShowGooglePlusHalo();
		} else {
			GUIManagerGameLobby.Instance.HideGooglePlusHalo();
		}
	}

	private bool HasSharedToday(string stringDateTime) {
		now = DateTime.Now;
		parsed = DateTime.Parse(stringDateTime);

		return new DateTime(parsed.Year, parsed.Month, parsed.Day).CompareTo(new DateTime(now.Year, now.Month, now.Day)) >= 0;
	}

	private void ValidatePlayerName() {
		string result = PlayerStatsManager.PlayerStats.PlayerName;

		if (result != null && result != "") {
			GUIManagerGameLobby.Instance.UpdatePlayerNameText(result);
		}
	}

	private void ValidatePlayerLevel() {
		int result = PlayerStatsManager.PlayerStats.Level;
		
		if (result > 0) {
			GUIManagerGameLobby.Instance.UpdatePlayerLevel("" + result);
		}
	}
}