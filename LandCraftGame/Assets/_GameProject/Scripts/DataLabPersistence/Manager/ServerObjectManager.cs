using DataLab;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		this.LoadResponse(dataObject);
	}

	protected abstract void LoadResponse(DataLabObject dataObject);
	protected abstract void LoadResponseList(List<DataLabObject> dataObjectList);
}