using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAfterAnim : StateMachineBehaviour
{
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var gameObject = animator.gameObject.GetComponentInParent<ObjectHandler>();
        foreach (var objet in gameObject.interactifElement.objectToInteract)
        {
            objet.GetComponent<ObjectHandler>().Interact(objet.transform.GetChild(0).gameObject);
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
