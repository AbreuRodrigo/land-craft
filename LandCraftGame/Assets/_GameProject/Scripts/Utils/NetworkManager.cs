using System.Net;
using UnityEngine;
using System.Collections;
using System.IO;

public class NetworkManager {
	private static NetworkManager instance;
	public static NetworkManager Instance {
		get {
			if(instance == null) {
				instance = new NetworkManager();
			}

			return instance;
		}
	}

	private NetworkManager() {}

	public bool HasInternetConnection() {
		string HtmlText = GetHtmlFromUri("http://google.com");
		
		if(HtmlText == "") {
			return false;
		}else if(!HtmlText.Contains("schema.org/WebPage")) {
			return false;
		}else {
			return true;
		}
	}

	private string GetHtmlFromUri(string resource) {
		string html = string.Empty;
		HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);

		try {
			using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse()) {
				bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
				if (isSuccess) {
					using (StreamReader reader = new StreamReader(resp.GetResponseStream())) {
						char[] cs = new char[80];
						reader.Read(cs, 0, cs.Length);
						foreach(char ch in cs) {
							html +=ch;
						}
					}
				}
			}
		}catch {
			return "";
		}

		return html;
	}
}
