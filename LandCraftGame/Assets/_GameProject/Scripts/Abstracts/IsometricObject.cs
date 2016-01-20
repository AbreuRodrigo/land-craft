using UnityEngine;
using System.Collections;

public abstract class IsometricObject : MonoBehaviour {
	public Collider myCollider;
	public IsoType isoType;
	public bool selected;

	public void Activate() {
		this.gameObject.SetActive(true);
	}

	public void Deactivate() {
		this.gameObject.SetActive(false);
	}
}
