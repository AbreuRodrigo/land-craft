using UnityEngine;
using System.Collections;
using Parse;

public class ParseIntializer : MonoBehaviour {

	void Start () {
		ParseClient.Initialize(new ParseClient.Configuration {
			ApplicationId = "shfcBS2YQdl5XyRRAdxo9WnOdRvqoMW3LuovDp03",
			WindowsKey = "mDjshuRBUODgkPLMxb7JxBuCIobNvCgLiWO50Ouw",
			Server = "http://parse-server-landcraft.herokuapp.com"
		});
	}
}
