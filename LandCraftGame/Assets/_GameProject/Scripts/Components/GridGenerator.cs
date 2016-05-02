using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridGenerator : MonoBehaviour {
	private const int MIN_GRID_SIZE = 2;
	private const int MAX_GRID_SIZE = 6;

	public bool gridSizeByCurrentStage;
	public int gridSize;

	public GameObject grid2x2Prefab;
	public GameObject grid3x3Prefab;
	public GameObject grid4x4Prefab;
	public GameObject grid5x5Prefab;
	public GameObject grid6x6Prefab;

	public GridBehaviour MyGrid { get; set; }
	public GridBehaviour OtherGrid { get; set; }

	public Transform MyView;
	public Transform OtherView;

	private GameObject myViewGrid;
	private GameObject otherViewGrid;

	void Awake() {
		if(gridSizeByCurrentStage) {
			DefineGridForGamePlay();
		} else {
			DefineGridForGameFreeMode();
		}

		if(gridSize < MIN_GRID_SIZE) {
			gridSize = MIN_GRID_SIZE;
		}
		if(gridSize > MAX_GRID_SIZE) {
			gridSize = MAX_GRID_SIZE;
		}
	}

	private void DefineGridForGamePlay() {
		gridSize = GridSizeByCurrentStage();

		myViewGrid = (GameObject)GameObject.Instantiate(PickGridByGridSize());
		otherViewGrid = (GameObject)GameObject.Instantiate(PickGridByGridSize());

		if(myViewGrid != null) {
			myViewGrid.transform.parent = MyView;
			myViewGrid.transform.localPosition = Vector3.zero;
			MyGrid = myViewGrid.GetComponent<GridBehaviour>();
		}
		
		if(otherViewGrid != null) {
			otherViewGrid.transform.parent = OtherView;
			otherViewGrid.transform.localPosition = Vector3.zero;
			OtherGrid = otherViewGrid.GetComponent<GridBehaviour>();
		}
	}

	private void DefineGridForGameFreeMode() {
		myViewGrid = (GameObject)GameObject.Instantiate(PickGridByGridSize());

		if(myViewGrid != null) {
			myViewGrid.transform.parent = MyView;
			myViewGrid.transform.localPosition = Vector3.zero;
			MyGrid = myViewGrid.GetComponent<GridBehaviour>();
		}
	}

	private GameObject PickGridByGridSize() {
		switch(gridSize) {
		default:
		case 2:
			return grid2x2Prefab;
			break;
		case 3:
			return grid3x3Prefab;
			break;
		case 4: 
			return grid4x4Prefab;
			break;
		case 5:
			return grid5x5Prefab;
			break;
		case 6:
			return grid6x6Prefab;
			break;		
		}
	}

	private int GridSizeByCurrentStage() {
		int stage = PlayerPrefsManager.ClickedStage;

		if(stage >= 1 && stage <= 4) {
			return 2;
		} else if(stage >= 5 && stage <= 13) {
			return 3;
		} else if(stage >= 14 && stage <= 30) {
			return 4;
		} else if(stage >= 31 && stage <= 56) {
			return 5;
		} else {
			return 6;
		}
	}
}