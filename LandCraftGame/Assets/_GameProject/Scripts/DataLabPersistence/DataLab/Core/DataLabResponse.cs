using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DataLab {

	public class DataLabResponse {
		private const string CODE_FIELD = "code";
		private const string MESSAGE_FIELD = "message";
		private const string RESPONSE_DOCUMENT_NAME = "Response";

		private Dictionary<string, object> fields;
		public Dictionary<string, object> Fields {
			get { return fields; }
			set { this.fields = value; }
		}

		public DataLabResponse(DataLabObject dataLabObject) {
			if(dataLabObject != null) {
				this.Fields = dataLabObject.Fields;
			}
		}

		public string Code {
			get {
				return Fields[CODE_FIELD].ToString();
			}
		}

		public string Message {
			get {
				return Fields[MESSAGE_FIELD].ToString();
			}
		}

		public string CompleteMessage {
			get {
				return "Code: " + Code + " Message: " + Message;
			}
		}
	}
}