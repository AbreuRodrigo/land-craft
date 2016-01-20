using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ModalManager : MonoBehaviour {
	public static ModalManager Instance;

	public GameLobbyController game;
	public PlayerStatsContent playerStatsContent;

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

	void Awake() {
		if(Instance == null) {
			Instance = this;
		}
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			CloseModal();
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
	}
}