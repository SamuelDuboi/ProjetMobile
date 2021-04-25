using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingTuto : ObjectHandler
{
    private int number;
    public override void Start()
    {
        base.Start();
    }
    public override void Interact(GameObject currentGameObject)
    {
        if (HitBoxZoom != null && currentGameObject == HitBoxZoom.gameObject)
        {
            interactifElement.interactionAnimator.SetTrigger("Interact");
            number++;
            interactifElement.interactionAnimator.SetInteger("Number", number);
        }
    }
}
