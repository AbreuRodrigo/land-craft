using Parse;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class ParseManager : MonoBehaviour {

	protected virtual void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	protected void LoadStats(ParseModel model) {
		ParseQuery<ParseObject> query = ParseObject.GetQuery(model.ClassName);
		query = query.WhereEqualTo(ParseModel.CLIENT_TOKEN_LABEL, model.clientToken);
		query.FirstAsync().ContinueWith(model.ParseFromObjectToStats);
	}
}