using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LandEvolutionManager {
	private static LandEvolutionManager instance;
	public static LandEvolutionManager Instance {
		get {
			if(instance == null) {
				instance =  new LandEvolutionManager();
			}

			return instance;
		}	
	}

	private LandEvolutionManager() {}

	private Dictionary<LandType, LandType> evolutionNextType = new Dictionary<LandType, LandType>() {
		{LandType.Waste, LandType.Lawn},
		{LandType.Lawn, LandType.Bush},
		{LandType.Bush, LandType.Trees},
		{LandType.Trees, LandType.ManaTree}
	};

	public LandType NextType(LandType currentType) {
		return evolutionNextType[currentType];
	}
}