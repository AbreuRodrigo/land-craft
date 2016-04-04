using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using OnePF;

public class OpenIABStore : MonoBehaviour {

	public Text crystals;
	public Text iabStatus;

	private const string _100_CRYSTALS_PACK = "100_crystals_pack";

	void Awake() {		
		OpenIABEventManager.billingSupportedEvent += OnBillingSupported;
		OpenIABEventManager.billingNotSupportedEvent += OnBillingNotSupported;
		OpenIABEventManager.queryInventorySucceededEvent += OnQueryInventorySucceeded;
		OpenIABEventManager.queryInventoryFailedEvent += OnQueryInventoryFailed;
		OpenIABEventManager.purchaseSucceededEvent += OnPurchaseSucceded;
		OpenIABEventManager.purchaseFailedEvent += OnPurchaseFailed;
		OpenIABEventManager.consumePurchaseSucceededEvent += OnConsumePurchaseSucceeded;
		OpenIABEventManager.consumePurchaseFailedEvent += OnConsumePurchaseFailed;
		OpenIABEventManager.transactionRestoredEvent += OnTransactionRestored;
		OpenIABEventManager.restoreSucceededEvent += OnRestoreSucceeded;
		OpenIABEventManager.restoreFailedEvent += OnRestoreFailed;
	}

	public void ConnectToStore() {
		OpenIAB.mapSku(_100_CRYSTALS_PACK, OpenIAB_Android.STORE_GOOGLE, "100_crystals_pack");

		Options options = new Options();
		options.checkInventory = true;
		options.verifyMode = OptionsVerifyMode.VERIFY_EVERYTHING;
		options.storeKeys.Add(OpenIAB_Android.STORE_GOOGLE, "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAordhGvQqC71rek6CUuipXLkb1L+LowHXl0y9F/FVw+IdiO3kT0h9wss8e9xLmVio5/k/pV8u7Bg5zQWGMcY9b7g+t8E8+3BMDAbWJGOgvIvuRIJzy4eJjvzcFced5ziybtAl4B5UFGxP9G2+h2lPqVwqhJ/zdD1Mk/FO3sg/230ZVFa8GTG2w9NCU5pgyKy9SkR8vV2fPJCcIlYmF4s+KJKwCJ5y6wnLNc+Q/BYbnVuD9lhD7GnV8Mjdh4h+/989LT5wtTVmkYrWUZkFxJj6lfecUieFwyg440irW3546/OoSCUWlF+j3NH4BWRP3Mk5AHMef/ktoXtpVllN4mxgVQIDAQAB");
		OpenIAB.init(options);
	}

	private void OnDestroy() {
		OpenIABEventManager.billingSupportedEvent -= OnBillingSupported;
		OpenIABEventManager.billingNotSupportedEvent -= OnBillingNotSupported;
		OpenIABEventManager.queryInventorySucceededEvent -= OnQueryInventorySucceeded;
		OpenIABEventManager.queryInventoryFailedEvent -= OnQueryInventoryFailed;
		OpenIABEventManager.purchaseSucceededEvent -= OnPurchaseSucceded;
		OpenIABEventManager.purchaseFailedEvent -= OnPurchaseFailed;
		OpenIABEventManager.consumePurchaseSucceededEvent -= OnConsumePurchaseSucceeded;
		OpenIABEventManager.consumePurchaseFailedEvent -= OnConsumePurchaseFailed;
		OpenIABEventManager.transactionRestoredEvent -= OnTransactionRestored;
		OpenIABEventManager.restoreSucceededEvent -= OnRestoreSucceeded;
		OpenIABEventManager.restoreFailedEvent -= OnRestoreFailed;
	}

	void OnBillingSupported() {
		iabStatus.color = Color.green;
		iabStatus.text = "Connected to the store!";
		OpenIAB.queryInventory();
	}

	void OnBillingNotSupported(string error) {
		iabStatus.color = Color.red;
		iabStatus.text = "Could not to the store!";
	}

	void OnQueryInventorySucceeded(Inventory inventory) {
	}

	void OnQueryInventoryFailed(string error) {
	}

	void OnPurchaseSucceded(Purchase purchase) {
		if(purchase.Sku.Equals(_100_CRYSTALS_PACK)) {
			OpenIAB.consumeProduct(purchase);

			int value = int.Parse(crystals.text);
			crystals.text = "Crystals: " + (value + 100);
		}
	}

	void OnPurchaseFailed(int errorCode, string error) {
	}

	void OnConsumePurchaseSucceeded(Purchase purchase) {
	}

	void OnConsumePurchaseFailed(string error) {
	}

	void OnTransactionRestored(string sku) {
	}

	void OnRestoreSucceeded() {
	}

	void OnRestoreFailed(string error) {
	}

	public void Purchase100CrystalsPack() {
		OpenIAB.purchaseProduct(_100_CRYSTALS_PACK);
	}
}
