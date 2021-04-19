using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    [HideInInspector] public InteractifElement interactifElement;
    public Collider HitBoxZoom;

  [HideInInspector] public bool isZoomed;

    [HideInInspector] public GameObject trialInstantiate;
    public string NameToAddIfAnimToAdd;
  public  virtual void Start()
    {
        interactifElement = GetComponent<InteractifElement>();
        if (interactifElement.isLInkedToWall)
            transform.SetParent(interactifElement.wallLinked.transform);

        EventManager.instance.ZoomOut += UnZoom;
        EventManager.instance.CollectObject += CollectObject;
        EventManager.instance.InteractObject += Interact;
        EventManager.instance.ZoomIn += MoveCam;

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

    private void MoveCam(Cams cams, float orthogrphicSize)
    {

        if (cams != null)
        {
            if(HitBoxZoom != null && HitBoxZoom.gameObject != null)
            HitBoxZoom.gameObject.layer = 9;
        }

    }

    public void ChoseToZoom( out Cams cams)
    {
        cams = Zoom(EventManager.instance.cuurrentCamDirection);
        
        if (cams != null )
        {
            if (interactifElement.onlyZoom)
            {
                HitBoxZoom.enabled = false;
            }
            else
            HitBoxZoom.gameObject.layer = 9;
            isZoomed = true;
        }
    }
    public virtual Cams Zoom(CamDirection currentDirection)
    {
        if (!isZoomed)      
        {          
            foreach (var cams in interactifElement.cams)
            {
                if (currentDirection == cams.camDirection)
                {
                    if (interactifElement.UpsideDown)
                        cams.upsideDown = true;
                    cams.current = gameObject;
                    return cams;
                }
            }
        }
        return null;

    }

    public virtual void UnZoom()
    {
        if (isZoomed)
        {
            if (interactifElement.onlyZoom)
            {
                HitBoxZoom.enabled = true;
            }
            HitBoxZoom.gameObject.layer = 8;
            isZoomed = false;
            if(trialInstantiate != null)
            {
                interactifElement.onlyZoom = false;
                HitBoxZoom.enabled = true;
                interactifElement.spawnNewTrial = true;
                Destroy(trialInstantiate);
            }
           
        }
        if (interactifElement.zoom)
        {
            if(HitBoxZoom != null && HitBoxZoom.gameObject != null)
            HitBoxZoom.gameObject.layer = 8;
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
           

            if(interactifElement.hasLinkGameObject)
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
            }
            InteractActiveObject(true);
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
                interactifElement.onlyZoom = true;
                HitBoxZoom.enabled = false;
                interactifElement.spawnNewTrial = false;
                trialInstantiate = Instantiate(interactifElement.TrialGameObjects, Camera.main.transform.position,Quaternion.identity);

                    trialInstantiate.transform.SetParent(transform);
            }
        }
    }
    public virtual void CollectObject(GameObject currentGameObject)
    {
        if (currentGameObject == HitBoxZoom.gameObject && interactifElement.inventory)
        {
            InventoryManager.Instance.AddList(gameObject, interactifElement.nameInventory, interactifElement.inventoryTexture);
            EventManager.instance.CollectObject -= CollectObject;
            gameObject.SetActive(false);

            //Destroy(gameObject);
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

    public IEnumerator WaitToPopUp(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        EventManager.instance.OnPopup(interactifElement.text, interactifElement.timePopup);
    }
}
