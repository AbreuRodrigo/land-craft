using UnityEngine;
using System.Collections;

public class GameLoadingController : CoreController {
	int timeoutLimit = 5;

	public void Awake() {
		base.Awake();

		StartCoroutine(StartLoadingBarProgress());
	}

	IEnumerator StartLoadingBarProgress() {
		while(timeoutLimit >= 0) {
			timeoutLimit--;
			yield return new WaitForSeconds(1);
		}

		if(NetworkManager.Instance.HasInternetConnection()) {
			GUIManagerGameLoading.Instance.GoFromLoadingToLobby();
		} else {
			GUIManagerGameLoading.Instance.ShowNoInternetConnectionMessage();
		}
	}

	protected override void InitializeComponents() {}
}