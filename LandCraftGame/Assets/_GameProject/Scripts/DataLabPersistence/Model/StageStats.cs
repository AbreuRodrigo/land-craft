using UnityEngine;
using System.Collections;

public class StageStats : ServerObject {
	public int stageNumber;
	public int stageStatus;//0, 1, 2 (closed=0, open=1, clear=2)
	public int stageStars;
	public string stageTiming;//00:00

	protected override void OnBegin() {
	}

	protected override void OnLoaded() {
	}
}