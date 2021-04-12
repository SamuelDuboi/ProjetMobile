using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateStick : ObjectHandler
{
    public WaterStick waterStick;
    public IEnumerator ActivateWaterStick()
    {
        interactifElement.onlyZoom = true;
        HitBoxZoom.enabled = false;
        waterStick.Interact(waterStick.HitBoxZoom.gameObject);
        yield return new WaitForSeconds(waterStick.interactifElement.interactionAnimator.GetCurrentAnimatorClipInfo(0).Length);
        UnZoom();
        HitBoxZoom.enabled = false;
    }
}
