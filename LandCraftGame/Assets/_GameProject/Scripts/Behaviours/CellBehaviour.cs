using UnityEngine;
using System.Collections;

public class CellBehaviour : IsometricObject {
	public int index;

	public int landValue;
	public int front;
	public int back;
	public int left;
	public int right;

	public LandBehaviour MyLand { get; set; }

	private int size;
	private int parentGridLimit = -1;

	public int Value {
		get { return landValue; } 
		set { landValue = value; }
	}
	public int Front {
		get { return front; }
	}
	public int Back { 
		get { return back; } 
	}
	public int Left { 
		get { return left; } 
	}
	public int Right {
		get { return right; } 
	}

	void Awake() {
		this.InitializeNeighbors();
	}

	void InitializeNeighbors() {
		size = transform.GetComponentInParent<GridBehaviour>().Size;
		parentGridLimit = transform.GetComponentInParent<GridBehaviour>().GridSize;
		
		front = index - 1;
		back = index + 1;
		left = index - size;
		right = index + size;

		if(front < 0 || ((front+1) % size == 0)) {
			front = -1;
		}
		if((back >= parentGridLimit) || back % size == 0) {
			back = -1;
		}
		if(left < 0) {
			left = -1;
		}
		if(right >= parentGridLimit) {
			right = -1;
		}
	}
}