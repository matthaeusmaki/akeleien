using UnityEngine;
using System.Collections;

public class test_animationcontrollerding : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("...update ...");

		if (Input.GetKeyDown ("0")) {
			anim.SetFloat ("speed", 10);
		} 

		if (Input.GetKeyDown("1")){
			anim.SetFloat ("speed", 0);
		}
	}
}
