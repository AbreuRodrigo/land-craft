using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIButtonExtraBehaviour : MonoBehaviour {
	public bool startsFadingIn;
	public Animator myAnimator;
	public Button myButton;
	public Image myImage;
	public Image halo;

	void Start() {
		if(startsFadingIn) {
			myAnimator.enabled = false;
			myImage.color = new Color(1, 1, 1, 0);
			myImage.enabled = false;
			myButton.enabled = false;
			StartCoroutine("FadeIn");
		}
	}

	public void DoMyClick() {
		myButton.interactable = false;
	}

	IEnumerator FadeIn() {
		yield return new WaitForSeconds(1.5f);

		myImage.enabled = true;

		Color alphaTransition = new Color(1, 1, 1, 0);

		while(myImage.color.a < 1) {
			alphaTransition.a += 0.05f;
			myImage.color = alphaTransition;
			yield return new WaitForSeconds(0.01f);
		}

		myButton.enabled = true;
		myAnimator.enabled = true;
	}
}
