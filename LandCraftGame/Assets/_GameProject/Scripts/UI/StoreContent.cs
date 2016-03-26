using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoreContent : MonoBehaviour {
	
	public GameObject itemPrefab;

	public RectTransform content;

	private float positionModifier;
	private Vector3 itemPanelSize = new Vector3 (1, 1, 1);

	void Start() {		
		ResetSpellShopContents();
	}

	public void ResetSpellShopContents() {
		RectTransform itemRect = itemPrefab.GetComponent<RectTransform>();

		int maxItemsInTheScreen = (int) (content.rect.width / itemRect.rect.width);

		float newContentWidth = ((itemRect.rect.width * (ModalManager.Instance.TotalItems - maxItemsInTheScreen)) + content.rect.width);

		content.sizeDelta = new Vector2(newContentWidth, 0);
		content.anchoredPosition = new Vector2(newContentWidth * 0.5f, 0);
	}

	public void AddNewItem(string name, string description, int price, Sprite image, int index, float x, float y) {
		if(content) {
			GameObject itemObject = (GameObject) Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
			itemObject.transform.parent = content.transform;

			ItemPanel itemPanel = itemObject.GetComponent<ItemPanel>();
			itemPanel.ItemName = name;
			itemPanel.ItemDescription = description;
			itemPanel.ItemPrice = price;
			itemPanel.ItemImage.sprite = image;

			RectTransform currentItemRect = itemObject.GetComponent<RectTransform>();
			positionModifier = (index * 1 + index * currentItemRect.rect.width);
			currentItemRect.anchoredPosition = new Vector2 (positionModifier + x, y - 25);
			currentItemRect.localScale = itemPanelSize;
		}
	}
}
