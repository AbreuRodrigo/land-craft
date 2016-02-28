using UnityEngine;
using System.Collections;
using System.Reflection;

public abstract class CoreController : MonoBehaviour {
	public int currentStage;
	public int totalStages;

	private PlayerStatsManager playerStatsManager;
	public PlayerStatsManager PlayerStatsManager {
		get { return playerStatsManager; }
	}

	private static ProgressManager progressManager;
	private static HashManager hashManager;

	public CameraBehaviour GameCamera;

	[SerializeField]
	private GameDifficulty difficulty;
	public GameDifficulty Difficulty {
		get { return difficulty; }
		set { difficulty = value; }
	}

	[SerializeField]
	public GameState state;
	public GameState State {
		get { return state; }
		set { 
			state = value;
			StateManager.ChangeState(state); 
		}
	}

	[SerializeField]
	private GameView view;
	public GameView View {
		get { return view; }
		set { view = value;	}
	}

	public ProgressManager ProgressManager { 
		get { return progressManager; } 
	}

	public HashManager HashManager {
		get { return hashManager; }
	}

	public StateManager StateManager { get; set; }

	private bool stageIsOnGoing;
	public bool StageIsOnGoing {
		get { return stageIsOnGoing; }
		set { stageIsOnGoing = value; } 
	}

	public void Awake() {
		StateManager = StateManager.Instance;
		StateManager.ChangeState(State);

		if(hashManager == null) {
			hashManager = new HashManager(totalStages, SystemInfo.deviceUniqueIdentifier);
		}
		if(progressManager == null) {
			progressManager = new ProgressManager(hashManager);
		}

		if (progressManager != null && (StateManager.IsGamePlayState || StateManager.IsGameStageGoalState)) {
			currentStage = PlayerPrefsManager.ClickedStage;
		} else {
			currentStage = 0;
		}

		playerStatsManager = FindObjectOfType<PlayerStatsManager>();

		InitializeComponents();
	}

	public void OnDisable() {
		UpdatePlayerOnServer();
	}

	protected abstract void InitializeComponents();

	public void LoadGamePlayScene() {
		LoadGameScene("gamePlay");
	}

	public void LoadGameFreeModeScene() {
		LoadGameScene("gameFreeMode");
	}

	public void LoadGameLobby() {
		LoadGameScene("gameLobby");
	}

	public void LoadGameStageSelection() {
		LoadGameScene("gameStageSelection");
	}

	public void LoadGameLoading() {
		LoadGameScene("gameLoading");
	}

	public void LoadGameModeSelection() {
		LoadGameScene("gameModeSelection");
	}

	public void LoadGameScene(string scene) {
		StartCoroutine(LoadSceneInSeconds(scene));
	}

	void ChangeGameView(GameView gameView) {
		view = gameView;
	}
	
	public void ChangeToOtherGameView() {
		ChangeGameView(GameView.OtherView);
	}
	
	public void ChangeToMyGameView() {
		ChangeGameView(GameView.MyView);
	}
	
	public bool IsOtherGameView() {
		return view.Equals(GameView.OtherView);
	}
	
	public bool IsMyGameView() {
		return view.Equals(GameView.MyView);
	}

	public void UpdatePlayerStatsContent(PlayerStatsContent playerStatsContent) {
		if(playerStatsContent != null && PlayerStatsManager != null && PlayerStatsManager.PlayerStats != null) {
			playerStatsContent.UpdatePlayerStatsContent(PlayerStatsManager.PlayerStats);
		}
	}
	
	public void UpdatePlayerNameOnServer(string newName) {
		if(playerStatsManager != null && playerStatsManager.PlayerStats != null) {
			playerStatsManager.PlayerStats.PlayerName = newName;
			playerStatsManager.PlayerStats.Save();
		}
	}

	public void UpdatePlayerScoreOnServer(long newScore) {
		if(playerStatsManager != null && playerStatsManager.PlayerStats != null) {
			playerStatsManager.PlayerStats.score = newScore;
			playerStatsManager.PlayerStats.Save();
		}
	}

	public void UpdatePlayerXPLocally(long xp) {
		if(playerStatsManager != null && playerStatsManager.PlayerStats != null) {
			playerStatsManager.PlayerStats.XP += xp;
		}
	}

	public void UpdatePlayerOnServer() {
		if(playerStatsManager != null && playerStatsManager.PlayerStats != null) {
			playerStatsManager.PlayerStats.Save();
		}
	}

	IEnumerator LoadSceneInSeconds(string scene) {
		yield return new WaitForSeconds(1.5f);

		Application.LoadLevel(scene);
	}
}