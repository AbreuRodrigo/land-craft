using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManagerGameLoading : GUIManagerBase {
	public static GUIManagerGameLoading Instance;

	[Header("Texts")]
	public Text loading;

	[Header("Components")]
	public TopScreenMessage topScreenMessage;

	void Awake() {
		if(Instance == null) {
			Instance = this;
		}
	}

	void Start() {
		base.Start();
	}
		
	protected override void StartMyStateUIEvent(GameState state) {	}
		
	public void GoFromLoadingToLobby() {
		if(screenFader != null) {
			screenFader.FadeOutFast(null, game.LoadGameLobby);
		}
	}

	public void PlayTakingLongerMessage() {
		if (topScreenMessage != null) {
			topScreenMessage.PlayTakingLongerMessage();
		}
	}

	public void PlayCouldNotConnectMessage() {
		DisableLoadingMessage();

		if (topScreenMessage != null) {
			topScreenMessage.PlayCouldNotConnectMessage();
		}
	}

	public void PlayRetryingTheConnectionMessage() {
		if (topScreenMessage != null) {
			topScreenMessage.PlayRetryingTheConnectionMessage();
		}
	}

	private void DisableLoadingMessage() {
		loading.enabled = false;
		loading.gameObject.SetActive(false);
	}
}
