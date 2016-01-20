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
		logo.enabled = false;
		logo.gameObject.SetActive(false);
		
		loading.enabled = false;
		loading.gameObject.SetActive(false);
		
		noInternetConnection.gameObject.SetActive(true);
	}
}
