using UnityEngine;
using System.Collections;
public class cameraController : MonoBehaviour
{
	void OnGUI()
	{
		GUI.Label(new Rect(5, 5, 200, 20), "Accelerometer X: " + Input.acceleration.x);
		GUI.Label(new Rect(5, 25, 200, 20), "Accelerometer Y:" + Input.acceleration.y);
		GUI.Label(new Rect(5, 45, 200, 20), "Accelerometer Z:" + Input.acceleration.z);
		GUI.Label(new Rect(5, 100, 200, 20), "Number of touches: " + Input.touchCount);
		for (int i = 0; i < Input.touchCount; i++)
		{
			PrintTouchData(Input.touches[i]);
		}
	}

	void PrintTouchData(Touch t)
	{
		GUI.Label(new Rect(t.position.x, Screen.height - t.position.y, 100, 20), t.position.ToString());
		GUI.Label(new Rect(t.position.x, Screen.height - t.position.y + 20, 100, 20), "Finger: " + t.fingerId.ToString());
		GUI.Label(new Rect(t.position.x, Screen.height - t.position.y + 40, 100, 20), "Tap count: " + t.tapCount.ToString());
	}
}