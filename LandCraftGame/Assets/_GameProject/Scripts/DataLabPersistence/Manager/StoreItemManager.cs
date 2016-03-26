using DataLab;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreItemManager : ServerObjectManager {

	private const string DOCUMENT_NAME = "StoreItem";

	public List<StoreItemView> items;

	void Start() {		
		items = new List<StoreItemView>();

		DataLabManager.Instance.ListObjects(DOCUMENT_NAME, LoadResponseList);
	}

	protected override void LoadResponse(DataLabObject dataObject) {
	}

	protected override void LoadResponseList(List<DataLabObject> dataObjectList) {
		if(dataObjectList != null && dataObjectList.Count > 0) {
			foreach (DataLabObject o in dataObjectList) {
				StoreItemView item = new StoreItemView ();
				item.Name = o.GetString ("name");
				item.Description = o.GetString ("description");
				item.Price = o.GetInt ("price");
				item.Image = o.GetSprite ("image");

				items.Add (item);
			}
		}
	}
}
