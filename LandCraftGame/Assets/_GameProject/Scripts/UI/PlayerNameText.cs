using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class PlayerNameText : MonoBehaviour {
	public InputField myInputField;
	public Text levelText;

	public GameLobbyController game;

	public void OnEndEdit(string text) {
		text = myInputField.text;

		if(game != null && text != null) {
			if(text != "") {
				game.UpdatePlayerNameOnServer(text);
			}else {
				myInputField.text = game.PlayerStatsManager.PlayerStats.PlayerName;
			}
		}
	}

	public void UpdatePlayerName(string text) {
		myInputField.text = text;
	}

	public void UpdateLevelText(string level) {
		levelText.text = level;
	}
}