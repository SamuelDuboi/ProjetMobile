using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandlerActiveWater : ObjectHandler
{
  public  TheWaterManager waterManager;
    private bool doOnce;
    override public void  Interact(GameObject currentGameObject)
    {

        if (HitBoxZoom != null && currentGameObject == HitBoxZoom.gameObject)
        {
            if (interactifElement.popInteract)
            {
                EventManager.instance.OnPopup(interactifElement.text, interactifElement.timePopup);
            }
            if (!doOnce)
            {
                doOnce = true;
                interactifElement.interactionAnimator.SetTrigger("Interact");
                StartCoroutine(WaterACtiveAfterAnim());
            }
        }
    }
    IEnumerator WaterACtiveAfterAnim()
    {
        yield return new WaitForSeconds(1f);
        waterManager.WaterUp();

    }
}
