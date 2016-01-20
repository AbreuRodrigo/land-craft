using UnityEngine;
using System.Collections;

public class ViewBehaviour : MonoBehaviour {
	public Animator myAnimator;

	public void Fold() {
		if(myAnimator != null) {
			myAnimator.Play("Fold");
		}
	}

	public void Unfold() {
		if(myAnimator != null) {
			myAnimator.Play("Unfold");
		}
	}
}
