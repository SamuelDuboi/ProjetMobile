using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollRoom2 : ObjectHandler
{
    public GameObject baseSalle;
    public override void Interact(GameObject currentGameObject)
    {
        if (HitBoxZoom != null && currentGameObject == HitBoxZoom.gameObject)
        {

            if (!interactifElement.spawnNewTrial)
            {
                if (ApplyOnInteract)
                    soundR.Play();
                interactifElement.interactionAnimator.SetTrigger("Interact");
                StartCoroutine(ChangeParent());
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
            
        }
    }

    public IEnumerator ChangeParent()
    {
        yield return new WaitForSeconds(1.5f);
        transform.SetParent(baseSalle.transform);

    }
}
