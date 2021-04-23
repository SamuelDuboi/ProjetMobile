using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStick : ObjectHandler
{
    public WaterRoom2 waterRoom2;
    public color activationColor;
    public override void Start()
    {
        base.Start();
    }

    public override void Interact(GameObject currentGameObject)
    {
        if (HitBoxZoom != null && currentGameObject == HitBoxZoom.gameObject)
        {
            if (interactifElement.popInteract)
            {
                EventManager.instance.OnPopup(interactifElement.text, interactifElement.timePopup);
            }
            InteractActiveObject(true);

            if (interactifElement.hasLinkGameObject)
            {
                int numberOfObject = 0;
                foreach (var curentGamObject in interactifElement.ObjectoOpen)
                {
                    foreach (var inventoryItem in InventoryManager.Instance.interactifElementsList)
                    {
                        if (inventoryItem.name == curentGamObject)
                            numberOfObject++;
                    }


                }
                if (numberOfObject != interactifElement.ObjectoOpen.Count)
                {
                    interactifElement.interactionAnimator.SetLayerWeight(1, 1);
                    interactifElement.interactionAnimator.SetLayerWeight(2, 0);
                    interactifElement.interactionAnimator.SetTrigger("Interact");
                    return;
                }
                interactifElement.interactionAnimator.SetLayerWeight(1, 0);
                interactifElement.interactionAnimator.SetLayerWeight(2, 1);
                waterRoom2.ActivateBool((int)activationColor);
            }
            if (!interactifElement.spawnNewTrial)
            {
                interactifElement.interactionAnimator.SetTrigger("Interact");
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
                EventManager.instance.OnInstantiateTrial();
                trialInstantiate = Instantiate(interactifElement.TrialGameObjects, Camera.main.transform.position, Quaternion.identity);
                trialInstantiate.transform.SetParent(transform);
            }
        }
    }

    public enum color { Green, Red,Yellow,Blue}
}
