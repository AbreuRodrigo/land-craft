using Parse;
using UnityEngine;
using System.Collections;

public class StoreItems : ParseModel {
	public int landBreakers = 0;
	public int landDowngraders = 0;
	public int landUpgraders = 0;
	public int timeFreezers = 0;
	public int seedsOfGrass = 0;
	public int seedsOfBush = 0;
	public int seedsOfTrees = 0;
	public int seedsOfMana = 0;

	protected override void OnBegin() {
	}
	
	protected override void OnLoaded() {
		this.landBreakers = parseObject.Get<int>("landBreakers");
		this.landDowngraders = parseObject.Get<int>("landDowngraders");
		this.landUpgraders = parseObject.Get<int>("landUpgraders");
		this.timeFreezers = parseObject.Get<int>("timeFreezers");
		this.seedsOfGrass = parseObject.Get<int>("seedsOfGrass");
		this.seedsOfBush = parseObject.Get<int>("seedsOfBush");
		this.seedsOfTrees = parseObject.Get<int>("seedsOfTrees");
		this.seedsOfMana = parseObject.Get<int>("seedsOfMana");
	}
}
