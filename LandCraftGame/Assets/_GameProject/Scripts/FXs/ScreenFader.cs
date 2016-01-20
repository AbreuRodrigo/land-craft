using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour {

	public ScreenFaderEvent startEvent;

	public Image myImage;
	public Animator myAnimator;

	void Awake() {
		DoEventLogic();
	}

	private void DoEventLogic() {
		myImage.enabled = true;
		myAnimator.Play(startEvent.ToString());
	}

	public void DoAfterFadeIn() {
		myImage.enabled = false;
	}

	public void DoAfterFadeOut() {
	}

	public void FadeOutSlow(System.Action before = null, System.Action after = null) {
		RunEvent(before);

		myImage.enabled = true;
		myAnimator.Play(ScreenFaderEvent.FadeOutSlow.ToString());

		RunEvent(after);
	}

	public void FadeOutFast(System.Action before = null, System.Action after = null) {
		RunEvent(before);

		myImage.enabled = true;
		myAnimator.Play(ScreenFaderEvent.FadeOutFast.ToString());

		RunEvent(after);
	}

	private void RunEvent(System.Action e) {
		if(e != null) {
			e();
		}
	}
}
