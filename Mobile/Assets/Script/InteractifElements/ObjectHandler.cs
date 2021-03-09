﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    [HideInInspector] public InteractifElement interactifElement;

    public BoxCollider hitBoxDezoom;
    public BoxCollider HitBoxZoom;

    private bool isZoomed;

   [HideInInspector]public GameObject trialInstantiate;

  public  virtual void Start()
    {
        interactifElement = GetComponent<InteractifElement>();
        if (interactifElement.isLInkedToWall)
            transform.SetParent(interactifElement.wallLinked.transform);

        EventManager.instance.ZoomOut += UnZoom;
        EventManager.instance.CollectObject += CollectObject;
        EventManager.instance.InteractObject += Interact;

        if (interactifElement.zoom)
            HitBoxZoom.gameObject.layer = 8;
        else           
            HitBoxZoom.gameObject.layer = 9;

        if (interactifElement.inventory)
            HitBoxZoom.gameObject.tag = "Collectable";

        if (interactifElement.objectToInteract != null || interactifElement.objectToInteract.Count != 0)
        {
        }

        InteractActiveObject(false);

    }


    public void ChoseToZoom( out float direction)
    {
        direction = Zoom(EventManager.instance.cuurrentCamDirection);
        if (direction != default)
        {
            hitBoxDezoom.gameObject.SetActive(true);
            if (interactifElement.onlyZoom)
            {
                HitBoxZoom.enabled = false;
            }
            else
            HitBoxZoom.gameObject.layer = 9;
            isZoomed = true;
        }
    }
    public float Zoom(CamDirection currentDirection)
    {
        if (!isZoomed)
        {
            if (interactifElement.zoomFromUp )
                if (!EventManager.instance.uspideDown)
                    return 2f;
                else
                    return -2f;
            else if (interactifElement.zoomFromDown)
            {
                if (!EventManager.instance.uspideDown)
                    return -2f;
                else
                    return 2f;
            }

            foreach (var direction in interactifElement.leftCam)
            {
                if (currentDirection == direction)
                {
                    return -1f;
                }
            }
            foreach (var direction in interactifElement.rightCam)
            {
                if (currentDirection == direction)
                {
                    return 1f;
                }
            }
        }


        return default;
    }

    private void UnZoom()
    {
        if (isZoomed)
        {
            hitBoxDezoom.gameObject.SetActive(false);
            if (interactifElement.onlyZoom)
            {
                HitBoxZoom.enabled = true;
            }
            HitBoxZoom.gameObject.layer = 8;
            isZoomed = false;
            Destroy(trialInstantiate);
        }
    }


    public virtual void Interact(GameObject currentGameObject)
    {
       
        if(HitBoxZoom != null && currentGameObject == HitBoxZoom.gameObject )
        {
            if (interactifElement.popInteract)
            {
                EventManager.instance.OnPopup(interactifElement.text, interactifElement.timePopup);
            }
            InteractActiveObject(true);

            if(interactifElement.hasLinkGameObject)
            {
                int numberOfObject = 0;
                foreach (var curentGamObject in interactifElement.ObjectoOpen)
                {
                    if (InventoryManager.Instance.interactifElementsList.Contains(curentGamObject))
                        numberOfObject++;

                }
                if (numberOfObject != interactifElement.ObjectoOpen.Count)
                    return;
            }
            if (!interactifElement.spawnNewTrial)
            {
                interactifElement.interactionAnimator.SetTrigger("Interact");
                if (interactifElement.PopupAfterAnim)
                {
                    StartCoroutine(WaitToPopUp(interactifElement.interactionAnimator.GetCurrentAnimatorClipInfo(0).Length));
                }
            }
            else if(trialInstantiate == null)
            {
               trialInstantiate = Instantiate(interactifElement.TrialGameObjects, Camera.main.transform.position,Quaternion.identity);
                trialInstantiate.transform.SetParent(transform);
                interactifElement.spawnNewTrial = false;
            }
        }
    }
    public virtual void CollectObject(GameObject currentGameObject)
    {
        if (currentGameObject == HitBoxZoom.gameObject && interactifElement.inventory)
        {
            InventoryManager.Instance.AddList(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void InteractActiveObject(bool setActive)
    {

        if (interactifElement.objectToActive != null)
        {
            foreach (var gameObject in interactifElement.objectToActive)
            {
                gameObject.SetActive(setActive);
            }
        }
    }

    private IEnumerator WaitToPopUp(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        EventManager.instance.OnPopup(interactifElement.text, interactifElement.timePopup);
    }
}
