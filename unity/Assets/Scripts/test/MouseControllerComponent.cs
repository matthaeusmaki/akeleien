using UnityEngine;
using System.Collections;

/*
 * Durch Mausklick soll das GameObject 
 * auf die angeklickte Position bewegt werden.
 */

public class MouseControllerComponent : MonoBehaviour {
	
	public int rayRange = 30;
	public float speed = 1f;
	
	private Vector3 endPoint;
	private bool isMoving = false;
	
	void Start () {
		this.endPoint = this.transform.position;
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Move();
		}

		//this.transform.position = Vector3.Lerp(this.transform.position, this.endPoint, Time.deltaTime * speed);
		this.transform.position = Vector3.MoveTowards (this.transform.position, this.endPoint, Time.deltaTime * speed);
	}
	
	private void Move() {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if (Physics.Raycast(ray, out hit)) {
			if (hit.collider.gameObject.name == "ground") {
				this.endPoint = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);
				Debug.Log ("target: " + endPoint);
			}
		}
	}
}