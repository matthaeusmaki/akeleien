using UnityEngine;
using System.Collections;

/**
 * Knotenpunkt für Wegfindung
 */
public class Node : MonoBehaviour {
	
	private Color color = new Color(1.0f, 0.0f, 0.0f);
	
	// Use this for initialization
	void Start () {
		this.GetComponent<Renderer>().material.color = color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}