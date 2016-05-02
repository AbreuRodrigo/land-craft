using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManagerGamePlay : GUIManagerBasePlay {
	public static GUIManagerGamePlay Instance;

	void Awake() {
		if(Instance == null) {
			Instance = this;	
		}
	}

	void Start() {
		base.Start();
	}

	protected override void StartMyStateUIEvent(GameState state) { }
}