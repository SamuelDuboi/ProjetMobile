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
            if (!interactifElement.spawnNewTrial)
            {
                if (interactifElement.PopupAfterAnim)
                {
                    StartCoroutine(WaitToPopUp(interactifElement.interactionAnimator.GetCurrentAnimatorClipInfo(0).Length));
                }
            }
            else if (trialInstantiate == null)
            {
                interactifElement.onlyZoom = true;
                HitBoxZoom.enabled = false;
                interactifElement.spawnNewTrial = false;
                trialInstantiate = Instantiate(interactifElement.TrialGameObjects, Camera.main.transform.position, Quaternion.identity);
                trialInstantiate.transform.SetParent(transform);
                return;
            }
            if (!doOnce)
            {
                doOnce = true;
                interactifElement.interactionAnimator.SetTrigger("Interact");
                if(interactifElement.objectToActive.Count>0)
                {
                    interactifElement.objectToActive[0].SetActive(true);
                }
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
