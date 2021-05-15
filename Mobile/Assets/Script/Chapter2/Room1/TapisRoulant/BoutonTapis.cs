using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BoutonTapis : ObjectHandler
{
    public BarrierManager barrierManager;
    public int index;
    public SoundReader soundReader;
    public override void Start()
    {
        base.Start();
    }

    public override void Interact(GameObject currentGameObject)
    {
        if (HitBoxZoom != null && currentGameObject == HitBoxZoom.gameObject)
        {
            barrierManager.PressButon(index);
            soundReader.Play();
        }
    }
    public void Destry()
    {
        
        EventManager.instance.ZoomOut -= UnZoom;
        EventManager.instance.CollectObject -= CollectObject;
        EventManager.instance.InteractObject -= Interact;
        EventManager.instance.ZoomIn -= MoveCam;
        EventManager.instance.InstantiateTrial -= SpwanTrial;
        EventManager.instance.DestroyTrial -= DestroyTrial;
    }
}
