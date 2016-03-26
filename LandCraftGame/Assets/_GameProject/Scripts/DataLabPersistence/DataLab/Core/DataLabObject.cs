using System;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

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

		public void List(System.Action<List<DataLabObject>> asyncResponse) {
			DataLabManager.Instance.ListObjects(documentName, asyncResponse);
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

		public byte[] GetBytes(string key) {
			BinaryFormatter bf = new BinaryFormatter();
			MemoryStream ms = new MemoryStream();
			bf.Serialize(ms, fields[key]);

			return ms.ToArray();
		}

		public Sprite GetSprite(string key) {
			Texture2D texture = new Texture2D (64, 64, TextureFormat.RGB24, false);
			texture.LoadImage(System.Convert.FromBase64String((string)fields[key]));

			return Sprite.Create(
				texture, 
				new Rect (0, 0, texture.width, texture.height), 
				new Vector2(0.5f, 0.5f)
			);
		}

		private void SaveImage(Texture2D t) {
			byte[] bytes = t.EncodeToJPG ();
			File.WriteAllBytes ("E:/myImage.jpg", bytes);
		}
	}
}