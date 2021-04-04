using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcAFeuSetup : ObjectHandler
{
    public GameObject[] objects;
    public string [] names;
    public bool canPutTorche;
    public override void Start()
    {
        base.Start();
    }

    public override void Interact(GameObject currentGameObject)
    {
        if (HitBoxZoom != null && currentGameObject == HitBoxZoom.gameObject)
        {
            int random;
            if (!canPutTorche)
            {
                var gameObject = InventoryManager.Instance.FindObject(names[0], out random);
                if (gameObject != null)
                {
                    InventoryManager.Instance.RemoveFromList(names[0], 1);
                    objects[0].SetActive(true);
                    interactifElement.onlyZoom = true;
                    HitBoxZoom.enabled = false;
                }
            }

            else
            {
                var gameObject = InventoryManager.Instance.FindObject(names[1], out random);
                if (gameObject != null)
                {
                    InventoryManager.Instance.RemoveFromList(names[1], 1);
                    objects[1].SetActive(true);
                    interactifElement.onlyZoom = true;
                    HitBoxZoom.enabled = false;
                }
            }
        }

    }


    public void CanPutTorche()
    {
        interactifElement.onlyZoom = false;
        HitBoxZoom.enabled = true;
        canPutTorche = true;
    }

}
