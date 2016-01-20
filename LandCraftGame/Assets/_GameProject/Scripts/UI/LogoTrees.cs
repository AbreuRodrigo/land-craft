using UnityEngine;
using System.Collections;

public class LogoTrees : MonoBehaviour {

	public void DoFallingSound() {
		SoundManager.Instance.PlayFalling();
	}

	public void DoCollidedSound() {
		SoundManager.Instance.PlayEarthShake();
	}
}
