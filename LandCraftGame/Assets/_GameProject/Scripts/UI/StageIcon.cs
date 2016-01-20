using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageIcon : MonoBehaviour {
	public int index;
	public bool isActive;

	public Animator myAnimator;
	public Button myButtonBehaviour;
	public Image openImg;
	public Image closedImg;
	public Image starOne;
	public Image starTwo;
	public Image starThree;
	public Text timingTxt;
	public Text numberTxt;

	private Action<int> myClickAction;

	public void DoClick() {
		if (isActive && numberTxt != null && myClickAction != null) {
			myClickAction(index);
		} else {
			SoundManager.Instance.PlayDenied();
		}
	}

	public void SetMyClickAction(Action<int> clickAction) {
		myClickAction = clickAction;
	}

	public void StageSetup(Stage stageModel) {
		if(stageModel != null) {

			index = stageModel.index + 1;
			isActive = true;

			if(stageModel.isClear) {
				closedImg.gameObject.SetActive(false);
				openImg.gameObject.SetActive(true);

				EnableNumberText(index);
				EnableTimingText(stageModel.time);

				if(stageModel.stars == 3) {
					EnableStarOne();
					EnableStarTwo();
					EnableStarThree();
				}else if(stageModel.stars == 2) {
					EnableStarOne();
					DisableStarTwo();
					EnableStarThree();
				}else if(stageModel.stars == 1) {
					EnableStarOne();
					DisableStarTwo();
					DisableStarThree();
				}
			}else if(stageModel.isOpen) {
				closedImg.gameObject.SetActive(false);
				openImg.gameObject.SetActive(true);

				EnableNumberText(index);

				DisableStarOne();
				DisableStarTwo();
				DisableStarThree();
				DisableTimingText();
			}else if(stageModel.isLocked) {
				openImg.gameObject.SetActive(false);
				closedImg.gameObject.SetActive(true);

				EnableNumberText(index);

				isActive = false;
			}
		}
	}

	private void EnableStarOne() {
		starOne.gameObject.SetActive(true);
	}
	private void EnableStarTwo() {
		starTwo.gameObject.SetActive(true);
	}
	private void EnableStarThree() {
		starThree.gameObject.SetActive(true);
	}
	private void EnableTimingText(int timing) {
		timingTxt.gameObject.SetActive(true);
		timingTxt.text = TimeManager.TimeSpanAsString(new TimeSpan(0, 0, timing));
	}
	private void EnableNumberText(int number) {
		numberTxt.gameObject.SetActive(true);
		numberTxt.text = "" + number;
	}

	private void DisableStarOne() {
		starOne.gameObject.SetActive(false);
	}
	private void DisableStarTwo() {
		starTwo.gameObject.SetActive(false);
	}
	private void DisableStarThree() {
		starThree.gameObject.SetActive(false);
	}
	private void DisableTimingText() {
		timingTxt.gameObject.SetActive(false);
	}
	private void DisableNumberText() {
		numberTxt.gameObject.SetActive(false);
	}
}