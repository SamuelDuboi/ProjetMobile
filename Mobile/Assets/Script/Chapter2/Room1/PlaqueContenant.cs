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
                    return;
                }
                interactifElement.objectToActive[0].SetActive(true);
                TipsManager.instance.changeIndex(interactifElement.indexOfTip);
                InventoryManager.Instance.RemoveFromList(interactifElement.ObjectoOpen[0], 1);
                water.BlockUp();
            }
           
        }
    }
}
