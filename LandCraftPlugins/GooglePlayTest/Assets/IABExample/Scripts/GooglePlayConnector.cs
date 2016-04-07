using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.SocialPlatforms;

public class GooglePlayConnector : MonoBehaviour {

	public Text connectionStatus;

	void Start() {
		//ConnectToGoogle();
	}

	public void ConnectToGoogle() {
		GooglePlayGames.PlayGamesPlatform.DebugLogEnabled = true;

		GooglePlayGames.PlayGamesPlatform.Activate();

		Social.localUser.Authenticate((bool success) => {
			if(connectionStatus != null) {
				if(success) {
					connectionStatus.color = Color.green;
					connectionStatus.text = "Status: Connected!";
				}else {
					connectionStatus.color = Color.red;
					connectionStatus.text = "Status: Disconnected!";
				}
			}
		});
	}
}