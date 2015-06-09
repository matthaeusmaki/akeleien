using UnityEngine;
using System.Collections;

public class CharacterUnit : MonoBehaviour {

	public int attackRange;
	public int hitPoints;
	public float movingSpeed = 2.5f;
	public float rotationSpeed = 5.0f;

	private Vector3 targetPoint;
	//private UnitStateEnum state = UnitStateEnum.IDLE;

	protected virtual void Start () {
		Debug.Log ("start characterUnit");
	}

	public void Move() {
		this.targetPoint.y = this.transform.lossyScale.y/2;

		LookToCorrectDirection ();
		this.transform.position = Vector3.MoveTowards (this.transform.position, this.targetPoint, Time.deltaTime * movingSpeed);
	}

	public void LookToCorrectDirection() {
		Vector3 _direction = (this.targetPoint - this.transform.position).normalized;
		
		//create the rotation we need to be in to look at the target
		Quaternion _lookRotation = Quaternion.LookRotation(_direction);
		
		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
	}

	public void SetTargetPoint(Vector3 target) {
		this.targetPoint = target;
	}

	public Vector3 GetTargetPoint() {
		return this.targetPoint;
	}
}
