using UnityEngine;
using System.Collections;

public class GridBehaviour : MonoBehaviour {
	public CellBehaviour[] cells;

	private int size;
	private int gridSize;

	public int Size { 
		get { return size; } 
	}
	public int GridSize { 
		get { return gridSize; } 
	}

	void Awake() {
		size = (int)Mathf.Sqrt(cells.Length);
		gridSize = size * size;
	}
}