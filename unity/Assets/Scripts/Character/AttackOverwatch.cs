using UnityEngine;
using System.Collections;

/// <summary>
/// Aufgabe:
///     -   Bestimmt ob ein Charakter wieder eingreifen darf.
/// 		Dies wird anhand dem starten und dem Beenden der Attack-Animation bestimmt.
/// </summary>
public class AttackOverwatch : StateMachineBehaviour
{    
	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
		if (animatorStateInfo.IsName ("Attack")) 
        {
            animator.gameObject.GetComponent<BasicCharacter>().isAttacking = false;
		}         
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
        if (animatorStateInfo.IsName("Attack"))
        {
            animator.gameObject.GetComponent<BasicCharacter>().isAttacking = true;            
        } 
	}
}
