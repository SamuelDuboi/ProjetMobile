using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : Singleton<InventoryManager>
{
    public List<InventoryItem> interactifElementsList = new List<InventoryItem>();

    public Image[] inventoryImages;
    private int globalIndex;
    private Sprite initialSprite;
    private void Start()
    {
        initialSprite = inventoryImages[0].sprite;
        foreach (Image image in inventoryImages)
        {
            image.transform.parent.gameObject.SetActive( false);
        }
    }
    public GameObject FindObject(string name, out int number)
    {
        for (int i = 0; i < interactifElementsList.Count; i++)
        {
            if( interactifElementsList[i].name == name)
            {
                number = interactifElementsList[i].number;
                return interactifElementsList[i].gameObject;
            }
        }
        number = 0;
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
    public void AddList(GameObject elementToAdd, string name, Sprite image, int number = 1)
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
                if (item.imageIndex != 1000)
                {
                    inventoryImages[item.imageIndex].GetComponentInChildren<TextMeshProUGUI>().text = item.number.ToString();
                    if (!inventoryImages[item.imageIndex].transform.parent.gameObject.activeSelf)
                        inventoryImages[item.imageIndex].transform.parent.gameObject.SetActive(true);
                }
                return;
            }
        }
        
        interactifElementsList.Add(new InventoryItem(elementToAdd, name, 1, image));
        //add image to ui
        if(image != null && image != default)
        {
            int _index = interactifElementsList.Count - 1;
            interactifElementsList[_index].imageIndex = globalIndex;
            inventoryImages[globalIndex].sprite = image;
              inventoryImages[globalIndex].SetNativeSize();
            inventoryImages[globalIndex].GetComponentInChildren<TextMeshProUGUI>().text = interactifElementsList[_index].number.ToString();
            if (!inventoryImages[globalIndex].transform.parent.gameObject.activeSelf)
                inventoryImages[globalIndex].transform.parent.gameObject.SetActive(true);

            globalIndex++;
        }
    }

    public void RemoveFromList(string name, int numberToRemove)
    {
        int index = 1000;
        for (int i = 0; i < interactifElementsList.Count; i++)
        {
            if(interactifElementsList[i].name == name)
            {
                if (interactifElementsList[i].number >numberToRemove)
                {
                    interactifElementsList[i].number-=numberToRemove;
                    if(interactifElementsList[i].imageIndex != 1000)
                    {
                        inventoryImages[interactifElementsList[i].imageIndex].GetComponentInChildren<TextMeshProUGUI>().text = interactifElementsList[i].number.ToString();
                    }
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
            globalIndex = 0;
            for (int i = 0; i < inventoryImages.Length; i++)
            {
                inventoryImages[i].sprite = initialSprite;
                inventoryImages[i].SetNativeSize();

                inventoryImages[i].GetComponentInChildren<TextMeshProUGUI>().text="0";
                inventoryImages[i].transform.parent.gameObject.SetActive(false); 
            }
            foreach (var item in interactifElementsList)
            {
                if (item.imageIndex != 1000)
                {
                    item.imageIndex = globalIndex;
                    inventoryImages[globalIndex].GetComponentInChildren<TextMeshProUGUI>().text = item.number.ToString();
                    inventoryImages[globalIndex].sprite= item.image;
                    inventoryImages[globalIndex].transform.parent.gameObject.SetActive(true);
                       inventoryImages[globalIndex].SetNativeSize();

                    globalIndex++;
                }
            }
        }
    }
    private void Awake()
    {
        CreateSingleton();
    }
}
