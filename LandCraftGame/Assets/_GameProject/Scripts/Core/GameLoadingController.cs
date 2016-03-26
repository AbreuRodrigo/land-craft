using UnityEngine;
using System.Collections;

public class GameLoadingController : CoreController {

	public int takingLonderToLoad = 7;
	public int limitCouldNotLoad = 20;

	private bool couldNotConnect;
	private bool showedTakingLongerMessage;

	void Awake() {
		base.Awake();
	}

	void Start() {	
		StartCoroutine(StartLoading());
	}

	IEnumerator StartLoading() {
		if (!NetworkManager.Instance.HasInternetConnection()) {
			GUIManagerGameLoading.Instance.ShowNoInternetConnectionMessage();
		} else {			
			while(!DataLab.DataLabManager.Instance.HasLoaded) {
				if(Time.time >= takingLonderToLoad && !showedTakingLongerMessage) {
					showedTakingLongerMessage = true;
					GUIManagerGameLoading.Instance.PlayTakingLongerMessage();
				}
				if(Time.time > limitCouldNotLoad) {
					couldNotConnect = true;
					GUIManagerGameLoading.Instance.PlayCouldNotConnectMessage();
				}

				yield return null;
			}

			if (!couldNotConnect) {
				float whenLoaded = Time.time;

				while (Time.time < whenLoaded + 2) {
					yield return null;
				}

				GUIManagerGameLoading.Instance.GoFromLoadingToLobby();
			}
		}
	}

	protected override void InitializeComponents() { }
}