using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {
	public static GUIManager Instance;

	public Text clientToken;

	[Header("Buttons")]
	public Button playBtn;
	public Button playLevelsBtn;
	public Button playFreeModeBtn;
	public Button configBtn;
	public Button storeBtn;

	[Header("Images")]
	public Image logo;
	public Image facebookBtnStructure;
	public Image twitterBtnStructure;
	public Image googlePlusBtnStructure;
	public Image interactionBlocker;

	[Header("Texts")]
	public Text crystals;
	public Text loading;
	public Text noInternetConnection;

	public UIResourceCounter WoodCounter;
	public UIResourceCounter ManaCounter;
	public UIResourceCounter StoneCounter;
	public UIResourceCounter GoldCounter;

	public ScreenFader screenFader;

	public CoreController game;

	private Dictionary<ResourceType, UIResourceCounter> CouterByResourceType = 
		new Dictionary<ResourceType, UIResourceCounter>();

	void Awake() {
		if(Instance == null) {
			Instance = this;
		}
	}

	void Start() {
		InitializeCouterByResourceType();

		if (clientToken != null) {
			clientToken.text = SystemInfo.deviceUniqueIdentifier;
		}
	}

	public void DoPlainButtonPress() {
		SoundManager.Instance.PlaySelector();
	}

	public void GoFromLoadingToGameLobby() {
		if(screenFader != null) {
			screenFader.FadeOutFast(null, game.LoadGameLobby);
		}
	}

	public void GoFromLoadingToLobby() {
		if(screenFader != null) {
			screenFader.FadeOutFast(null, game.LoadGameLobby);
		}
	}

	public void GoFromGameFreeModeToGameLobby() {
		SoundManager.Instance.PlaySelector();
		
		if(screenFader != null) {
			screenFader.FadeOutFast(null, game.LoadGameFreeModeScene);
		}
	}

	public void SetCrystals(int crystals) {
		this.crystals.text = "" + crystals;
	}

	public void AddCrystals(int crystals) {
		crystals += int.Parse(this.crystals.text);
		SetCrystals(crystals);
	}

	public void DeductCrystals(int crystals) {
		crystals -= int.Parse(this.crystals.text);
		SetCrystals(crystals);
	}

	public void DoLobbyPlayFreeModeButtonPress() {
		DoSimpleClickFadeLogics (
			playFreeModeBtn.GetComponent<UIButtonExtraBehaviour>().DoMyClick,
			game.LoadGameFreeModeScene
		);
	}

	private void DoSimpleClickFadeLogics(System.Action before = null, System.Action after = null) {
		SoundManager.Instance.PlaySelector();
		SoundManager.Instance.DoFadeSoundOut();

		if(screenFader != null) {
			screenFader.FadeOutFast (before, after);
		}
	}

	public void ShowResourceCounterByType(LandType type) {
		if(type.Equals(LandType.ManaTree)) {
			ShowManaResourceCounter();
		}
		if(type.Equals(LandType.Trees)) {
			ShowWoodResourceCounter();
		}
	}

	public void ShowManaResourceCounter() {
		if(ManaCounter != null) {
			ManaCounter.FadeIn();
		}
	}
	public void ShowWoodResourceCounter() {
		if(WoodCounter != null) {
			WoodCounter.FadeIn ();
		}
	}
	public void ShowStoneResourceCounter() {
		if(StoneCounter != null) {
			StoneCounter.FadeIn();
		}
	}
	public void ShowGoldResourceCounter() {
		if(GoldCounter != null) {
			GoldCounter.FadeIn ();
		}
	}

	public void ShowNoInternetConnectionMessage() {
		logo.enabled = false;
		logo.gameObject.SetActive(false);

		loading.enabled = false;
		loading.gameObject.SetActive(false);

		noInternetConnection.gameObject.SetActive(true);
	}

	public void HideFacebookHalo() {
		facebookBtnStructure.gameObject.SetActive(false);
	}

	public void ShowFacebookHalo() {
		facebookBtnStructure.gameObject.SetActive(true);
	}

	public void HideTwitterHalo() {
		twitterBtnStructure.gameObject.SetActive(false);
	}

	public void ShowTwittweHalo() {
		twitterBtnStructure.gameObject.SetActive(true);
	}

	public void HideGooglePlusHalo() {
		googlePlusBtnStructure.gameObject.SetActive(false);
	}

	public void ShowGooglePlusHalo() {
		googlePlusBtnStructure.gameObject.SetActive(true);
	}

	public void SumUpResourceByType(ResourceType type, int amount) {
		CouterByResourceType[type].AddResource(amount);
	}

	public void DisableGUIInteraction() {
		interactionBlocker.gameObject.SetActive(true);
	}

	public void EnableGUIInteraction() {
		interactionBlocker.gameObject.SetActive(false);
	}

	private void InitializeCouterByResourceType() {
		CouterByResourceType.Add(ResourceType.Wood, WoodCounter);
		CouterByResourceType.Add(ResourceType.Mana, ManaCounter);
		CouterByResourceType.Add(ResourceType.Stone, StoneCounter);
		CouterByResourceType.Add(ResourceType.Gold, GoldCounter);
	}

	IEnumerator ScheduleAction(float time, Button button) {
		yield return new WaitForSeconds(time);
		
		button.gameObject.SetActive(true);
		button.GetComponent<Animator>().Play("Show");
	}
}