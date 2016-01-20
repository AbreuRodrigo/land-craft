using UnityEngine;
using System.Collections;

public class CloudBehaviour : MonoBehaviour {

	public float speed;

	private Vector3 respawnPoint;

	void Start() {
		respawnPoint = new Vector3(transform.parent.position.x, transform.position.y, transform.position.z);
	}

	void Update() {	
		transform.Translate(Vector3.left * Time.deltaTime * speed);
	}

	void OnBecameInvisible() {
		transform.position = respawnPoint;
	}
}
