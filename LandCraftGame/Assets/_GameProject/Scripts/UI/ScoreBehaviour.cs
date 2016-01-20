using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreBehaviour : MonoBehaviour {
	public bool hasPulseAnimation;
	public Text myValue;
	public Animator myAnimator;
	public bool doAchievementAnimation;

	private long longValue = 0;

	UIAchievement achievement;

	void Start() {
		achievement = FindObjectOfType<UIAchievement>();
	}

	public long LongValue {
		get { return longValue; }
	}

	public void AddPoints(long morePoints) {
		longValue += morePoints;

		myValue.text = longValue.ToString();

		if(hasPulseAnimation) {
			this.PulseAnimation();
		}

		if(longValue >= 100 && doAchievementAnimation) {
			achievement.ShowAchievement();
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