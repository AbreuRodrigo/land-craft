using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ModalManager : MonoBehaviour {
	private const string ITEM_TYPE_SPELL = "Spell";
	private const string ITEM_TYPE_CURRENCY = "Currency";

	public static ModalManager Instance;

	public GameLobbyController game;

	[Header("Contents")]
	public PlayerStatsContent playerStatsContent;
	public StoreContent storeContentSpells;
	public StoreContent storeContentCurrencies;

	[Header("Components")]
	public ModalPanelFX modalPanel;

	[Header("Images")]
	public Image modalTitle;

	[Header("Sprites")]
	public Sprite configTitle;
	public Sprite inventoryTitle;
	public Sprite playerStatsTitle;
	public Sprite storeTitle;

	[Header("ModalContents")]
	public GameObject configContentPanel;
	public GameObject inventoryContentPanel;
	public GameObject playerStatsContentPanel;
	public GameObject storeContentPanel;

	[Header("Tabs")]
	public Button tab1;
	public Button tab2;

	[Header("Icons")]
	public Sprite spellIcon;
	public Sprite currencyIcon;

	private StoreItemManager storeItemManager;

	public int TotalSpellItems { get; set; }
	public int TotalCurrencyItems { get; set; }

	void Awake() {		
		if(Instance == null) {
			Instance = this;
		}

		storeItemManager = FindObjectOfType<StoreItemManager>();
	}

	void Start() {
		if (storeItemManager != null) {
			TotalSpellItems = storeItemManager.SpellCount;
			TotalCurrencyItems = storeItemManager.CurrencyCount;
			SetupGameStore ();
		}

		modalPanel.transform.SetSiblingIndex(1);
		tab1.transform.SetSiblingIndex(2);
		tab2.transform.SetSiblingIndex(0);
	}

	public void ShowConfigModal() {
		modalTitle.sprite = configTitle;
		ShowConfigContent();
		ShowModal(false);
	}

	public void ShowInventoryModal() {
		modalTitle.sprite = inventoryTitle;
		ShowInventoryContent();
		ShowModal(false);
	}

	public void ShowPlayerStatsModal() {
		modalTitle.sprite = playerStatsTitle;
		ShowPlayerStatsContent();
		UpdatePlayerStats();
		ShowModal(false);
	}

	public void ShowStoreModal() {
		modalTitle.sprite = storeTitle;
		ShowStoreContent();
		ShowModal(true);
	}

	public void CloseModal() {
		GUIManagerGameLobby.Instance.EnableGUIInteraction();
		modalPanel.Hide();
	}

	public void UpdatePlayerStats() {
		if(game != null) { 
		   game.UpdatePlayerStatsContent(playerStatsContent);
		}
	}

	public void TabClickLogics(int index) {
		SoundManager.Instance.PlaySelector();

		if (index == 1) {
			tab1.transform.SetSiblingIndex(2);
			tab2.transform.SetSiblingIndex(0);
			storeContentCurrencies.DisableContent ();
			storeContentSpells.EnableContent ();
		} else if (index == 2) {
			tab1.transform.SetSiblingIndex(0);
			tab2.transform.SetSiblingIndex(2);
			storeContentSpells.DisableContent ();
			storeContentCurrencies.EnableContent ();
		}
	}

	public int GetTotalByStoreItemType(StoreItemType type) {
		if(type.Equals(StoreItemType.Spell)) {
			return TotalSpellItems;
		}
		if(type.Equals(StoreItemType.Currency)) {
			return TotalCurrencyItems;
		}

		return 0;
	}

	private void ShowModal(bool showTabs) {
		GUIManagerGameLobby.Instance.DisableGUIInteraction();

		if (showTabs) {
			ShowStoreTabs();
		} else {
			HideStoreTabs();
		}

		modalPanel.Show();
	}

	private void ShowConfigContent() {
		configContentPanel.gameObject.SetActive(true);
		inventoryContentPanel.gameObject.SetActive(false);
		playerStatsContentPanel.gameObject.SetActive(false);
		storeContentPanel.gameObject.SetActive(false);
	}
		
	private void ShowInventoryContent() {
		configContentPanel.gameObject.SetActive(false);
		inventoryContentPanel.gameObject.SetActive(true);
		playerStatsContentPanel.gameObject.SetActive(false);
		storeContentPanel.gameObject.SetActive(false);
	}

	private void ShowPlayerStatsContent() {
		configContentPanel.gameObject.SetActive(false);
		inventoryContentPanel.gameObject.SetActive(false);
		playerStatsContentPanel.gameObject.SetActive(true);
		storeContentPanel.gameObject.SetActive(false);
	}

	private void ShowStoreContent() {
		configContentPanel.gameObject.SetActive(false);

		inventoryContentPanel.gameObject.SetActive(false);

		playerStatsContentPanel.gameObject.SetActive(false);

		storeContentPanel.gameObject.SetActive(true);

		storeContentSpells.ResetSpellShopContents();
		storeContentSpells.gameObject.SetActive(true);

		storeContentCurrencies.ResetSpellShopContents();
		storeContentCurrencies.gameObject.SetActive(false);
	}

	private void ShowStoreTabs() {
		tab1.gameObject.SetActive(true);
		tab2.gameObject.SetActive(true);
	}

	private void HideStoreTabs() {
		tab1.gameObject.SetActive(false);
		tab2.gameObject.SetActive(false);
	}

	private void SetupGameStore() {
		if(storeContentSpells && storeContentCurrencies) {
			int indexSpells = 0;
			int indexCurrencies = 0;

			foreach(StoreItemView item in storeItemManager.Items) {
				if(ITEM_TYPE_CURRENCY.Equals(item.Type)) {
					storeContentCurrencies.AddNewItem(item.Name, item.Description, item.Price, item.Image, currencyIcon, StoreItemType.Currency, indexCurrencies, 100, 0);
					indexCurrencies++;
				}
				if(ITEM_TYPE_SPELL.Equals(item.Type)) {
					storeContentSpells.AddNewItem(item.Name, item.Description, item.Price, item.Image, spellIcon, StoreItemType.Spell, indexSpells, 100, 0);
					indexSpells++;
				}
			}
		}
	}
}