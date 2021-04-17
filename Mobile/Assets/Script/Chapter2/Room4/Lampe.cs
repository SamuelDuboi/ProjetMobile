using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lampe : ObjectHandler
{

    public ObjectHandler meuble;
    public override void Interact(GameObject currentGameObject)
    {
        if (HitBoxZoom != null && currentGameObject == HitBoxZoom.gameObject)
        {
            if (interactifElement.popInteract)
            {
                EventManager.instance.OnPopup(interactifElement.text, interactifElement.timePopup);
            }


            if (interactifElement.hasLinkGameObject)
            {
                int numberOfObject = 0;
                foreach (var curentGamObject in interactifElement.ObjectoOpen)
                {
                    int number;
                    var inventoryItem = InventoryManager.Instance.FindObject(curentGamObject, out number);
                    if (inventoryItem)
                    {
                        numberOfObject++;
                        InventoryManager.Instance.RemoveFromList(curentGamObject, 1);
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
                InventoryManager.Instance.AddList(gameObject, meuble.NameToAddIfAnimToAdd, default, 1);
                meuble.Interact(meuble.HitBoxZoom.gameObject);
            }
            InteractActiveObject(true);
             if (trialInstantiate == null)
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
