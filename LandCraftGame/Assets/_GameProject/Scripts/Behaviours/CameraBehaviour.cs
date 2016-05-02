using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraBehaviour : MonoBehaviour {
	private const float ZOOM_MIN = 2.0f;
	private const float ZOOM_MAX = 5.0f;

	private float zoomPace = 0.25f;

	void Awake() {
#if UNITY_ANDROID		
		zoomPace = 0.05f;
#endif
#if UNITY_EDITOR
		zoomPace = 0.25f;
#endif
	}

	void Update() {
#if UNITY_ANDROID
		if(Input.touches != null && Input.touches.Length >= 2) {
			Touch t0 = Input.GetTouch(0);
			Touch t1 = Input.GetTouch(1);

			Vector2 touchZeroPrevPos = t0.position - t0.deltaPosition;
			Vector2 touchOnePrevPos = t1.position - t1.deltaPosition;

			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (t0.position - t1.position).magnitude;

			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			if(t0.phase.Equals(TouchPhase.Moved) || t1.phase.Equals(TouchPhase.Moved)) {
				if(deltaMagnitudeDiff >= 0.1f) {
					ZoomOut();
				}else if(deltaMagnitudeDiff <= 0.1f){
					ZoomIn();
				}
			}
		}
#endif
#if UNITY_EDITOR
		if(Input.mouseScrollDelta.y > 0) {
			ZoomIn();
		} else if (Input.mouseScrollDelta.y < 0) {
			ZoomOut();
		}
#endif
	}

	void ZoomOut() {
		if(Camera.main.orthographicSize > ZOOM_MIN) {
			Camera.main.orthographicSize -= zoomPace;

			if(Camera.main.orthographicSize < ZOOM_MIN) {
				Camera.main.orthographicSize = ZOOM_MIN;
			}
		}
	}

	void ZoomIn() {
		if(Camera.main.orthographicSize < ZOOM_MAX) {
			Camera.main.orthographicSize += zoomPace;

			if(Camera.main.orthographicSize > ZOOM_MAX) {
				Camera.main.orthographicSize = ZOOM_MAX;
			}
		}
	}
}