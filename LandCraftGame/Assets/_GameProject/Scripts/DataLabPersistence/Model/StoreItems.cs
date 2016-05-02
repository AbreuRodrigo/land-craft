using UnityEngine;
using System.Collections;

public class StoreItems : ServerObject {
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
		if (IsDataObjectValid()) {
			this.landBreakers = (int)dataObject.Fields ["landBreakers"];
			this.landDowngraders = (int)dataObject.Fields ["landDowngraders"];
			this.landUpgraders = (int)dataObject.Fields ["landUpgraders"];
			this.timeFreezers = (int)dataObject.Fields ["timeFreezers"];
			this.seedsOfGrass = (int)dataObject.Fields ["seedsOfGrass"];
			this.seedsOfBush = (int)dataObject.Fields ["seedsOfBush"];
			this.seedsOfTrees = (int)dataObject.Fields ["seedsOfTrees"];
			this.seedsOfMana = (int)dataObject.Fields ["seedsOfMana"];
		}
	}
}
