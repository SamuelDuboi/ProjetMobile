using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandlerActiveWater : ObjectHandler
{
    public TheWaterManager waterManager;

    private bool doOnceWater;
    public bool ActiveSoundOnInteract;
    public bool Final;
    override public void Interact(GameObject currentGameObject)
    {

        if (HitBoxZoom != null && currentGameObject == HitBoxZoom.gameObject)
        {
            if (Final)
            {
                FinalInteract();
                return;
            }
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
                EventManager.instance.OnInstantiateTrial();
                interactifElement.spawnNewTrial = false;
                trialInstantiate = Instantiate(interactifElement.TrialGameObjects, Camera.main.transform.position, Quaternion.identity);
                trialInstantiate.transform.SetParent(transform);
                if (ActiveSoundOnInteract)
                    soundR.Play();
                return;
            }
            if (!doOnceWater)
            {
                doOnceWater = true;
                interactifElement.interactionAnimator.SetTrigger("Interact");
                if (ActiveSoundOnInteract)
                    soundR.Play();

                if (interactifElement.objectToActive.Count > 0)
                {
                    interactifElement.objectToActive[0].SetActive(true);
                }
                StartCoroutine(WaterACtiveAfterAnim());
            }
        }
    }
    private void FinalInteract()
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
            foreach (var curentGamObject in interactifElement.ObjectoOpen)
            {
                int number;
                var inventoryItem = InventoryManager.Instance.FindObject(curentGamObject, out number);
                if (inventoryItem)
                {
                    InventoryManager.Instance.RemoveFromList(curentGamObject, 1);
                }
            }
            interactifElement.interactionAnimator.SetLayerWeight(1, 0);
            interactifElement.interactionAnimator.SetLayerWeight(2, 1);
            interactifElement.interactionAnimator.SetTrigger("Interact");
            interactifElement.hasLinkGameObject = false;
            InteractActiveObject(true);
            EventManager.instance.OnZoomOut();
            StartCoroutine(WaterACtiveAfterAnim());
        }

    }
    IEnumerator WaterACtiveAfterAnim()
    {
        yield return new WaitForSeconds(1f);
        waterManager.WaterUp();
        yield return new WaitForSeconds(1f);
        soundR.StopSound();

    }
}
