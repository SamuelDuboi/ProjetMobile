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
            if (!objet.activeSelf)
                objet.SetActive(true);
            if (objet.GetComponent<ObjectHandler>())
            {
                if (objet.GetComponent<ObjectHandler>().interactifElement.hasLinkGameObject)
                    InventoryManager.Instance.AddList(gameObject.gameObject, objet.GetComponent<ObjectHandler>().NameToAddIfAnimToAdd, default);
                objet.GetComponent<ObjectHandler>().Interact(objet.GetComponent<ObjectHandler>().HitBoxZoom.gameObject);
            }
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
