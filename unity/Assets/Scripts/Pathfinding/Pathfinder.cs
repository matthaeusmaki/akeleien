using UnityEngine;
using System.Collections;

public class Pathfinder : MonoBehaviour {

	private GameObject goal;
	private ArrayList nodeList = new ArrayList();

	void Start () {
	
	}

	void Update () {
	
	}

	public void FindPath(Vector3 goalPoint) {
		if (goal != null) {
			GameObject.Destroy(goal);
		}
		goal = Instantiate(Resources.Load("PathNode"), goalPoint, Quaternion.identity) as GameObject;
	}

	/**
	 * Ermittelt die Eckpunkte bei einem Hindernis
	 */
	private void FindNodes() {
		
	}

}
