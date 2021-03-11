using UnityEngine;

public class InventoryItem 
{
    public GameObject gameObject;
    public string name;
    public int number;
    public Texture2D image;
    public InventoryItem(GameObject gameObject, string name, int number, Texture2D image)
    {
        this.gameObject = gameObject;
        this.name = name;
        this.number = number;
        this.image = image;
    }
}
