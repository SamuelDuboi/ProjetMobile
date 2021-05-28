using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
public class TipsManager : MonoBehaviour
{
    public static TipsManager instance;
    private int index;
    public string[] text;
    public Sprite[] images;
    public TextMeshProUGUI textRenderer;
    public Image image;

    private List<int> indexes = new List<int>();
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Instance of tips already exist");
            Destroy(gameObject);
        }
    }

    public void changeIndex(int _index)
    {
        if(_index== index + 1)
        {
            index = _index;
            for (int i = 0; i < indexes.Count; i++)
            {
                if( index+1 == indexes[i])
                {
                    index = indexes[i] ;
                }
            }
        }
        else
        {
            indexes.Add(_index);
            indexes.Sort();
        }
        
    }
    public void DisplayText()
    {
        textRenderer.text = text[index];
        if(images[index] != default)
        {
            image.gameObject.SetActive(true);
            image.sprite = images[index];
            image.SetNativeSize();
        }
        else
        {
            image.gameObject.SetActive(false);
        }
    }

}
