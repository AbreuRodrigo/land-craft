using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {
	public static EventManager Instance;
	public Image eventIcon;
	public Animator myAnimator;

	private bool isShowingEvent;

	//Event Sprites
	public Sprite levelUpEventIcon;

	public CoreController game;

	void Awake() {
		if(Instance == null) {
			Instance = this;
		}
		if(game == null) {
			game = FindObjectOfType<CoreController>();
		}
	}

	public void ShowLevelUpEvent() {
		ShowEvent(levelUpEventIcon);
	}

	private void ShowEvent(Sprite icon) {
		if(icon != null) {
			if (!isShowingEvent) {
				eventIcon.sprite = icon;
				myAnimator.Play("Show");
				isShowingEvent = true;

				StartCoroutine(StartEvent());
			} else {
				HideEvent();
				ShowEvent(icon);
			}

			if(game != null) {
				game.UpdatePlayerOnServer();
			}
		}
	}

	private void HideEvent() {
		myAnimator.Play("Hide");
		isShowingEvent = false;
	}

	IEnumerator StartEvent() {
		yield return new WaitForSeconds(5);

		HideEvent();

		yield return new WaitForSeconds(1);

		eventIcon.sprite = null;
	}
}
