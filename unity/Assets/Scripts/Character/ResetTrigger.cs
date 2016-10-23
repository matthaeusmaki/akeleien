using UnityEngine;
using System.Collections;


public class ResetTrigger : StateMachineBehaviour
{
    public string triggerName = null;

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        /*
        if (triggerName != null)
        {
            Debug.Log(Time.deltaTime + " ResetTrigger '" + triggerName + "'");
            animator.ResetTrigger(triggerName);
        }
         */
        animator.SetBool("isAttacking", false);
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetBool("isAttacking", true);
    }

}