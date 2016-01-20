using UnityEngine;
using System.Collections;

public class PlayerPrefsManager {

	private const string CLICKED_STAGE = "CLICKED_STAGE";

	public static int ClickedStage {
		get { return GetIntPref(CLICKED_STAGE, 1); }
		set { SetIntPref(CLICKED_STAGE, value); }
	}

	private static void SetIntPref(string key, int val) {
		PlayerPrefs.SetInt(key, val);
	}

	private static int GetIntPref(string key, int defaultValue = 0) {
		if(defaultValue != 0) {
			return PlayerPrefs.GetInt(key, defaultValue);
		}

		return PlayerPrefs.GetInt(key);
	}
}