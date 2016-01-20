using UnityEngine;
using System.Collections;

public class AIAgent : MonoBehaviour {
	public bool active;

	private int[] grid;
	private int size;
	private int gridSize;

	void Start() {
		if(active) {
			StartCoroutine("StartAI");
		}
	}

	IEnumerator StartAI() {
		yield return new WaitForSeconds(5);

		gridSize = WorldBehaviour.Instance.GridSize;

		Random.seed = (int)System.DateTime.Now.Ticks;

		int chosenCell = -1;

		while(WorldBehaviour.Instance.StillHasEmptyCells()) {
			yield return new WaitForSeconds(0.25f);

			if(chosenCell == -1 || (Random.Range(0, 100) >= 50)) {
				chosenCell = Random.Range(0, gridSize);
			}

			InputManager.Instance.DoCellPressFromOutside(chosenCell);
		}
	}
}