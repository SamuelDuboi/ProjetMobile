using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoirScript : ObjectHandler
{
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
                if (ApplyOnInteractIfHAsObject)
                    soundR.Play();
                
                interactifElement.interactionAnimator.SetLayerWeight(1, 0);
                interactifElement.interactionAnimator.SetLayerWeight(2, 1);
                interactifElement.hasLinkGameObject = false;
            }
            InteractActiveObject(true);
            if (!interactifElement.spawnNewTrial)
            {
                if (ApplyOnInteract)
                    soundR.Play();
                interactifElement.interactionAnimator.SetBool("CanAct", true);
                interactifElement.interactionAnimator.SetTrigger("Interact");
                if (parent != null)
                {
                    StartCoroutine(ChangeParent());
                }
                if (interactifElement.activateTips && !doOnce)
                {
                    doOnce = true;
                    TipsManager.instance.changeIndex(interactifElement.indexOfTip);
                }
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
                EventManager.instance.OnInstantiateTrial();
                trialInstantiate.transform.SetParent(transform);
            }
        }
    }
    public virtual void CollectObject(GameObject currentGameObject)
    {
        if (HitBoxZoom != null && currentGameObject == HitBoxZoom.gameObject && interactifElement.inventory)
        {
            if (ApplyOnCollect)
                soundR.Play();
            InventoryManager.Instance.AddList(gameObject, interactifElement.nameInventory, interactifElement.inventoryTexture, 1, soundR);
            EventManager.instance.CollectObject -= CollectObject;
            if (interactifElement.activateTips && !doOnce)
            {
                doOnce = true;
                TipsManager.instance.changeIndex(interactifElement.indexOfTip);
            }
            gameObject.SetActive(false);

            //Destroy(gameObject);
        }
    }
}
