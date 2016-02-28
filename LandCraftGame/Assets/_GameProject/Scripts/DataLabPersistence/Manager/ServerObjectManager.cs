using DataLab;
using UnityEngine;
using System.Collections;

public abstract class ServerObjectManager : MonoBehaviour {

	protected DataLabObject dataLabObject;

	protected virtual void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	protected void LoadStats(ServerObject obj) {
		dataLabObject = new DataLabObject(obj.ClassName);
		dataLabObject.Load(ServerObject.CLIENT_TOKEN_LABEL, obj.clientToken, ResponseFromLoad);
	}

	private void ResponseFromLoad(DataLabObject dataObject) {
		this.LoadStatsResponse(dataObject);
	}

	protected abstract void LoadStatsResponse(DataLabObject dataObject);
}