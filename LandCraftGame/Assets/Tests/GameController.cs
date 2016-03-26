using DataLab;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public Image image;

	void Start () {
		DataLabManager.Instance.ListObjects ("StoreItem", Response);
	}

	private void Response(List<DataLabObject> responseList) {
		DataLabObject o = responseList[0];

		image.sprite = o.GetSprite("image");
	}
}
