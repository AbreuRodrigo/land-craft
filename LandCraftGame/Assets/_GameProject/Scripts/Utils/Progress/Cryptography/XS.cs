using UnityEngine;
using System.Collections;

[System.Serializable]
public class XS {
	public int a;
	public string b;
	public int c;
	public int d;
	public bool e;
	public bool f;
	public bool g;
	public int h;

	public XS(int x, string i) {
		a = x;
		b = i;
		c = 0;
		d = 0;
		e = true;
		f = false;
		g = false;
		h = 0;
	}

	public XS(int x, string i, int s, int t, bool l, bool z, bool w, int y) {
		this.a = x;
		this.b = i;
		this.c = s;
		this.d = t;
		this.e = l;
		this.f = z;
		this.g = w;
		this.h = y;
	}

	public XS(Stage stage) {
		a = stage.index;
		b = stage.id;
		c = stage.stars;
		d = stage.time;
		e = stage.isLocked;
		f = stage.isOpen;
		g = stage.isClear;
		h = stage.steps;
	}
}