using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateStick : ObjectHandler
{
    public WaterStick waterStick;
    public bool isGreen;
   

    public void ActivateStickLunch()
    {
        StartCoroutine(ActivateWaterStick());
    }
    public IEnumerator ActivateWaterStick()
    {
        interactifElement.onlyZoom = true;
        HitBoxZoom.enabled = false;
        Cams cams = null;
        float orthographicSize = waterStick.interactifElement.orthoGraphicSize;
        waterStick.ChoseToZoom(out cams);
        if (cams != null)
            EventManager.instance.OnZoomIn(cams, orthographicSize, waterStick.gameObject);
        yield return new WaitForSeconds(0.5f);
        waterStick.Interact(waterStick.HitBoxZoom.gameObject);
        yield return new WaitForSeconds(waterStick.interactifElement.interactionAnimator.GetCurrentAnimatorClipInfo(0).Length);
        EventManager.instance.OnZoomOut();
        UnZoom();
        HitBoxZoom.enabled = false;
    }



}
