using UnityEngine;
using System.Collections;

public class AssetManager : MonoBehaviour {
	public static AssetManager Instance;

	public LandBehaviour[] wasteLands;
	public LandBehaviour[] lawnLands;
	public LandBehaviour[] bushLands;
	public LandBehaviour[] treeLands;
	public LandBehaviour[] lifeTreeLands;

	void Awake () {
		if(Instance == null) {
			Instance = this;
		}
	}

	public LandBehaviour GetLandPrefabByType(LandType type) {
		LandBehaviour landPrefab = null;

		switch (type) {
			case LandType.Waste:
				landPrefab = GetWasteLandPrefab();
			break;
			case LandType.Lawn:
				landPrefab = GetLawnLandPrefab();
			break;
			case LandType.Bush:
				landPrefab = GetBushLandPrefab();
			break;
			case LandType.Trees:
				landPrefab = GetTreeLandPrefab();
			break;
			case LandType.ManaTree:
				landPrefab = GetLifeTreeLandPrefab();
			break;
		}

		return landPrefab;
	}

	private LandBehaviour GetWasteLandPrefab() {
		return wasteLands[RandomIntFromTo(0, wasteLands.Length)];
	}

	private LandBehaviour GetLawnLandPrefab() {
		return lawnLands[RandomIntFromTo(0, lawnLands.Length)];
	}

	private LandBehaviour GetBushLandPrefab() {
		return bushLands[RandomIntFromTo(0, bushLands.Length)];
	}

	private LandBehaviour GetTreeLandPrefab() {
		return treeLands[RandomIntFromTo(0, treeLands.Length)];
	}

	private LandBehaviour GetLifeTreeLandPrefab() {
		return lifeTreeLands[RandomIntFromTo(0, lifeTreeLands.Length)];
	}

	private int RandomIntFromTo(int from, int to) {
		Random.seed = (int)System.DateTime.Now.Ticks;

		return Random.Range(from, to);
	}

	public GameObject InstantiateMagicParticle(Vector3 point) {
		return null;
	}
}
