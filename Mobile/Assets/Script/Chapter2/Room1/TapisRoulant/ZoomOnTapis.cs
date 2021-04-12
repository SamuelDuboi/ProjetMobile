using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOnTapis : ObjectHandler
{
    public LevelManager levelManager;
    public WaterStick redStick;
    public MonoBehaviour[] scriptToActive;
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

    public IEnumerator  ActivateStick()
    {
        redStick.Interact(redStick.HitBoxZoom.gameObject);
        yield return new WaitForSeconds(redStick.interactifElement.interactionAnimator.GetCurrentAnimatorClipInfo(0).Length);
        UnZoom();
        HitBoxZoom.enabled = false;
    }
}
