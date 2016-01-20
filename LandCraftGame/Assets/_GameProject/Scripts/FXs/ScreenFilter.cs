using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFilter : MonoBehaviour {
	public Image myImage;

	public void Hide() {
		myImage.enabled = false;
	}

	public void Show() {
		myImage.enabled = true;
	}
}
