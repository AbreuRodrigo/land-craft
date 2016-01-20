using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManagerGameLobby : GUIManagerBase {
	public static GUIManagerGameLobby Instance;

	[Header("Buttons")]
	public Button playLevelsBtn;
	public Button playFreeModeBtn;
	public Button configBtn;
	public Button storeBtn;
	public Button inventoryBtn;
	public Button playerStatsBtn;
			
	[Header("Images")]
	public Image facebookBtnStruct;
	public Image twitterBtnStruct;
	public Image googlePlusBtnStruct;
	public Image interactionBlocker;
			
	[Header("Texts")]
	public Text crystals;

	[Header("MyComponents")]
	public PlayerNameText playerNameText;

	public void Awake() {
		if(Instance == null) {
			Instance = this;
		}
	}

	void Start () {
		base.Start();

		if(playerNameText == null) {
			playerNameText = FindObjectOfType<PlayerNameText>();
		}
	}

	public void DoLobbyPlayLevelsButtonPress() {
		DoSimpleClickFadeLogics (
			playLevelsBtn.GetComponent<UIButtonExtraBehaviour>().DoMyClick,
			game.LoadGameModeSelection
		);
	}
	
	public void DoLobbyPlayFreeModeButtonPress() {
		DoSimpleClickFadeLogics (
			playFreeModeBtn.GetComponent<UIButtonExtraBehaviour>().DoMyClick,
			game.LoadGameFreeModeScene
		);
	}

	public void DoInventoryButtonPress() {
		SoundManager.Instance.PlaySelector();
		ModalManager.Instance.ShowInventoryModal();
	}
	
	public void DoPlayerStatsButtonPress() {
		SoundManager.Instance.PlaySelector();
		ModalManager.Instance.ShowPlayerStatsModal();
	}
	
	public void DoStoreButtonPress() {
		SoundManager.Instance.PlaySelector();
		ModalManager.Instance.ShowStoreModal();		
	}

	public void UpdatePlayerNameText(string name) {
		playerNameText.UpdatePlayerName(name);
	}
	
	public void UpdatePlayerLevel(string level) {
		playerNameText.UpdateLevelText(level);
	}

	public void HideFacebookHalo() {
		facebookBtnStruct.gameObject.SetActive(false);
	}
	
	public void ShowFacebookHalo() {
		facebookBtnStruct.gameObject.SetActive(true);
	}
	
	public void HideTwitterHalo() {
		twitterBtnStruct.gameObject.SetActive(false);
	}
	
	public void ShowTwittweHalo() {
		twitterBtnStruct.gameObject.SetActive(true);
	}
	
	public void HideGooglePlusHalo() {
		googlePlusBtnStruct.gameObject.SetActive(false);
	}
	
	public void ShowGooglePlusHalo() {
		googlePlusBtnStruct.gameObject.SetActive(true);
	}

	public void DisableGUIInteraction() {
		interactionBlocker.gameObject.SetActive(true);
	}
	
	public void EnableGUIInteraction() {
		interactionBlocker.gameObject.SetActive(false);
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

	protected override void StartMyStateUIEvent(GameState state) { }
}
