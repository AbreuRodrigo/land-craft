using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	public static InputManager Instance;

	public SelectorBehaviour Selector;
	public SelectorBehaviour HarvestingSelector;
	public CellBehaviour SelectedCell;
	private CellBehaviour TargetCell;
	private LandBehaviour GlobalTargetLand;

	private RaycastHit inputRayHit;
	private Ray inputRay;

	void Awake () {
		if(Instance == null) {
			Instance = this;
		}

		Input.multiTouchEnabled = true;
	}

	void Update() {
		InputLogics();
	}

	//LOGICS
	private void InputLogics() {
		if((IsGamePlayState() || IsGameFreeMode()) && IsMyGameView()) {
			if(Application.platform.Equals(RuntimePlatform.WindowsEditor)) {
				if(Input.GetMouseButtonDown(0)) {
					InputSelectionLogicsByPoint(Input.mousePosition);
				}
			} else if(Application.platform.Equals(RuntimePlatform.Android)) {
				if(Input.touches != null && Input.touches.Length == 1) {
					foreach(Touch t0 in Input.touches) {
						if(t0.phase.Equals(TouchPhase.Began)) {
							InputSelectionLogicsByPoint(t0.position);
						}
					}
				}
			}
		}
	}

	private void InputSelectionLogicsByPoint(Vector3 inputPoint) {
		inputRay = Camera.main.ScreenPointToRay(inputPoint);
		
		if(Physics.Raycast(inputRay, out inputRayHit)) {
			IsometricObject targetIsoObj = inputRayHit.transform.gameObject.GetComponent<IsometricObject>();

			if(targetIsoObj != null) { 
				if(targetIsoObj.isoType.Equals(IsoType.Cell)) {
					DoInputSelectionLogicsByPointForCell(targetIsoObj);
				} else if(targetIsoObj.isoType.Equals(IsoType.Land)) {
					DoInputSelectionLogicsByPointForLand(targetIsoObj);
				}
			}
		} else {
			ClearGridSelection();
			ClearHarvestingSelection();
		}
	}

	private void DoInputSelectionLogicsByPointForCell(IsometricObject targetIsoObj) {
		ClearHarvestingSelection();

		TargetCell = targetIsoObj.GetComponent<CellBehaviour>();
		
		if(TargetCell != null) {
			if((SelectedCell != TargetCell) || (SelectedCell == TargetCell && !SelectedCell.selected)) {
				DoInputSelectionLogics(targetIsoObj.transform.position);
			} else {
				DoCellPressedLogics();
			}
		}
	}

	private void DoInputSelectionLogicsByPointForLand(IsometricObject targetIsoObj) {
		if (targetIsoObj != null) {
			ClearGridSelection();

			LandBehaviour targetLand = (LandBehaviour)targetIsoObj;

			if (targetLand.hasResource) {
				if (targetLand.selectedToHarvest) {
					InstantiateHarvestingFX(targetLand.transform.position, targetLand.type);
					targetLand.HarvestMyResource();
					ClearHarvestingSelection();
					WorldBehaviour.Instance.LandAttractor.DoAttractionLogics(targetLand);
					SoundManager.Instance.PlayHarvesting();
				} else {
					ClearHarvestingSelection();
					targetLand.selectedToHarvest = true;
					DoHarvestSelectionLogics(targetIsoObj.transform.position);
					DefineGlobalTargetLand(ref targetLand);
				}
			}else {
				ClearHarvestingSelection();
			}
		}
	}

	//Setting cell as selected
	private void DoInputSelectionLogics(Vector3 targetPoint) {
		ClearGridSelection();

		if(TargetCell != null) {
			SelectedCell = TargetCell;
		}

		SelectedCell.selected = true;
		
		Selector.Reposition(targetPoint);

		SoundManager.Instance.PlaySelector();
	}

	private void DoHarvestSelectionLogics(Vector3 targetPoint) {
		HarvestingSelector.Reposition(targetPoint);

		SoundManager.Instance.PlaySelectorHarvesting();
	}

	public void DoCellPressFromOutside(int cellIndex) {
		if(cellIndex >= 0 && cellIndex < WorldBehaviour.Instance.GridSize) {
			CellBehaviour targetCell = WorldBehaviour.Instance.MyGridCells[cellIndex];

			if(targetCell != null && targetCell.MyLand == null) {
				DoInputSelectionLogicsByPointForCell(targetCell);
			}
		}
	}

	private void DoCellPressedLogics() {
		if(SelectedCell != null && SelectedCell.gameObject.activeInHierarchy && Selector.Visible && 
		   !WorldBehaviour.Instance.HasLandEventsOnGoing()) {
			Selector.Hide();

			WorldBehaviour.Instance.LandInstantiator.InstantiateWasteLand (
				SelectedCell.transform.position, SelectedCell, false, WorldBehaviour.Instance.TestCriterias
			);

			if(WorldBehaviour.Instance.Game.StateManager.IsGamePlayState) {
				WorldBehaviour.Instance.GamePlay.StageStapForward();
			}

			ClearGridSelection();
			SelectedCell.Deactivate();
		}
	}

	private void ClearGridSelection() {
		Selector.Hide();

		if(SelectedCell != null) {
			SelectedCell.selected = false;
		}
	}

	private void ClearHarvestingSelection() {
		HarvestingSelector.Hide();

		if(GlobalTargetLand != null) {
			GlobalTargetLand.selectedToHarvest = false;
			GlobalTargetLand = null;
		}
	}

	private void DefineGlobalTargetLand(ref LandBehaviour targetLand) {
		GlobalTargetLand = targetLand;
	}

	//EXTERNAL VALIDATIONS
	private bool IsGamePlayState() {
		return WorldBehaviour.Instance.Game.StateManager.IsGamePlayState;
	}

	private bool IsGameFreeMode() {
		return WorldBehaviour.Instance.Game.StateManager.IsGameFreeMode;
	}

	private bool IsMyGameView() {
		return WorldBehaviour.Instance.Game.IsMyGameView();
	}

	private void InstantiateHarvestingFX(Vector3 origin, LandType landType) {
		WorldBehaviour.Instance.GameFreeMode.HarvestTargetLand(origin, landType);
	}
}