using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStateMessage : MonoBehaviour {
	public Image myImage;
	public Animator myAnimator;

	public Sprite stageClearSprite;
	public Sprite gameOverSprite;
	public Sprite victorySprite;
	public Sprite failureSprite;

	public void RunStageClearMessage() {
		myImage.sprite = stageClearSprite;
		ShowMessage();
	}

	public void RunGameOverMessage() {
		myImage.sprite = gameOverSprite;
		ShowMessage();
	}

	public void RunVictoryMessage() {
		myImage.sprite = victorySprite;
		ShowMessage();
	}

	public void RunFailureMessage() {
		myImage.sprite = failureSprite;
		ShowMessage();
	}

	private void ShowMessage() {
		myAnimator.Play("Show");
	}

	private void HideMessage() {
		myAnimator.Play("Hide");
	}
}
