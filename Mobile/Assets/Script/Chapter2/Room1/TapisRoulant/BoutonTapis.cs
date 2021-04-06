using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BoutonTapis : ObjectHandler
{
    public BarrierManager barrierManager;
    public int index;

    public override void Start()
    {
        base.Start();
    }

    public override void Interact(GameObject currentGameObject)
    {
        if (HitBoxZoom != null && currentGameObject == HitBoxZoom.gameObject)
        {
            barrierManager.PressButon(index);
        }
    }
}
