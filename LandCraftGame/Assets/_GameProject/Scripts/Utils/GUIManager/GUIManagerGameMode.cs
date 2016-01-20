using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManagerGameMode : GUIManagerBase {
	public static GUIManagerGameMode Instance;

	private const string BASE_TOTAL_STARS_STR = "/300";

	[Header("Buttons")]
	public Button backToLobbyBtn;
	public Button playLevelsBtn;
	public Button playFreeModeBtn;

	[Header("Texts")]
	public Text highscoreTxt;
	public Text totalStarsTxt;

	void Awake() {
		if(Instance == null) {
			Instance = this;
		}
	}

	public void Start() {
		base.Start();

		InitializeUIValues();
	}

	public void BackToLobbyButtonPress() {
		DoSimpleClickFadeLogics (
			backToLobbyBtn.GetComponent<UIButtonExtraBehaviour>().DoMyClick,
			game.LoadGameLobby
		);
	}

	public void PlayStagesButtonPress() {
		DoSimpleClickFadeLogics (
			playLevelsBtn.GetComponent<UIButtonExtraBehaviour>().DoMyClick,
			game.LoadGameStageSelection
		);
	}
	
	public void PlayFreeModeButtonPress() {
		DoSimpleClickFadeLogics (
			playFreeModeBtn.GetComponent<UIButtonExtraBehaviour>().DoMyClick,
			game.LoadGameFreeModeScene
		);
	}

	protected override void StartMyStateUIEvent(GameState state) { }

	private void InitializeUIValues() {
		if(highscoreTxt != null && game != null && game.PlayerStatsManager != null) {
			highscoreTxt.text = game.PlayerStatsManager.PlayerStats.score.ToString();
		}
		
		if(totalStarsTxt != null && game != null && game.PlayerStatsManager != null) {
			totalStarsTxt.text = game.PlayerStatsManager.PlayerStats.totalStars.ToString();
			totalStarsTxt.text += BASE_TOTAL_STARS_STR;
		}
	}
}