using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOnTapis : ObjectHandler
{
    public LevelManager levelManager;
    public WaterStick redStick;
    public BoutonTapis[] scriptToActive;
    public MonoBehaviour barrier;
    public bool done;
    public override void Start()
    {
        base.Start();
        levelManager.zoomOnTapis = this;
        levelManager.enabled = false;
        foreach (var objectToActive in scriptToActive)
        {
            objectToActive.enabled = false;
        }
        barrier.enabled = false;
    }

    public override Cams Zoom(CamDirection currentDirection)
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
                    if (!done)
                    {
                        foreach (var objectToActive in scriptToActive)
                        {
                            objectToActive.enabled = true;
                        }
                        levelManager.enabled = true;
                    }
                    

                    return cams;
                }
            }
        }
        return null;
    }


    public override void UnZoom()
    {
        if (isZoomed && !done)
        {
            foreach (var objectToActive in scriptToActive)
            {
                objectToActive.enabled = false;
            }
            levelManager.enabled = false;

        }
        base.UnZoom();

    }

    public void StartActivateStick()
    {
        StartCoroutine(ActivateStick());
    }
    public IEnumerator  ActivateStick()
    {
        Cams cams = null;
        float orthographicSize = redStick.interactifElement.orthoGraphicSize;
        redStick.ChoseToZoom(out cams);
        if (cams != null)
            EventManager.instance.OnZoomIn(cams, orthographicSize, redStick.gameObject);
        yield return new WaitForSeconds(0.5f);
        redStick.Interact(redStick.HitBoxZoom.gameObject);
        yield return new WaitForSeconds(redStick.interactifElement.interactionAnimator.GetCurrentAnimatorClipInfo(0).Length);
        UnZoom();
        EventManager.instance.OnZoomOut(); 
        HitBoxZoom.enabled = false;
    }

    public void Destry()
    {
        foreach (var bouton in scriptToActive)
        {
            bouton.Destry();
        }
        EventManager.instance.ZoomOut -= UnZoom;
        EventManager.instance.CollectObject -= CollectObject;
        EventManager.instance.InteractObject -= Interact;
        EventManager.instance.ZoomIn -= MoveCam;
        EventManager.instance.InstantiateTrial -= SpwanTrial;
        EventManager.instance.DestroyTrial -= DestroyTrial;
    }
}
