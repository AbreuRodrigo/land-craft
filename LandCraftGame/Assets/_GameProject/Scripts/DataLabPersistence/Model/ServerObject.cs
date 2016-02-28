using DataLab;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

public abstract class ServerObject {
	public const string CLIENT_TOKEN_LABEL = "clientToken";

	public string clientToken;
	protected string className;
	protected DataLabObject dataObject;
	protected string[] fieldLabels;

	public string ClassName {
		get { return className; }
	}

	protected ServerObject() {
		clientToken = SystemInfo.deviceUniqueIdentifier;

		className = this.GetType().Name;
		
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
}