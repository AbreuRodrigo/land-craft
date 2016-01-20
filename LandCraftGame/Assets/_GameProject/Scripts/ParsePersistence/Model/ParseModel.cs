using Parse;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;

public abstract class ParseModel {
	public const string CLIENT_TOKEN_LABEL = "clientToken";

	public string clientToken;
	protected string className;
	protected ParseObject parseObject;
	protected string[] fieldLabels;

	public string ClassName {
		get { return className; }
	}

	protected ParseModel() {
		clientToken = SystemInfo.deviceUniqueIdentifier;

		className = this.GetType().Name;
		
		parseObject = new ParseObject(className);
		
		FieldInfo[] fields = this.GetType().GetFields();
		
		fieldLabels = new string[fields.Length];
		
		for(int i = 0; i < fields.Length; i++) {
			fieldLabels[i] = fields[i].Name;
		}

		this.OnBegin();
		
		this.SaveModelStatsOnNotExists();
	}

	protected abstract void OnBegin();
	protected abstract void OnLoaded();

	protected void SaveModelStatsOnNotExists() {
		ParseQuery<ParseObject> query = ParseObject.GetQuery(this.className);
		query = query.WhereEqualTo(CLIENT_TOKEN_LABEL, clientToken);
		query.FirstAsync().ContinueWith(SaveAfterwards);
	}

	void SaveAfterwards(Task<ParseObject> t) {
		if (t.IsFaulted) {
			this.Save ();
		} else {
			this.LoadStats();
		}
	}

	public void Save() {
		foreach(string field in fieldLabels) {
			parseObject[field] = this.GetType().GetField(field).GetValue(this);
		}

		parseObject.SaveAsync();
	}

	public void LoadStats() {
		ParseQuery<ParseObject> query = ParseObject.GetQuery(this.className);
		query = query.WhereEqualTo(CLIENT_TOKEN_LABEL, clientToken);
		query.FirstAsync().ContinueWith(ParseFromObjectToStats);
	}

	public void ParseFromObjectToStatsPassingParseObject(ParseObject po) {
		parseObject = po;
		this.OnLoaded();
	}

	public void ParseFromObjectToStats(Task<ParseObject> t) {
		parseObject = t.Result;
		this.OnLoaded();
	}
}