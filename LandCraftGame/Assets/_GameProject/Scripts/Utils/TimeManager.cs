using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeManager : MonoBehaviour {
	public Text timeText;

	void FixedUpdate () {
		timeText.text = TreatedTime();
	}

	private string TreatedTime() {
		return String.Format("{0:HH:mm}", System.DateTime.Now);
	}

	public static DateTime CurrentDateTime() {
		return System.DateTime.Now;
	}

	public static string CurrentTime() {
		return String.Format("{0:HH:mm:ss}", System.DateTime.Now);
	}

	public static string TimeStringFromNowToStart(DateTime start) {
		string format = "{0}:{1}";

		TimeSpan t = DateTime.Now.Subtract(start);

		TimeSpan ts = new TimeSpan(0, 0, 60);
		t = ts.Subtract(t);
								
		return String.Format(format, TreatNumber(t.Minutes), TreatNumber(t.Seconds));
	}

	public static TimeSpan TimeSpanFromNowToStart(DateTime start, int timer) {
		TimeSpan t = DateTime.Now.Subtract(start);
		
		TimeSpan ts = new TimeSpan(0, 0, timer);
		t = ts.Subtract(t);
		
		return t;
	}

	public static string TimeSpanAsString(TimeSpan ts) {
		string format = "{0}:{1}";

		return String.Format(format, TreatNumber(ts.Minutes), TreatNumber(ts.Seconds));
	}

	private static string TreatNumber(int value) {
		return value <= 9 ? "0" + value : "" + value;
	}
}