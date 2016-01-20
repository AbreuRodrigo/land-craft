using UnityEngine;
using System.Collections;

public class Stage {
	public int index;
	public string id;
	public int stars;
	public int time;
	public bool isLocked;
	public bool isOpen;
	public bool isClear;
	public int steps;

	public Stage() {
	}

	public Stage(XS xs) {
		index = xs.a;
		id = xs.b;
		stars = xs.c;
		time = xs.d;
		isLocked = xs.e;
		isOpen = xs.f;
		isClear = xs.g;
		steps = xs.h;
	}
}