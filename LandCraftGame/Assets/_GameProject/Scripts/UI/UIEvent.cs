using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIEvent : MonoBehaviour {
	public Image achievementContainer;

	public Sprite levelUpAchivement;

	public Animator myAnimator;

	private bool isShowingAchievement;
	private bool isEnabled = true;

	public void ShowAchievement() {
		if(!isShowingAchievement && isEnabled) {
			achievementContainer.sprite = levelUpAchivement;
			myAnimator.Play("Show");

			isShowingAchievement = true;

			StartCoroutine("StartAutoHideEvent");
		}
	}

	public void HideAchievement() {
		myAnimator.Play("Hide");
		isShowingAchievement = false;
		isEnabled = false;
	}

	IEnumerator StartAutoHideEvent() {
		yield return new WaitForSeconds(5);

		HideAchievement();
	}
}
