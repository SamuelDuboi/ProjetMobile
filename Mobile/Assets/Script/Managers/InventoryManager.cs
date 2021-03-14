using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public List<InventoryItem> interactifElementsList = new List<InventoryItem>();


    public GameObject FindObject(string name)
    {
        for (int i = 0; i < interactifElementsList.Count; i++)
        {
            if( interactifElementsList[i].name == name)
            {
                return interactifElementsList[i].gameObject;
            }
        }
        return null;        
    }
    public GameObject FindObject(GameObject gameObject)
    {
        for (int i = 0; i < interactifElementsList.Count; i++)
        {
            if (interactifElementsList[i].gameObject == gameObject)
            {
                return interactifElementsList[i].gameObject;
            }
        }
        return null;
    }
    public void AddList(GameObject elementToAdd, string name, Texture2D image, int number = 1)
    {
        if(interactifElementsList == null)
        {
            interactifElementsList = new List<InventoryItem>();
        }
        foreach (var item in interactifElementsList)
        {
            if(item.name == name)
            {
                item.number+= number;
                break;
            }
        }
        
        interactifElementsList.Add(new InventoryItem(elementToAdd, name, 1, image));
    }

    public void RemoveFromList(string name, int numberToRemove)
    {
        int index = 1000;
        for (int i = 0; i < interactifElementsList.Count; i++)
        {
            if(interactifElementsList[i].name == name)
            {
                if (interactifElementsList[i].number > 1 +numberToRemove)
                {
                    interactifElementsList[i].number-=numberToRemove;
                }
                else
                {
                    index = i;
                    break;
                }
            }
        }
        if(index != 1000)
        {
            interactifElementsList.RemoveAt(index);
        }
    }
    private void Awake()
    {
        CreateSingleton();
    }
}
