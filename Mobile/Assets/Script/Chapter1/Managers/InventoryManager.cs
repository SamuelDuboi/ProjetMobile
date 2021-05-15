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
    public Image panel;
    Vector3 initPos;
    public bool Moving;
    public bool isDoingAnim;
   List<int>numberOfAnim;
    int currentIndex;
    private void Start()
    {
        if(inventoryImages.Length>0 && inventoryImages[0]!= null)
        {
            initialSprite = inventoryImages[0].sprite;
            foreach (Image image in inventoryImages)
            {
                image.transform.parent.gameObject.SetActive( false);
            }
        }
        numberOfAnim = new List<int>();
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
                }
                return;
            }
        }
        
        interactifElementsList.Add(new InventoryItem(elementToAdd, name, number, image));
        //add image to ui
        if(image != null && image != default)
        {
            int _index = interactifElementsList.Count - 1;
            interactifElementsList[_index].imageIndex = globalIndex;
            inventoryImages[globalIndex].sprite = image;
             // inventoryImages[globalIndex].SetNativeSize();
            inventoryImages[globalIndex].GetComponentInChildren<TextMeshProUGUI>().text = interactifElementsList[_index].number.ToString();
          
            if (!isDoingAnim)
                StartCoroutine(ItemAnim(globalIndex));
            else
                numberOfAnim.Add(globalIndex);
            globalIndex++;


        }
    }
    IEnumerator ItemAnim(int index)
    {
        if (!inventoryImages[index].transform.parent.gameObject.activeSelf)
            inventoryImages[index].transform.parent.gameObject.SetActive(true);
        isDoingAnim = true;
        inventoryImages[index].gameObject.SetActive(false);
        inventoryImages[index].transform.GetChild(0).gameObject.SetActive(false);
        initPos  = inventoryImages[index].transform.position;
        panel.gameObject.SetActive(true);
        panel.color = new Color(0, 0, 0, 0);
        float timer = 0;
        inventoryImages[index].color = Color.white;
        currentIndex = index;
        while (timer < 0.3f)
        {
            inventoryImages[index].transform.position = new Vector3(Screen.width/2, Screen.height/2);
            panel.color += new Color(0, 0, 0, 0.02f);
            inventoryImages[index].gameObject.SetActive(true);

            inventoryImages[index].transform.localScale += Vector3.one*0.1f;
            timer += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        Moving = true;

        StartCoroutine(WaitToMove(index));
    }
    public void SkipeMove()
    {
        if (Moving)
        {
            StartCoroutine(MoveBack(currentIndex));
        }
        
    }
    IEnumerator WaitToMove(int index)
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(MoveBack(index));
    }
    bool isMovingBack;
    IEnumerator MoveBack(int index)
    {
        if (!isMovingBack)
        {
            Moving = false;

            isMovingBack = true;
            float timer = 0;
            float step = Vector2.Distance(initPos, inventoryImages[index].transform.position) / 30;
            while (timer < 0.3f)
            {

                inventoryImages[index ].transform.position = Vector3.MoveTowards(inventoryImages[index].transform.position, initPos, step);
                panel.color -= new Color(0, 0, 0, 0.02f);
                inventoryImages[index ].gameObject.SetActive(true);
                if(inventoryImages[index ].transform.localScale.x>1)
                    inventoryImages[index].transform.localScale -= Vector3.one * 0.1f;
                timer += 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
            isMovingBack = false;

            panel.gameObject.SetActive(false);
            inventoryImages[index ].transform.GetChild(0).gameObject.SetActive(true);
        }
        isDoingAnim = false;
        if (numberOfAnim.Count > 0)
        {
            StartCoroutine(ItemAnim(numberOfAnim[numberOfAnim.Count - 1]));
            numberOfAnim.RemoveAt(numberOfAnim.Count - 1);
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
                //inventoryImages[i].SetNativeSize();

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
                       //inventoryImages[globalIndex].SetNativeSize();

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
