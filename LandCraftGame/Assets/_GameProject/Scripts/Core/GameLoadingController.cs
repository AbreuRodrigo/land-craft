using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using UnityEngine.SocialPlatforms;

public class GameLoadingController : CoreController {

	public int takingLongerToLoad = 10;
	public int limitCouldNotLoad = 20;
	public int limitRetry = 25;

	private bool couldNotConnect;
	private bool showedTakingLongerMessage;
	private bool retried;

	private DataLab.DataLabManager dataLabManager;

	void Awake() {
		base.Awake();
	}

	void Start() {
		//ConnectToGoogle();

		InitializeComponents();

		StartCoroutine(StartLoading());
	}

	IEnumerator StartLoading() {
		float startLoadingTime = Time.time;

		if (!NetworkManager.Instance.HasInternetConnection()) {
			GUIManagerGameLoading.Instance.ShowNoInternetConnectionMessage();
		} else {
			while(dataLabManager == null || !dataLabManager.HasLoaded) {
				if(GetDeltaTime(startLoadingTime) >= takingLongerToLoad && !showedTakingLongerMessage) {
					showedTakingLongerMessage = true;
					GUIManagerGameLoading.Instance.PlayTakingLongerMessage();
				}
				if(GetDeltaTime(startLoadingTime) > limitCouldNotLoad) {
					couldNotConnect = true;
					GUIManagerGameLoading.Instance.PlayCouldNotConnectMessage();
				}
				if(GetDeltaTime(startLoadingTime) > limitRetry && !retried) {
					InitializeComponents();
					retried = true;
					startLoadingTime = Time.time;
					showedTakingLongerMessage = false;
					couldNotConnect = false;
					GUIManagerGameLoading.Instance.PlayRetryingTheConnectionMessage();
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

	private float GetDeltaTime(float initial) {
		return Time.time - initial;
	}

	private void ConnectToGoogle() {
		GooglePlayGames.PlayGamesPlatform.Activate();

		Social.localUser.Authenticate((bool success) => {
		});
	}

	protected override void InitializeComponents() { 
		dataLabManager = DataLab.DataLabManager.Instance;
	}
}