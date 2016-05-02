using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using UnityEngine.SocialPlatforms;

public class GameLoadingController : CoreController {

	public int takingLongerToLoad = 10;
	public int limitCouldNotLoad = 20;

	private bool couldNotConnect;

	private DataLab.DataLabManager dataLabManager;

	void Awake() {
		base.Awake();
	}

	void Start() {
		//ConnectToGoogle();

		StartCoroutine(StartLoading());
	}

	IEnumerator StartLoading() {
		float startLoadingTime = Time.time;

		while((dataLabManager == null || !dataLabManager.HasLoaded) && !couldNotConnect) {
			if(GetDeltaTime(startLoadingTime) >= takingLongerToLoad) {
				GUIManagerGameLoading.Instance.PlayTakingLongerMessage();
			}
			if((GetDeltaTime(startLoadingTime) > limitCouldNotLoad) || dataLabManager.HasError()) {
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

	private float GetDeltaTime(float initial) {
		return Time.time - initial;
	}

	private void ConnectToGoogle() {
		//GooglePlayGames.PlayGamesPlatform.Activate();

		Social.localUser.Authenticate((bool success) => {
		});
	}

	protected override void InitializeComponents() { 
		dataLabManager = DataLab.DataLabManager.Instance;
	}
}