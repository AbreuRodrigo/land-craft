using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementContainer : MonoBehaviour {
	private const float WAIT_UNTIL_AUTO_HIDE = 5;

	public Animator myAnimator;
	public Image icon;
	public Text description;

	public void Show(Sprite sprite, string text) {
		if(myAnimator != null) {
			if(sprite != null) {
				icon.sprite = sprite;
			}
			if(description != null) {
				description.text = text;
			}

			myAnimator.Play("Show");

			StartCoroutine(TimingToAutoHide());
		}
	}

	public void Hide() {
		if(myAnimator != null) {
			myAnimator.Play("Hide");
		}
	}

	IEnumerator TimingToAutoHide() {
		yield return new WaitForSeconds(WAIT_UNTIL_AUTO_HIDE);

		this.Hide();

		yield return new WaitForSeconds(1);

		this.enabled = false;
		this.gameObject.SetActive(false);
		this.icon.sprite = null;
		this.description.text = "";
	}
}