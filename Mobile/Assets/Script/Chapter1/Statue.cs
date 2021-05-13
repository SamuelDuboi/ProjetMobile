using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : ObjectHandler
{
    public GameObject[] parts;
    public string[] partsName;
    private int index = 0;
    public override void Start()
    {
        base.Start();
    }

    public override void Interact(GameObject currentGameObject)
    {
        if (HitBoxZoom != null && currentGameObject == HitBoxZoom.gameObject)
        {
            int random;
            var gameObject = InventoryManager.Instance.FindObject(partsName[index],out random);
            if(gameObject!= null)
            {
                parts[index].SetActive(true);
                if (ApplyOnInteract)
                    soundR.Play();
                InventoryManager.Instance.RemoveFromList(partsName[index], 1);
                index++;
                if(index == partsName.Length)
                {
                    if (interactifElement.activateTips && !doOnce)
                    {
                        doOnce = true;
                        TipsManager.instance.changeIndex( interactifElement.indexOfTip);
                    }
                    InteractActiveObject(true);
                    UnZoom();
                }
            }
        }

     }
}
