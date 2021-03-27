using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcAFeuSetup : ObjectHandler
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
            var gameObject = InventoryManager.Instance.FindObject(partsName[index], out random);
            if (gameObject != null)
            {
                parts[index].SetActive(true);
                index++;
                if (index == partsName.Length)
                {
                    interactifElement.onlyZoom = true;
                    HitBoxZoom.enabled = false;
                }
            }
        }

    }
}
