using UnityEngine;
using System.Collections;

public class SelectorBehaviour : MonoBehaviour {
	public MeshRenderer selector;

	public bool Hidden {
		get { return !selector.enabled; }
	}
	public bool Visible {
		get { return selector.enabled; }
	}
				
	public void Reposition(Vector3 targetPosition) {
		if(targetPosition != null) {
			Vector3 newPosition = targetPosition;
			newPosition.y = 0.28f;
			
			selector.transform.position = newPosition;

			Show();
		}
	}

	public void Show() {
		gameObject.SetActive(true);
		selector.enabled = true;
	}
	
	public void Hide() {
		selector.enabled = false;
		gameObject.SetActive(false);
	}
}