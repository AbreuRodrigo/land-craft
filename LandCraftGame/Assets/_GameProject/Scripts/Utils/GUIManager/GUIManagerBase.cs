using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class GUIManagerBase : MonoBehaviour {
	public static GUIManagerBase Instance;

	protected string clientToken;

	[Header("BaseComponents")]
	public CoreController game;
	public ScreenFader screenFader;
	public ScreenFilter screenFilter;
	public Image interactionBlocker;

	protected virtual void Start() {
		if(game != null) {
			StartUILogicsForGameState(game.State);
		}

		if (clientToken == null) {
			clientToken = SystemInfo.deviceUniqueIdentifier;
		}
	}

	protected abstract void StartMyStateUIEvent(GameState state);

	protected void DoSimpleClickFadeLogics(System.Action before = null, System.Action after = null) {
		SoundManager.Instance.PlaySelector();
		SoundManager.Instance.DoFadeSoundOut();
		
		if(screenFader != null) {
			screenFader.FadeOutFast (before, after);
		}
	}

	public void ShowScreenFilter() {
		if(screenFilter != null) {
			screenFilter.gameObject.SetActive(true);
			screenFilter.Show();
		}
	}
	
	public void HideScreenFilter() {
		if(screenFilter != null) { 
			screenFilter.Hide ();
			screenFilter.gameObject.SetActive (false);
		}
	}

	public void DoPlainButtonPress() {
		SoundManager.Instance.PlaySelector();
	}

	public void GoToGameLobbyScene() {
		SoundManager.Instance.PlaySelector();

		if(screenFader != null) {
			screenFader.FadeOutFast(null, game.LoadGameLobby);
		}
	}
	
	public void GoToStageSelectionScene() {
		SoundManager.Instance.PlaySelector();
		
		if(screenFader != null) {
			screenFader.FadeOutFast(null, game.LoadGameStageSelection);
		}
	}
	
	public void GoToGameFreeModeScene() {
		SoundManager.Instance.PlaySelector();
		
		if(screenFader != null) {
			screenFader.FadeOutFast(null, game.LoadGameFreeModeScene);
		}
	}

	public void DisableGUIInteraction() {
		if(interactionBlocker != null) {
			interactionBlocker.gameObject.SetActive(true);
		}
	}
	
	public void EnableGUIInteraction() {
		if(interactionBlocker != null) {
			interactionBlocker.gameObject.SetActive(false);
		}
	}

	private void StartUILogicsForGameState(GameState state) {
		StartMyStateUIEvent(state);
	}

	protected IEnumerator ScheduleAction(float time, Button button) {
		yield return new WaitForSeconds(time);
		
		button.gameObject.SetActive(true);
		button.GetComponent<Animator>().Play("Show");
	}
}
