using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManagerStageSelection : GUIManagerBase {
	public static GUIManagerStageSelection Instance;

	void Awake() {
		if (Instance == null) {
			Instance = this;
		}
	}

	void Start() {
		base.Start();
	}

	protected override void StartMyStateUIEvent(GameState state) {
	
	}

	public void DoStageSelectionPress(int index) {
		PlayerPrefsManager.ClickedStage = index;
		
		DoSimpleClickFadeLogics(null, game.LoadGamePlayScene);
	}
}
