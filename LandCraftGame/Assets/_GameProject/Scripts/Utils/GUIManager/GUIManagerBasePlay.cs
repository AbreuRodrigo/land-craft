using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GUIManagerBasePlay : GUIManagerBase {
	public UIResourceCounter WoodCounter;
	public UIResourceCounter ManaCounter;
	public UIResourceCounter StoneCounter;
	public UIResourceCounter GoldCounter;

	protected virtual void Start() {
		InitializeCouterByResourceType();
		base.Start();
	}

	private Dictionary<ResourceType, UIResourceCounter> CouterByResourceType = 
		new Dictionary<ResourceType, UIResourceCounter>();

	protected override void StartMyStateUIEvent(GameState state) {
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
			GoldCounter.FadeIn();
		}
	}

	public void SumUpResourceByType(ResourceType type, int amount) {
		CouterByResourceType[type].AddResource(amount);
	}

	protected void InitializeCouterByResourceType() {
		CouterByResourceType.Add(ResourceType.Wood, WoodCounter);
		CouterByResourceType.Add(ResourceType.Mana, ManaCounter);
		CouterByResourceType.Add(ResourceType.Stone, StoneCounter);
		CouterByResourceType.Add(ResourceType.Gold, GoldCounter);
	}
}