using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour {

	public int dyingState;
	public int runState;
	public int attackState;
	public int gethitState;

	public int aliveBool;
	public int speedFloat;
	public int attackBool;
	public int gethitTrigger;


	void Awake() {
		dyingState = Animator.StringToHash ("Base Layer.Dying");
		runState = Animator.StringToHash ("Run");
		attackState = Animator.StringToHash ("Attack");
		gethitState = Animator.StringToHash ("GetHit");

		aliveBool = Animator.StringToHash ("Alive");
		speedFloat = Animator.StringToHash ("Speed");
		attackBool = Animator.StringToHash ("Attack");
		gethitTrigger = Animator.StringToHash ("GettingHit");

	}
}
