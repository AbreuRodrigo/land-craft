using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreBehaviour : MonoBehaviour {
	public bool hasPulseAnimation;
	public Text myValue;
	public Animator myAnimator;

	private long longValue = 0;

	public long LongValue {
		get { return longValue; }
	}

	public void AddPoints(long morePoints) {
		longValue += morePoints;

		myValue.text = longValue.ToString();

		if(hasPulseAnimation) {
			this.PulseAnimation();
		}
	}

	public void OverwriteValue(long newValue) {
		longValue = 0;
		AddPoints(newValue);
	}

	private void PulseAnimation() {
		if(myAnimator != null) {
			myAnimator.Play("Pulse");
		}
	}
}