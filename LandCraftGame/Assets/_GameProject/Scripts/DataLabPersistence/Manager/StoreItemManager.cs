using DataLab;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreItemManager : ServerObjectManager {

	private const string DOCUMENT_NAME = "StoreItem";

	public List<StoreItemView> Items { get; set; }

	public int SpellCount { get; set; }
	public int CurrencyCount { get; set; }

	private Dictionary<StoreIcon, Sprite> icons = new Dictionary<StoreIcon, Sprite>();

	[Header("Icons")]
	public Sprite invokeGrassIcon;
	public Sprite invokeBushIcon;
	public Sprite invokeTreesIcon;
	public Sprite invokeManaIcon;
	public Sprite destroyLandIcon;
	public Sprite _100_crystals_pack;
	public Sprite _500_crystals_pack;
	public Sprite _1000_crystals_pack;
	public Sprite _5000_crystals_pack;
	public Sprite _10000_crystals_pack;

	void Start() {
		Items = new List<StoreItemView>();

		InitializeIcons();

		DataLabManager.Instance.ListObjects(DOCUMENT_NAME, LoadResponseList);
	}

	protected override void LoadResponse(DataLabObject dataObject) {
	}

	protected override void LoadResponseList(List<DataLabObject> dataObjectList) {
		if (dataObjectList != null && dataObjectList.Count > 0) {
			foreach (DataLabObject o in dataObjectList) {			
				StoreItemView item = new StoreItemView ();

				StoreIcon image = (StoreIcon)System.Enum.Parse (typeof(StoreIcon), o.GetString ("image"), true);
				StoreItemType type = (StoreItemType)System.Enum.Parse (typeof(StoreItemType), o.GetString ("type"), true);

				item.Name = o.GetString ("name");
				item.Description = o.GetString ("description");
				item.Price = o.GetString ("price");
				item.Image = icons [image];
				item.Type = o.GetString ("type");

				if (StoreItemType.Spell == type) {
					SpellCount++;
				} else if (StoreItemType.Currency == type) {
					CurrencyCount++;
				}

				Items.Add (item);
			}
		}
	}

	private void InitializeIcons() {
		icons[StoreIcon.InvokeGrass] = invokeGrassIcon;
		icons[StoreIcon.InvokeBush] = invokeBushIcon;
		icons[StoreIcon.InvokeTrees] = invokeTreesIcon;
		icons[StoreIcon.InvokeMana] = invokeManaIcon;
		icons[StoreIcon.DestroyLand] = destroyLandIcon;
		icons[StoreIcon._100_crystals_pack] = _100_crystals_pack;
		icons[StoreIcon._500_crystals_pack] = _500_crystals_pack;
		icons[StoreIcon._1000_crystals_pack] = _1000_crystals_pack;
		icons[StoreIcon._5000_crystals_pack] = _5000_crystals_pack;
		icons[StoreIcon._10000_crystals_pack] = _10000_crystals_pack;
	}
}