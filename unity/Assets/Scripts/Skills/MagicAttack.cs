using UnityEngine;
using System.Collections;

public class MagicAttack : MonoBehaviour {

    private bool fired = false;
    private GameObject player;
    private Vector3 targetPosition;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
