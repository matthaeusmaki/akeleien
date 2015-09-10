using UnityEngine;
using System.Collections;

public class AnimatorController_UI : MonoBehaviour {

    private Animator animator;
    int attackHash = Animator.StringToHash("Attack");
    

	// Use this for initialization
	void Start () {
        this.animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        float move = Input.GetAxis("Vertical");
        animator.SetFloat("Speed", move);
        //animator.SetFloat("turn", Input.GetAxis("Horizontal"));

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger(attackHash);
        }

	}
}
