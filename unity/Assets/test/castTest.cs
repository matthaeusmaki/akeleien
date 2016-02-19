using UnityEngine;
using System.Collections;

public class castTest : MonoBehaviour {

	public GameObject ziel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine (this.transform.position, ziel.transform.position);
	}
}
