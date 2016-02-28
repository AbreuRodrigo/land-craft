using UnityEngine;
using System.Collections;
using DataLab;

public class GameExample : MonoBehaviour {

	void Start () {
		DataLabObject playerStats = new DataLabObject("PlayerStats");

		playerStats.AddField("clientToken", SystemInfo.deviceUniqueIdentifier)
				   .AddField("playerName", "Warlord Drake")
				   .AddField("score", 100)
				   .AddField("level", 1);

		//playerStats.Persist(ResponseFromPersistence);

		playerStats.Load("clientToken", SystemInfo.deviceUniqueIdentifier, ResponseFromLoad);
	}

	void ResponseFromPersistence(DataLabObject response) {
		DataLabResponse r = new DataLabResponse(response);

		if("0".Equals(r.Code)) {
			Debug.Log("PlayerStats was saved successfully!");
		}else {
			Debug.LogError("Something went wrong!");
		}
	}

	void ResponseFromLoad(DataLabObject response) {
		foreach(var f in response.Fields) {
			Debug.Log(f.Key + " : " + f.Value);
		}
	}
}