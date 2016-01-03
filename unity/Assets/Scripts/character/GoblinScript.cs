using UnityEngine;
using System.Collections;

public class GoblinScript : MonoBehaviour {

	Animator animator;

	//int jumpHash = Animator.StringToHash("Jump");
	int runStateHash = Animator.StringToHash("BaseLayer.run");


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		float move = Input.GetAxis ("Vertical");
		animator.SetFloat ("Speed", move);

		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo (0);


		/*
		if (Input.GetKeyDown (KeyCode.Space) && stateInfo.nameHash == runStateHash) {
			animator.SetTrigger( jumpHash);
		}
		*/


	}
}
