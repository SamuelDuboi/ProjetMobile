using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcAFeuSetup : ObjectHandler
{
    public GameObject[] objects;
    public string [] names;
    public bool canPutTorche;
    public LightManager lightManager; 
    public override void Start()
    {
        base.Start();
        EventManager.instance.LightObject += OnLightOn;
    }

    public override void Interact(GameObject currentGameObject)
    {
        if (HitBoxZoom != null && currentGameObject == HitBoxZoom.gameObject)
        {
            int random;
            if (canPutTorche)
            {
               var gameObject = InventoryManager.Instance.FindObject(names[0], out random);
                lightManager.ignoreZoom = false;
                if (gameObject != null)
                {
                    InventoryManager.Instance.RemoveFromList(names[0], 1);
                    objects[0].SetActive(true);
                    interactifElement.onlyZoom = true;
                    HitBoxZoom.enabled = false;
                }
            }
        }

    }

    public override Cams Zoom(CamDirection currentDirection)
    {
        return base.Zoom(currentDirection);
    }

    public bool doOnceLight;
    public void OnLightOn(GameObject currentObject)
    {

        if (gameObject == currentObject.GetComponentInParent<ObjectHandler>().gameObject)
        {

            if (!doOnceLight)
            {
                doOnceLight = true;
                interactifElement.onlyZoom = false;
                HitBoxZoom.enabled = true;
                canPutTorche = true;
                interactifElement.interactionAnimator.SetInteger("FlammeNumber",4);
            }
        }
    }
}
