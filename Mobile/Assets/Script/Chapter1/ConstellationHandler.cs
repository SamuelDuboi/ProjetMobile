using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationHandler : ObjectHandler
{
    public EnigmeCristaux enigmeCristaux;
    public override void Interact(GameObject currentGameObject)
    {
        if (HitBoxZoom != null && currentGameObject == HitBoxZoom.gameObject)
        {
            enigmeCristaux.enabled = true;
            enigmeCristaux.gameObject.layer = default;
        }
        base.Interact(currentGameObject);
        
    }
    public override void UnZoom()
    {
        base.UnZoom();
        enigmeCristaux.enabled = false;
        enigmeCristaux.gameObject.layer = 8;
    }
   
}
