using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Reflection;

public abstract class CoreController : MonoBehaviour {
	private PlayerStatsManager playerStatsManager;
	public PlayerStatsManager PlayerStatsManager {
		get { return playerStatsManager; }
	}

	public CameraBehaviour GameCamera;

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

	public StateManager StateManager { get; set; }

	private bool stageIsOnGoing;
	public bool StageIsOnGoing {
		get { return stageIsOnGoing; }
		set { stageIsOnGoing = value; } 
	}

	public int[] PlayerGridSetupStats {
		get { 
			return PlayerStatsManager != null ? PlayerStatsManager.PlayerStats.GridSetup : null; 
		}
	}

	public void Awake() {
		StateManager = StateManager.Instance;
		StateManager.ChangeState(State);

		playerStatsManager = FindObjectOfType<PlayerStatsManager>();

		InitializeComponents();
	}

	public virtual void OnApplicationQuit() {
		UpdatePlayerOfflineOnServer ();
	}

	public virtual void OnApplicationPause(bool pauseStatus) {
		if(pauseStatus) {
			UpdatePlayerOfflineOnServer();
		}else {
			UpdatePlayerOnlineOnServer();
		}
	}

	protected abstract void InitializeComponents();

	public void LoadGameLobby() {
		LoadGameScene("gameLobby");
	}

	public void LoadGameLoading() {
		LoadGameScene("gameLoading");
	}

	public void LoadGameFreeModeScene() {
		LoadGameScene("gamePlay");
	}

	public void LoadGameScene(string scene) {
		StartCoroutine(LoadSceneInSeconds(scene));
	}

	void ChangeGameView(GameView gameView) {
		view = gameView;
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

	public void UpdatePlayerOnlineOnServer() {
		if (playerStatsManager != null && playerStatsManager.PlayerStats != null) {
			playerStatsManager.PlayerStats.Online = true;
			playerStatsManager.PlayerStats.Save();
		}
	}

	public void UpdatePlayerOfflineOnServer() {
		if (playerStatsManager != null && playerStatsManager.PlayerStats != null) {
			playerStatsManager.PlayerStats.Online = false;
			playerStatsManager.PlayerStats.Save();
		}
	}

	public void UpdatePlayerOnServer() {
		if(playerStatsManager != null && playerStatsManager.PlayerStats != null) {
			playerStatsManager.PlayerStats.Save();
		}
	}

	IEnumerator LoadSceneInSeconds(string scene) {
		yield return new WaitForSeconds(1f);

		SceneManager.LoadScene(scene);
	}
}