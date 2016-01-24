using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConfirmManager : MonoBehaviour {
	public static ConfirmManager Instance;
	public CoreController game;
	public GUIManagerBase guiManager;

	private const string EXIT_MESSAGE = "Do you really want to exit?";

	[Header("Components")]
	public ConfirmPanelFX confirmPanel;

	[Header("Texts")]
	public Text confirmText;

	private bool isShowingConfirmation;

	void Awake() {
		if(Instance == null) {
			Instance = this;
		}
	}

	public void HandleConfirmExitModal() {
		confirmText.text = EXIT_MESSAGE;

		if(!isShowingConfirmation) {		
			this.ShowConfirmModal();
		}else {
			this.HideConfirmModal();
		}
	}

	private void HideConfirmModal() {
		isShowingConfirmation = false;

		if(confirmPanel != null) {
			confirmPanel.Hide();
		}
		if(guiManager != null) {
			guiManager.EnableGUIInteraction();
		}
	}

	private void ShowConfirmModal() {
		isShowingConfirmation = true;

		if(guiManager != null) {
			guiManager.DisableGUIInteraction();
		}
		if(confirmPanel != null) {
			confirmPanel.Show();
		}
	}
}