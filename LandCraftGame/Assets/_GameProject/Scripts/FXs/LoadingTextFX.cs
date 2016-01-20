using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingTextFX : MonoBehaviour {
	public Text loadingTxt;
	private int dots = 1;
	private string loadingLabel = "Loading";

	void Start() {
		StartCoroutine("LoadingAnimation");
	}

	IEnumerator LoadingAnimation() {
		while(true) {
			loadingTxt.text = loadingLabel + NumberToDots(dots);

			dots++;

			if(dots > 3) {
				dots = 1;
			}

			yield return new WaitForSeconds(0.25f);
		}
	}

	private string NumberToDots(int n) {
		if (n == 1) { 
			return ".";
		} else if (n == 2) {
			return "..";
		} else if (n == 3) {
			return "...";
		}

		return "";
	}
}
