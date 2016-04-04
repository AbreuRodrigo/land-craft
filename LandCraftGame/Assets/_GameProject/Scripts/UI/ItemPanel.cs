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

	[SerializeField]
	private Image currencyImage;

	[SerializeField]
	private Button myButton;

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

	public string ItemPrice {
		get { return itemPrice.text; }
		set { itemPrice.text = value; }
	}

	public Image CurrencyImage { 
		get { return currencyImage; } 
		set { currencyImage = value; }
	}

	public Button MyButton {
		get { return myButton; }
		set { myButton = value; }
	}

	public void AddButtonEvent(StoreItemType type, string name, string price) {
		MyButton.onClick.AddListener(() => GUIManagerGameLobby.Instance.BuyItem(type, name, price));
	}
}