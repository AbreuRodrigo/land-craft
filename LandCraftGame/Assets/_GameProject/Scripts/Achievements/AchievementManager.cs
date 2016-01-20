using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour {
	public AchievementContainer container;
	public Sprite levelUpIcon;

	private Dictionary<AchievementEvents, System.Action> EventActions = 
		new Dictionary<AchievementEvents, System.Action>();

	void Start() {
		InitializeEventDictionary();

		if(container == null) {
			container = FindObjectOfType<AchievementContainer>();
		}
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.A)) {
			StartAchievementEvent(AchievementEvents.LEVELUP);
		}
	}

	//EXTERNAL CALL - Exposition
	public void StartAchievementEvent(AchievementEvents aEvent) {
		EventActions[aEvent].Invoke();
	}

	//INTERNAL
	private void ShowLevelUpAchievement() {
		ShowAchievement(levelUpIcon, "Level Up!!");
	}

	private void ShowAchievement(Sprite achievementIcon, string text) {
		if(container != null) {
			container.gameObject.SetActive(true);
			container.enabled = true;
			container.Show(achievementIcon, text);
		}
	}

	private void InitializeEventDictionary() {
		EventActions.Add(AchievementEvents.LEVELUP, ShowLevelUpAchievement);
	}
}