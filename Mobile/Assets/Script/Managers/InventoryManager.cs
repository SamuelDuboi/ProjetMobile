using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public List<GameObject> interactifElementsList = new List<GameObject>();

    public void AddList(GameObject elementToAdd)
    {
        if(interactifElementsList == null)
        {
            interactifElementsList = new List<GameObject>();
        }

        interactifElementsList.Add(elementToAdd);
    }

    public void RemoveFromList(GameObject elementToRemove)
    {
        if (interactifElementsList.Contains(elementToRemove))
        {
            interactifElementsList.Remove(elementToRemove);
        }
    }
    private void Awake()
    {
        CreateSingleton();
    }
}
