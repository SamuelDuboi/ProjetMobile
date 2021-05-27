using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAfterAnim : StateMachineBehaviour
{
    public bool Unzoom;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Unzoom)
            EventManager.instance.OnZoomOut();
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var gameObject = animator.gameObject.GetComponentInParent<ObjectHandler>();
        if (gameObject.interactifElement.objectToInteract != null)
        {
            foreach (var objet in gameObject.interactifElement.objectToInteract)
            {
                if (!objet.activeSelf)
                    objet.SetActive(true);
                if (objet.GetComponent<ObjectHandler>() && objet.GetComponent<ObjectHandler>().interactifElement)
                {
                  if (objet.GetComponent<ObjectHandler>().interactifElement.hasLinkGameObject)
                        InventoryManager.Instance.AddList(gameObject.gameObject, objet.GetComponent<ObjectHandler>().NameToAddIfAnimToAdd, default);
                    objet.GetComponent<ObjectHandler>().Interact(objet.GetComponent<ObjectHandler>().HitBoxZoom.gameObject);
                   
                }
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
