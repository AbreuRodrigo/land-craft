using UnityEngine;
using System.Collections;

public class MoreResourceFX : MonoBehaviour {

	public TextMesh myTextMesh;
	public SpriteRenderer mySpriteRenderer;

	public int Value {
		get; set;
	}

	public Sprite Icon {
		get { return mySpriteRenderer.sprite; }
		set { mySpriteRenderer.sprite = value; }
	}
	
	void Start() {
		if(myTextMesh != null) {
			myTextMesh.text = "+" + Value.ToString();
		}
		
		StartCoroutine(StartFadingOut());
	}

	IEnumerator StartFadingOut() {
		float alpha = 1.0f;
		Color tmc = myTextMesh.color;
		Color src = mySpriteRenderer.color;
		
		Vector3 init = transform.position;

		while(alpha > 0) {
			yield return new WaitForSeconds(0.01f);
			
			alpha -= 0.01f;
			
			tmc.a = alpha;
			src.a = alpha;
			
			myTextMesh.color = tmc;
			mySpriteRenderer.color = src;
			
			init.y += 0.025f;
			transform.position = init;
		}
		
		Destroy(gameObject, 0.25f);
	}
}