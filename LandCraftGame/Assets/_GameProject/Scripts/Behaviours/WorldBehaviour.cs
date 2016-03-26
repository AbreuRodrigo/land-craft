using UnityEngine;
using System.Collections;

public class WorldBehaviour : MonoBehaviour {
	public static WorldBehaviour Instance;

	//Components
	public ViewBehaviour MyView;
	public ViewBehaviour OtherView;
	public GridGenerator GridGenerator;
	public GoalGenerator GoalGenerator;
	public LandInstantiator LandInstantiator;
	public LandAttractor LandAttractor;

	public CoreController Game { get; set; }
	public GamePlayController GamePlay { 
		get { return (GamePlayController) Game; }
	}
	public GameFreeModeController GameFreeMode {
		get { return (GameFreeModeController) Game; }
	}

	public int landEvents;

	public GridBehaviour MyGrid {
		get { return GridGenerator.MyGrid; }
		set { GridGenerator.MyGrid = value; }
	}

	public GridBehaviour OtherGrid {
		get { return GridGenerator.OtherGrid; }
		set { GridGenerator.OtherGrid = value; }
	}

	public CellBehaviour[] OtherGridCells {
		get { return OtherGrid.cells; }
	}

	public CellBehaviour[] MyGridCells {
		get { return MyGrid.cells; }
	}

	public int Size {
		get { return MyGrid.Size; }
	}

	public int GridSize {
		get { return MyGrid.cells.Length; }
	}
	
	public bool IsShaking { get; set; }
	
	public Animator myAnimator;

	void Awake() {
		if(Instance == null) {
			Instance = this;
		}

		Game = GameObject.FindObjectOfType<CoreController>();
	}

	public void TestCriterias() {
		if(Game != null && MyGrid != null) {
			if(OtherGrid != null && Game.StateManager.IsGamePlayState) {
				GamePlay.TestStageGoalCriterias(MyGrid, OtherGrid);
			}/*else if(Game.StateManager.IsGameFreeMode) {
				GameFreeMode.TestStageGameOver();
			}*/
		}
	}

	public void DoWorldShake() {
		if(!IsShaking && myAnimator != null && myAnimator.enabled) {
			SoundManager.Instance.PlayEarthShake();
			
			myAnimator.Play("WorldShake");
		}
	}
		
	public void StartShaking() {
		IsShaking = true;
	}
	
	public void StopShaking() {
		IsShaking = false;
	}

	public bool StillHasEmptyCells() {
		int emptyCells = 0;

		if(MyGridCells != null) {
			foreach(CellBehaviour cell in MyGridCells) {
				if(cell != null && cell.MyLand == null) {
					emptyCells++;
				}
			}
		} else {
			emptyCells = 1;
		}

		return emptyCells > 0;
	}
	
	public void ResetCellFromMyGridByIndex(int index) {
		if(RetrieveCellFromMyGridByIndex(index) != null) {
			RetrieveCellFromMyGridByIndex(index).Value = 0;
		}
	}

	public void FoldMyView() {
		if(MyView != null) {
			MyView.Fold();
		}
	}

	public void UnfoldMyView() {
		if(MyView != null) {
			MyView.Unfold();
		}
	}

	public void FoldOtherView() {
		if(OtherView != null) {
			OtherView.Fold();
		}
	}

	public void UnfoldOtherView() {
		if(OtherView != null) {
			OtherView.Unfold();
		}
	}

	public void AddLandEvent() {
		if(Game.StateManager.IsGamePlayState) {
			landEvents++;
		}
	}

	public void RemoveLandEvent() {
		if (Game.StateManager.IsGamePlayState && HasLandEventsOnGoing()) {
			landEvents--;
		}
	}

	public bool HasLandEventsOnGoing() {
		return landEvents > 0;
	}

	private CellBehaviour RetrieveCellFromMyGridByIndex(int index) {
		if(index >= 0 && index < MyGridCells.Length) {
			return MyGridCells[index];
		}

		return null;
	}
}