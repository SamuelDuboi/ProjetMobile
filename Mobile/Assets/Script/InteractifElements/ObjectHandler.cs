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
    private int tempLayer;
    [HideInInspector] public bool doOnce;
    public SoundReader soundR;
    public bool ApplyOnInteractIfHAsObject;
    public bool ApplyOnInteract;
    public bool ApplyOnCollect;
  public  virtual void Start()
    {
        interactifElement = GetComponent<InteractifElement>();
        if (interactifElement.isLInkedToWall)
            transform.SetParent(interactifElement.wallLinked.transform);

        EventManager.instance.ZoomOut += UnZoom;
        EventManager.instance.CollectObject += CollectObject;
        EventManager.instance.InteractObject += Interact;
        EventManager.instance.ZoomIn += MoveCam;
        EventManager.instance.InstantiateTrial += SpwanTrial;
        EventManager.instance.DestroyTrial+= DestroyTrial;

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

    public void SpwanTrial()
    {
        if(HitBoxZoom != null)
        {
            tempLayer = HitBoxZoom.gameObject.layer;
            HitBoxZoom.gameObject.layer = 0;
        }
    }
    public void DestroyTrial()
    {
        if (HitBoxZoom != null)
        {
            if (interactifElement.zoom)
                HitBoxZoom.gameObject.layer = 8;
            else
            HitBoxZoom.gameObject.layer = tempLayer;
        }
    }

    public void MoveCam(Cams cams, float orthogrphicSize,GameObject currentObejct)
    {

        if (cams != null  )
        {
            if (currentObejct == null)
                Debug.Log(gameObject.name);
            if(currentObejct == gameObject)
            {
                if(HitBoxZoom != null && HitBoxZoom.gameObject != null)
                    HitBoxZoom.gameObject.layer = 9;
            }
            else
            {
                if (interactifElement.zoom)
                {
                    HitBoxZoom.gameObject.layer = 8;
                    isZoomed = false;
                    if (interactifElement.onlyZoom)
                        HitBoxZoom.enabled = true;
                }
            }
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
                EventManager.instance.OnDestroyTrial();
                HitBoxZoom.enabled = true;
                interactifElement.spawnNewTrial = true;                
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
                interactifElement.interactionAnimator.SetTrigger("Interact");
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
            else if(trialInstantiate == null)
            {
                interactifElement.onlyZoom = true;
                HitBoxZoom.enabled = false;
                interactifElement.spawnNewTrial = false;
                trialInstantiate = Instantiate(interactifElement.TrialGameObjects, Camera.main.transform.position,Quaternion.identity);
                EventManager.instance.OnInstantiateTrial();
                    trialInstantiate.transform.SetParent(transform);
            }
        }
    }
    public virtual void CollectObject(GameObject currentGameObject)
    {
        if (HitBoxZoom != null&& currentGameObject == HitBoxZoom.gameObject && interactifElement.inventory)
        {
            if (ApplyOnCollect)
                soundR.Play();
            InventoryManager.Instance.AddList(gameObject, interactifElement.nameInventory, interactifElement.inventoryTexture, 1,soundR);
            EventManager.instance.CollectObject -= CollectObject;
            if (interactifElement.activateTips && !doOnce)
            {
                doOnce = true;
                TipsManager.instance.changeIndex( interactifElement.indexOfTip);
            }
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
