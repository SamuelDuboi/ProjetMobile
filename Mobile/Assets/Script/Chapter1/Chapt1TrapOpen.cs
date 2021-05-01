 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapt1TrapOpen : StateMachineBehaviour
{
     GameObject light;
     MeshRenderer number;
     bool activeNumber;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<Room3Capt1Trap>().interactifElement.spawnNewTrial = false;
        if(light == null)
        light = animator.GetComponentInParent<Room3Capt1Trap>().light;
        light.SetActive(false);
        number = animator.GetComponentInParent<Room3Capt1Trap>().numbers.GetComponent<MeshRenderer>();
        if (activeNumber)
            number.enabled =true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        light.SetActive(true);
        if(number.enabled)
        {
            activeNumber = true;
            number.enabled = false;
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
