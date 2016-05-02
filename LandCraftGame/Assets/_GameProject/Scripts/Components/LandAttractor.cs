using UnityEngine;
using System.Collections;

public class LandAttractor : MonoBehaviour {

	public void DoAttractionLogics(LandBehaviour land) {
		if(WorldBehaviour.Instance.Game.StateManager.IsGamePlayState) {			
			TestNeighborsAttractionByType (land);
			
			if (land.CausedAttraction) {
				if (!land.IsUpgrading) {
					EndAttraction (land);
					land.UpgradeToNextType ();
				}
			} else {
				WorldBehaviour.Instance.RemoveLandEvent ();
			}
		}
	}

	private void DoMyAttractionMove(LandBehaviour other, Vector3 targetPosition) {
		if(WorldBehaviour.Instance.Game.StateManager.IsGamePlayState) {
			if (!other.InAttractionProcess && !other.CausedAttraction) {
				other.InAttractionProcess = true;
				StartCoroutine(StartMyAttractionProcess(other, targetPosition));
			}
		}
	}

	private void EndAttraction(LandBehaviour land) {
		if(WorldBehaviour.Instance.Game.StateManager.IsGamePlayState) {
			StartCoroutine(EndLandAttractionRoutine(land));
		}
	}

	private LandBehaviour RetrieveLandAtCell(int cell) {
		if(cell >= 0) {
			return WorldBehaviour.Instance.MyGridCells[cell].MyLand;
		}
		return null;
	}

	private void TestTypeAttraction(LandBehaviour land, LandBehaviour other) {
		if(WorldBehaviour.Instance.Game.StateManager.IsGamePlayState) {
			if (other != null && !other.InAttractionProcess && !other.CausedAttraction && land.type.Equals(other.type)) {
				land.CausedAttraction = true;
				DoMyAttractionMove(other, land.transform.position);
			}
		}
	}

	private void TestNeighborsAttractionByType(LandBehaviour land) {
		if(WorldBehaviour.Instance.Game.StateManager.IsGamePlayState) {
			TestTypeAttraction(land, RetrieveLandAtCell(land.MyCell.Left));
			TestTypeAttraction(land, RetrieveLandAtCell(land.MyCell.Right));
			TestTypeAttraction(land, RetrieveLandAtCell(land.MyCell.Back));
			TestTypeAttraction(land, RetrieveLandAtCell(land.MyCell.Front));
		}
	}

	IEnumerator EndLandAttractionRoutine(LandBehaviour land) {
		yield return new WaitForSeconds(0.25f);
		
		SoundManager.Instance.PlayAttraction();
		
		land.CausedAttraction = false;
	}

	IEnumerator StartMyAttractionProcess(LandBehaviour other, Vector3 targetPosition) {
		yield return new WaitForSeconds(0.25f);

		if (other != null && targetPosition != null) {
			other.myAnimator.Play("LandFadingScalingOut");
					
			Transform myObjs = other.transform.FindChild("Objects");
		
			if (myObjs != null) {
				myObjs.gameObject.SetActive(false);
			}
		
			float starTime = Time.time;
		
			while(other.transform.position != targetPosition && other.InAttractionProcess) {
				other.transform.position = Vector3.Lerp(other.transform.position, targetPosition, (Time.time - starTime) * 5f);
				yield return new WaitForSeconds(0.01f);
			}
		
			if(other.MyCell != null) {
				other.MyCell.gameObject.SetActive(true);
				other.MyCell.MyLand = null;
				other.MyCell.Value = 0;

				WorldBehaviour.Instance.UpdateGridSetupStatsForPersistence(other.MyCell.index, 0);
			}

			Destroy(other.gameObject, 0.2f);
		}
	}
}