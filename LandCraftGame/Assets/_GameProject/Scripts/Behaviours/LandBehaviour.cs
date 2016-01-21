using UnityEngine;
using System.Collections;

public class LandBehaviour : IsometricObject {
	private const float GROUND_Y = 0;

	public LandType type;
	public bool isFinalType;
	public bool hasResource;
	public bool selectedToHarvest;

	public Animator myAnimator;
	
	public CellBehaviour MyCell { get; set; }
	public bool CausedAttraction { get; set; }
	public bool InAttractionProcess { get; set; }
	public bool IsUpgrading { get; set; }
	public bool IsFalling { get; set; }
	public bool IsFixingPosition { get; set; }
	public Vector3 AttractionPoint { get; set; }

	public Vector3 fixedPosition;

	private Transform myObjects;

	void Start() {
		if (!IsUpgrading) {
			WorldBehaviour.Instance.AddLandEvent();

			StartCoroutine("StartFallingProcess");
		} else {
			IsUpgrading = false;

			WorldBehaviour.Instance.LandAttractor.DoAttractionLogics(this);
		}

		myObjects = transform.FindChild("Objects");
	}

	void Update() {
		if(!IsFalling) {
			FixMyPosition();
		}
	}

	//PUBLIC
	public void UpgradeToNextType() {
		StartCoroutine("UpgradeToNextTypeRoutine");
	}

	public void HarvestMyResource() {
		if(this.hasResource && this.selectedToHarvest) {
			HideLandObjects();
			this.type = LandType.Lawn;
			this.hasResource = false;
			this.selectedToHarvest = false;
			this.isFinalType = false;
		}
	}
		
	//PRIVATE
	private void FixMyPosition() {
		if(transform.position.y != GROUND_Y) {
			fixedPosition = transform.position;
			fixedPosition.y = GROUND_Y;
			transform.position = fixedPosition;

			IsFixingPosition = false;
		}
	}

	private void HideLandObjects() {
		if (myObjects != null) {
			myObjects.gameObject.SetActive (false);
		}
	}

	//COROUTINES
	IEnumerator StartFallingProcess() {
		SoundManager.Instance.PlayFalling();

		Vector3 startPoint = transform.position;
		Vector3 endPoint = transform.position;
		endPoint.y = GROUND_Y;

		float startTime = Time.time;

		while(transform.position.y > GROUND_Y) {
			transform.position = Vector3.Lerp(startPoint, endPoint, (Time.time - startTime) * 5f);
			yield return new WaitForSeconds(0.01f);
		}

		IsFalling = false;

		WorldBehaviour.Instance.DoWorldShake();
		WorldBehaviour.Instance.LandAttractor.DoAttractionLogics(this);

		IsFixingPosition = true;
	}

	IEnumerator UpgradeToNextTypeRoutine() {
		IsUpgrading = true;

		yield return new WaitForSeconds(0.2f);

		LandType nextType = LandEvolutionManager.Instance.NextType(type);

		if(this.MyCell != null) {
			this.MyCell.Value = (int) nextType;

			WorldBehaviour.Instance.ResetCellFromMyGridByIndex(this.MyCell.Back);					
			WorldBehaviour.Instance.ResetCellFromMyGridByIndex(this.MyCell.Front);
			WorldBehaviour.Instance.ResetCellFromMyGridByIndex(this.MyCell.Left);					
			WorldBehaviour.Instance.ResetCellFromMyGridByIndex(this.MyCell.Right);
		}

		WorldBehaviour.Instance.LandInstantiator.InstantiateLand (
			nextType, transform.position, this.MyCell, IsUpgrading, WorldBehaviour.Instance.TestCriterias
		);

		PlayerExperienceManager.Instance.GiveXpToPlayer(nextType);

		Destroy(gameObject, 0.2f);
	}
}