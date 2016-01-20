using UnityEngine;
using System.Collections;

public class MorePointsFX : MonoBehaviour {

	public TextMesh myTextMesh;

	public int Value {
		get; set;
	}

	void Start() {
		if(myTextMesh != null) {
			myTextMesh.text = "+" + Value.ToString();
		}

		StartCoroutine(StartFadingOut());
	}

	IEnumerator StartFadingOut() {
		float alpha = 1.0f;
		Color newColor = myTextMesh.color;

		Vector3 init = transform.position;

		while(alpha > 0) {
			yield return new WaitForSeconds(0.01f);

			alpha -= 0.01f;

			newColor.a = alpha; 

			myTextMesh.color = newColor;

			init.y += 0.025f;
			transform.position = init;
		}

		Destroy(gameObject, 0.25f);
	}
}