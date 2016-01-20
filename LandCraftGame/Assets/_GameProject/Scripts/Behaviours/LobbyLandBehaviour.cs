using UnityEngine;
using System.Collections;

public class LobbyLandBehaviour : MonoBehaviour {
	public float speed;
	public float distance;

	private int direction;
	private float initY;

	void Start() {
		initY = transform.position.y;
	}

	void Update() {
		Bounce();
	}

	void Bounce() {
		if (direction == 0) {
			transform.Translate(Vector3.down * Time.deltaTime * speed);

			if (transform.position.y <= (initY)) {
				direction = 1;
			}
		} else {
			transform.Translate(Vector3.up * Time.deltaTime * speed);
			
			if(transform.position.y >= (initY + distance)) {
				direction = 0;
			}
		}
	}
}