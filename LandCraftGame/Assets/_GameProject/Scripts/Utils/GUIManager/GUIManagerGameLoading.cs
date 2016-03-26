using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManagerGameLoading : GUIManagerBase {
	public static GUIManagerGameLoading Instance;

	[Header("Images")]
	public Image logo;

	[Header("Texts")]
	public Text loading;
	public Text noInternetConnection;

	[Header("Components")]
	public TopScreenMessage topScreenMessage;

	void Awake() {
		if(Instance == null) {
			Instance = this;
		}
	}

	public void Start() {
		base.Start();
	}
		
	protected override void StartMyStateUIEvent(GameState state) {	}
		
	public void GoFromLoadingToLobby() {
		if(screenFader != null) {
			screenFader.FadeOutFast(null, game.LoadGameLobby);
		}
	}
	
	public void ShowNoInternetConnectionMessage() {
		DisableLogo();		
		DisableLoadingMessage();
		
		noInternetConnection.gameObject.SetActive(true);
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

	private void DisableLogo() {
		logo.enabled = false;
		logo.gameObject.SetActive(false);
	}

	private void DisableLoadingMessage() {
		loading.enabled = false;
		loading.gameObject.SetActive(false);
	}
}
