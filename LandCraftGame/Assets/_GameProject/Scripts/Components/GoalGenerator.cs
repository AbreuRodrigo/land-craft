using UnityEngine;
using System.Collections;

public class GoalGenerator : MonoBehaviour {
	public bool active;
				
	private int[] grid;
	private int size;
	private int gridSize;

	void Start() {
		if(active) {
			StartCoroutine("GenerateGoal");
		}
	}

	private void TestNeighborCells(int index) {
		int front = index - 1;
		int back = index + 1;
		int left = index - size;
		int right = index + size;
		
		bool hadFront = false;
		bool hadBack = false;
		bool hadLeft = false;
		bool hadRight = false;
		
		int indexValue = grid[index];
		
		if(front >= 0 && ((front+1) % size != 0)) {
			if(indexValue == grid[front]) {
				hadFront = true;
				grid[front] = 0;
			}
		}
		if((back <= gridSize - 1) && back % size != 0) {
			if(indexValue == grid[back]) {
				hadBack = true;
				grid[back] = 0;
			}
		}
		if(left >= 0) {
			if(indexValue == grid[left]) {
				hadLeft = true;
				grid[left] = 0;
			}
		}
		if(right < gridSize) {
			if(indexValue == grid[right]) {
				hadRight = true;
				grid[right] = 0;
			}
		}		
		if(hadFront || hadBack || hadLeft || hadRight) {
			grid[index] += 1;
			
			TestNeighborCells(index);
		}
	}
	
	private void PlayLand(int targetCell) {
		if(grid[targetCell] == 0) {
			grid[targetCell] = 1;
			
			TestNeighborCells(targetCell);
		}
	}
	
	private bool TestGridOver() {
		for(int i = 0; i < gridSize; i++) {
			if(i == gridSize && grid[i] != 0) {
				return true;
			}
		}
		
		return false;
	}
	
	IEnumerator GenerateGoal() {
		yield return new WaitForSeconds(0);

		GridGenerator gridGenerator = GameObject.FindObjectOfType<GridGenerator>();

		size = gridGenerator.gridSize;
		gridSize = gridGenerator.MyGrid.cells.Length;

		grid = new int[gridSize];
		
		Random.seed = (int)System.DateTime.Now.Ticks;
		
		int chosenCell;
		
		int diff = (int)WorldBehaviour.Instance.Game.Difficulty;
		int stage = WorldBehaviour.Instance.Game.currentStage;
		int steps = (int)(gridSize * 0.5f) + stage;//Random.Range((int)(gridSize * 0.5f), gridSize + stage);
		int counter = 0;
		int maxIteration = 0;

		WorldBehaviour.Instance.GamePlay.DefineLevelSteps(steps);
		
		while(counter < steps) {
			chosenCell = Random.Range(0, gridSize);
			
			if(grid[chosenCell] == 0) {
				PlayLand(chosenCell);
				counter++;
			}
			
			maxIteration++;
			
			if(TestGridOver() || (maxIteration >= steps * 2)) {
				break;
			}
		}
				
		for(int i = 0; i < grid.Length; i++) {
			InputManager.Instance.SelectedCell = WorldBehaviour.Instance.OtherGridCells[i];
			Vector3 p = InputManager.Instance.SelectedCell.transform.position;
			
			if(grid[i] == 1) {
				WorldBehaviour.Instance.LandInstantiator.InstantiateWasteGoalLand(p);
			}else if(grid[i] == 2) {
				WorldBehaviour.Instance.LandInstantiator.InstantiateLawnGoalLand(p);
			}else if(grid[i] == 3) {
				WorldBehaviour.Instance.LandInstantiator.InstantiateBushGoalLand(p);
			}else if(grid[i] == 4) {
				WorldBehaviour.Instance.LandInstantiator.InstantiateTreesGoalLand(p);
			}else if(grid[i] == 5) {
				WorldBehaviour.Instance.LandInstantiator.InstantiateLifeTreeGoalLand(p);
			}

			InputManager.Instance.SelectedCell.Value = grid[i];
		}
		
		foreach(CellBehaviour c in WorldBehaviour.Instance.OtherGridCells) {
			if(c.Value != 0) {
				c.gameObject.SetActive(false);
			}
		}
	}
}