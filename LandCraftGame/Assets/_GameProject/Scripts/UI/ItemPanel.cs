using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemPanel : MonoBehaviour {

	[SerializeField]
	private Text itemName;
	[SerializeField]
	private Image itemImage;
	[SerializeField]
	private Text itemDescription;
	[SerializeField]
	private Text itemPrice;

	public string ItemName {
		get { return itemName.text; }
		set { itemName.text = value; }
	}

	public Image ItemImage { 
		get { return itemImage; } 
		set { itemImage = value; }
	}

	public string ItemDescription {
		get { return itemDescription.text; }
		set { itemDescription.text = value; }
	}

	public int ItemPrice {
		get { return int.Parse(itemPrice.text); } 
		set { itemPrice.text = value.ToString(); }
	}
}
