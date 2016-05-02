using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LandInstantiator : MonoBehaviour {
	public Transform myLandContainer;
	public Transform otherLandContainer;

	public delegate void AfterEvent();

	public void InstantiateLand(LandType type, Vector3 position, CellBehaviour choosenCell, bool isUpgrading = false, AfterEvent afterEvent = null) {
		StartCoroutine(CreateLand(type, position, myLandContainer, choosenCell, isUpgrading, afterEvent));
	}

	public void InstantiateWasteLand(Vector3 position, CellBehaviour choosenCell, bool isUpgrading = false, AfterEvent afterEvent = null) {
		InstantiateLand(LandType.Waste, position, choosenCell, isUpgrading, afterEvent);
	}

	/*public void InstantiateLawnLand(Vector3 position, CellBehaviour choosenCell, bool isUpgrading = false, AfterEvent afterEvent = null) {
		InstantiateLand(LandType.Lawn, position, choosenCell, isUpgrading, afterEvent);
	}

	public void InstantiateBushLand(Vector3 position, CellBehaviour choosenCell, bool isUpgrading = false, AfterEvent afterEvent = null) {
		InstantiateLand(LandType.Bush, position, choosenCell, isUpgrading, afterEvent);
	}

	public void InstantiateTreesLand(Vector3 position, CellBehaviour choosenCell, bool isUpgrading = false, AfterEvent afterEvent = null) {
		InstantiateLand(LandType.Trees, position, choosenCell, isUpgrading, afterEvent);
	}

	public void InstantiateLifeTreeLand(Vector3 position, CellBehaviour choosenCell, bool isUpgrading = false, AfterEvent afterEvent = null) {
		InstantiateLand(LandType.ManaTree, position, choosenCell, isUpgrading, afterEvent);
	}*/

	IEnumerator CreateLand(LandType type, Vector3 position, Transform parent, CellBehaviour selectedCell, bool isUpgrading = false, AfterEvent afterEvent = null) {
		if(!isUpgrading) {
			position.y = 5f;
		}

		yield return new WaitForSeconds(0.2f);

		GameObject land = (GameObject)Instantiate(
			AssetManager.Instance.GetLandPrefabByType(type).gameObject, position, Quaternion.identity
		);

		if(land != null) {
			LandBehaviour lb = land.GetComponent<LandBehaviour>();
			lb.MyCell = selectedCell;
			lb.MyCell.MyLand = lb;
			lb.MyCell.Value = (int)type;
			lb.transform.parent = parent;
			lb.IsUpgrading = isUpgrading;
			lb.IsFalling = !isUpgrading;

			if(WorldBehaviour.Instance.Game.StateManager.IsGamePlayState) {
				WorldBehaviour.Instance.UpdateGridSetupStatsForPersistence(selectedCell.index, (int)type);

				if(isUpgrading) {
					WorldBehaviour.Instance.GamePlay.LandUpgradeLogicsGamePlay(lb);
				}
			}

			if(afterEvent != null) {
				afterEvent.Invoke();
			}
		}
	}
}