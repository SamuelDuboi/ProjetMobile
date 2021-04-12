using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaqueContenant : ObjectHandler
{
    public Water water;

    public override void Interact(GameObject currentGameObject)
    {
        if (HitBoxZoom != null && currentGameObject == HitBoxZoom.gameObject)
        {
           
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
                water.BlockUp();
            }
           
        }
    }
}
