using UnityEngine;
using System.Collections;

public class Hero : CharacterUnit {
		
	protected override void Start() {
		base.Start ();
		Debug.Log ("start Hero");
	}
	
	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			FindTargetPoint();
		}

		if (this.GetTargetPoint() != this.transform.position) {
			this.Move();
		}
	}

	private void FindTargetPoint() {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if (Physics.Raycast(ray, out hit)) {
			if (hit.collider.gameObject.name == "ground") {
				this.SetTargetPoint(new Vector3(hit.point.x, this.transform.position.y/2, hit.point.z));
			}
		}
	}
}