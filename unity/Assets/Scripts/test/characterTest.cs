using UnityEngine;
using System.Collections;

public class characterTest : BasicCharacter {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Update: (Time= " + Time.deltaTime + ") (IsAttacking? " + this.isAttacking + ")");
	    
	}
}
