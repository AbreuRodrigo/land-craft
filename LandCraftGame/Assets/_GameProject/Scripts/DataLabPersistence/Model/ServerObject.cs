using DataLab;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

public abstract class ServerObject {
	public const string CLIENT_TOKEN_LABEL = "clientToken";
	pivate const string PLAYER_STATS_STR = "PlayerStats";

	public string clientToken;
	public string deviceModel;
	public string deviceType;
	protected string className;
	protected DataLabObject dataObject;
	protected string[] fieldLabels;

	public string ClassName {
		get { return className; }
	}

	protected ServerObject() {
		clientToken = SystemInfo.deviceUniqueIdentifier;

		className = this.GetType().Name;

		if(PLAYER_STATS_STR.Equals(className)) {
			deviceModel = SystemInfo.deviceModel;
			deviceType = SystemInfo.deviceType.ToString();
		}
		
		dataObject = new DataLabObject(className);
		
		FieldInfo[] fields = this.GetType().GetFields();
		
		fieldLabels = new string[fields.Length];
		
		for(int i = 0; i < fields.Length; i++) {
			fieldLabels[i] = fields[i].Name;
		}

		this.OnBegin();
	}

	protected abstract void OnBegin();
	protected abstract void OnLoaded();

	public void Save() {
		foreach(string field in fieldLabels) {
			dataObject.Fields[field] = this.GetType().GetField(field).GetValue(this);
		}

		dataObject.Persist(null);
	}

	public void ConvertObjectToServerObject(DataLabObject dataObject) {
		this.dataObject = dataObject;
		this.OnLoaded();
	}

	protected bool IsDataObjectValid() {
		return dataObject != null && dataObject.Fields != null && dataObject.Fields.Count > 0 && 
			(dataObject.Fields.Count > 2 || (dataObject.Fields.Count == 2 && "0".Equals (dataObject.Fields ["code"])));
	}
}