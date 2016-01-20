using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceFXManager : MonoBehaviour {
	public Sprite woodIcon;
	public Sprite manaIcon;
	public Sprite stoneIcon;
	public Sprite goldIcon;

	public Transform moreResourceFxParent;
	public GameObject moreResourcePrefab;

	private class ResourceRelation {
		public ResourceType resource;
		public Sprite icon;
		public int value;

		public ResourceRelation(ResourceType resource, Sprite icon, int value) {
			this.resource = resource;
			this.icon = icon;
			this.value = value;
		}
	}

	private Dictionary<LandType, ResourceRelation[]> relationByLandType = 
		new Dictionary<LandType, ResourceRelation[]>();

	void Start() {
		InitializeDictionary();
	}		

	private void InitializeDictionary() {
		ResourceRelation[] treesRelations = new ResourceRelation[] {
			new ResourceRelation(ResourceType.Wood, woodIcon, 5)
		};
		ResourceRelation[] manaTreeRelations = new ResourceRelation[] {
			new ResourceRelation(ResourceType.Wood, woodIcon, 10),
			new ResourceRelation(ResourceType.Mana, manaIcon, 5)
		};
		ResourceRelation[] mountainRelations = new ResourceRelation[] {
			new ResourceRelation(ResourceType.Stone, stoneIcon, 10)
		};
		ResourceRelation[] goldMineRelations = new ResourceRelation[] {
			new ResourceRelation(ResourceType.Gold, goldIcon, 5)
		};
		
		relationByLandType.Add(LandType.Trees, treesRelations);
		relationByLandType.Add(LandType.ManaTree, manaTreeRelations);
		relationByLandType.Add(LandType.Mountain, mountainRelations);
		relationByLandType.Add(LandType.GoldMine, goldMineRelations);
	}

	public void InstantiateMoreResource(Vector3 origin, LandType landType) {
		StartCoroutine(StartResourceHarvestingAnimations(origin, landType));
	}

	IEnumerator StartResourceHarvestingAnimations(Vector3 origin, LandType landType) {
		ResourceRelation[] resources = relationByLandType[landType];
		
		if (resources != null) {
			foreach(ResourceRelation relation in resources) {
				if (moreResourcePrefab != null && moreResourceFxParent != null) {
					GameObject moreResourceObj = 
						(GameObject)Instantiate(moreResourcePrefab, origin, moreResourcePrefab.transform.rotation);
					
					if (moreResourceObj != null) {
						MoreResourceFX moreResourceFX = moreResourceObj.GetComponent<MoreResourceFX>();
						
						if (moreResourceFX != null) {
							moreResourceFX.transform.parent = moreResourceFxParent;
							moreResourceFX.Icon = relation.icon;
							moreResourceFX.Value = relation.value;
						}

						GUIManagerGameFreeMode.Instance.SumUpResourceByType(relation.resource, relation.value);
					}

					yield return new WaitForSeconds(0.5f);
				}
			}
		}
	}
}