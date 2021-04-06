using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burningOil : ObjectHandler
{


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
                    return;
                }
                InventoryManager.Instance.AddList(gameObject, "burningOil", default, 1);
                interactifElement.objectToInteract[0].GetComponent<ObjectHandler>().interactifElement.ObjectoOpen.RemoveAt(0);
                interactifElement.objectToInteract[0].GetComponent<ObjectHandler>().interactifElement.PopupAfterAnim = false;

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
                trialInstantiate = Instantiate(interactifElement.TrialGameObjects, Camera.main.transform.position, Quaternion.identity);
                trialInstantiate.transform.SetParent(transform);
            }
        }
    }
}
