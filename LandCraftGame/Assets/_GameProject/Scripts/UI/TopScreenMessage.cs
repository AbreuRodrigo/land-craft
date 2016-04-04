using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TopScreenMessage : MonoBehaviour {
	private const string TAKING_LONGER_MSG = "It's taking longer to communicate with server...";
	private const string COULD_NOT_CONNECT_MSG = "Could not connect to server, please try again later.";
	private const string RETRYING_THE_CONNECTION_MSG = "Retrying the connection...";

	public Animator myAnimator;
	public Text myText;

	public void PlayTakingLongerMessage() {
		PlayMessage(TAKING_LONGER_MSG);
	}

	public void PlayCouldNotConnectMessage() {
		PlayMessage(COULD_NOT_CONNECT_MSG);
	}

	public void PlayRetryingTheConnectionMessage() {
		PlayMessage(RETRYING_THE_CONNECTION_MSG);
	}

	private void PlayMessage(string msg) {
		myText.enabled = true;
		myText.text = msg;

		myAnimator.Play("PlayMessage");
	}
}
