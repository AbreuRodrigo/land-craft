﻿using UnityEngine;
using System.Collections;

public class ModalPanelFX : MonoBehaviour {
	public Animator myAnimator;

	public void Show() {
		if(myAnimator != null) {
			myAnimator.Play("Show");
		}
	}

	public void Hide() {
		if(myAnimator != null) {
			myAnimator.Play("Hide");
		}
	}
}
