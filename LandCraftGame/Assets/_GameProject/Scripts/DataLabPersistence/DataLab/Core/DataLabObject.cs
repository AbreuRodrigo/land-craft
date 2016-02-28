using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DataLab {

	public class DataLabObject {
		protected string documentName;
		public string DocumentName { 
			get {
				return documentName;
			}
		}

		private Dictionary<string, object> fields;
		public Dictionary<string, object> Fields {
			get { return fields; }
			set { this.fields = value; }
		}

		public DataLabObject(string documentName) {
			this.documentName = documentName;
			this.fields = new Dictionary<string, object>();
		}

		public object GetField(string attrName) {
			return fields[attrName];
		}

		public DataLabObject AddField(string key, object value) {
			fields.Add(key, value);

			return this;
		}

		public void Save(System.Action<DataLabObject> asyncResponse) {
			DataLabManager.Instance.SaveObject(this, asyncResponse);
		}

		public void Load(string attrName, string attrValue, System.Action<DataLabObject> asyncResponse) {
			DataLabManager.Instance.LoadObject(documentName, attrName, attrValue, asyncResponse);
		}

		public void Delete(System.Action<DataLabObject> asyncResponse) {
			DataLabManager.Instance.DeleteObject(this, asyncResponse);
		}

		public void Update(System.Action<DataLabObject> asyncResponse) {
			DataLabManager.Instance.UpdateObject(this, asyncResponse);
		}

		public void Persist(System.Action<DataLabObject> asyncResponse) {
			DataLabManager.Instance.PersistObject(this, asyncResponse);
		}

		public bool GetBoolean(string key) {
			return Convert.ToBoolean(fields[key] == null);
		}

		public short GetShort(string key) {
			return Convert.ToInt16(fields[key]);
		}

		public int GetInt(string key) {
			return Convert.ToInt32(fields[key]);
		}

		public long GetLong(string key) {
			return Convert.ToInt64(fields[key]);
		}

		public float GetFloat(string key) {
			return Convert.ToSingle(fields[key]);
		}

		public double GetDouble(string key) {
			return Convert.ToDouble(fields[key]);
		}

		public string GetString(string key) {
			return Convert.ToString(fields.ContainsKey(key) ? fields[key] : "");
		}
	}
}