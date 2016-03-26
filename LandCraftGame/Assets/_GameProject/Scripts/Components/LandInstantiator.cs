using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LandInstantiator : MonoBehaviour {
	public Transform myLandContainer;
	public Transform otherLandContainer;

	public delegate void AfterEvent();

	//GOAL INSTANTIATION
	private void InstantiateGoalLand(LandType type, Vector3 position, CellBehaviour choosenCell, bool isUpgrading = false) {
		StartCoroutine(CreateLand(type, position, otherLandContainer, choosenCell, isUpgrading, null));
	}

	public void InstantiateWasteGoalLand(Vector3 position, bool isUpgrading = false) {
		InstantiateGoalLand(LandType.Waste, position, InputManager.Instance.SelectedCell, isUpgrading);
	}	

	public void InstantiateLawnGoalLand(Vector3 position, bool isUpgrading = false) {
		InstantiateGoalLand(LandType.Lawn, position, InputManager.Instance.SelectedCell, isUpgrading);
	}

	public void InstantiateBushGoalLand(Vector3 position, bool isUpgrading = false) {
		InstantiateGoalLand(LandType.Bush, position, InputManager.Instance.SelectedCell, isUpgrading);
	}
	
	public void InstantiateTreesGoalLand(Vector3 position, bool isUpgrading = false) {
		InstantiateGoalLand(LandType.Trees, position, InputManager.Instance.SelectedCell, isUpgrading);
	}
	
	public void InstantiateLifeTreeGoalLand(Vector3 position, bool isUpgrading = false) {
		InstantiateGoalLand(LandType.ManaTree, position, InputManager.Instance.SelectedCell, isUpgrading);
	}

	//LAND NORMAL INSTANTIATION
	public void InstantiateLand(LandType type, Vector3 position, CellBehaviour choosenCell, bool isUpgrading = false, AfterEvent afterEvent = null) {
		StartCoroutine(CreateLand(type, position, myLandContainer, choosenCell, isUpgrading, afterEvent));
	}

	public void InstantiateWasteLand(Vector3 position, CellBehaviour choosenCell, bool isUpgrading = false, AfterEvent afterEvent = null) {
		InstantiateLand(LandType.Waste, position, choosenCell, isUpgrading, afterEvent);
	}

	public void InstantiateLawnLand(Vector3 position, CellBehaviour choosenCell, bool isUpgrading = false, AfterEvent afterEvent = null) {
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
	}

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

			if(isUpgrading && WorldBehaviour.Instance.Game.StateManager.IsGameFreeMode) {
				WorldBehaviour.Instance.GameFreeMode.LandUpgradeLogicsFreeMode(lb);
			}

			if(lb.hasResource && WorldBehaviour.Instance.Game.StateManager.IsGameFreeMode) {
				GUIManagerGameFreeMode.Instance.ShowResourceCounterByType(lb.type);
			}

			if(afterEvent != null) {
				afterEvent.Invoke();
			}
		}
	}
}