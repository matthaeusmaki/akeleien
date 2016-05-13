using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CurvedSlider : MonoBehaviour {

	private float staticSliderValue = 77;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
//		GUILayout.HorizontalSlider (Rect (940, 135+(Mathf.Sin(hSliderValue*-.0175)*30), 243, 30), staticSliderValue, 0.0, 180.0);
		//float hSliderValue = GUI.HorizontalSlider (Rect (940, 135+(Mathf.Sin(hSliderValue*-.0175)*30), 243, 30), staticSliderValue, 0.0, 180.0);

		staticSliderValue = GUI.HorizontalSlider (
			new Rect (940, 135+(Mathf.Sin(staticSliderValue*-.0175f)*30), 243, 30),
			staticSliderValue, 
			0.0f, 
			100.0f
		);


	}
}
