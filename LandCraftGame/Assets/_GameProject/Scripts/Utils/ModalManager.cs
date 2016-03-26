using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ModalManager : MonoBehaviour {
	public static ModalManager Instance;

	public GameLobbyController game;

	[Header("Contents")]
	public PlayerStatsContent playerStatsContent;
	public StoreContent storeContent;

	[Header("Components")]
	public ModalPanelFX modalPanel;

	[Header("Images")]
	public Image modalTitle;

	[Header("Sprites")]
	public Sprite inventoryTitle;
	public Sprite playerStatsTitle;
	public Sprite storeTitle;

	[Header("ModalContents")]
	public GameObject inventoryContentPanel;
	public GameObject playerStatsContentPanel;
	public GameObject storeContentPanel;

	private StoreItemManager storeItemManager;

	public int TotalItems { get; set; }

	void Awake() {
		if(Instance == null) {
			Instance = this;
		}

		storeItemManager = FindObjectOfType<StoreItemManager>();
	}

	void Start() {
		if (storeItemManager != null) {
			TotalItems = storeItemManager.items.Count;
			SetupGameStore ();
		}
	}

	public void ShowInventoryModal() {
		modalTitle.sprite = inventoryTitle;
		ShowInventoryContent();
		ShowModal();
	}

	public void ShowPlayerStatsModal() {
		modalTitle.sprite = playerStatsTitle;
		ShowPlayerStatsContent();
		UpdatePlayerStats();
		ShowModal();
	}

	public void ShowStoreModal() {
		modalTitle.sprite = storeTitle;
		ShowStoreContent();
		ShowModal();
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

	private void ShowModal() {
		GUIManagerGameLobby.Instance.DisableGUIInteraction();
		modalPanel.Show();
	}
		
	private void ShowInventoryContent() {
		inventoryContentPanel.gameObject.SetActive(true);
		playerStatsContentPanel.gameObject.SetActive(false);
		storeContentPanel.gameObject.SetActive(false);
	}

	private void ShowPlayerStatsContent() {
		inventoryContentPanel.gameObject.SetActive(false);
		playerStatsContentPanel.gameObject.SetActive(true);
		storeContentPanel.gameObject.SetActive(false);
	}

	private void ShowStoreContent() {
		inventoryContentPanel.gameObject.SetActive(false);
		playerStatsContentPanel.gameObject.SetActive(false);
		storeContentPanel.gameObject.SetActive(true);
		storeContent.ResetStoreContents();
	}

	private void SetupGameStore() {
		if(storeContent) {
			int index = 0;

			foreach(StoreItemView item in storeItemManager.items) {
				storeContent.AddNewItem (item.Name, item.Description, item.Price, item.Image, index, 100, 0);
				index++;
			}
		}
	}
}