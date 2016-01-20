using UnityEngine;
using System.Collections;

public class ParseAttribute<T> {
	private string label;
	private T value;

	public string Label {
		get { return this.label; }
		set { this.label = value; }
	}
	public T Value {
		get { return this.value; }
		set { this.value = value; }
	}

	public ParseAttribute(string label, T value) {
		this.label = label;
		this.value = value;
	}
}
