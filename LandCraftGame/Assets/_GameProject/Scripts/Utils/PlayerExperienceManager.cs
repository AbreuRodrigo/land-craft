using UnityEngine;
using System.Collections;

public class PlayerExperienceManager {

	public static long GetNextLevelValue(int currentLevel) {
		return (long) (currentLevel * 1000f);
	}
}
