using UnityEngine;

public class InventoryItem 
{
    public GameObject gameObject;
    public string name;
    public int number;
    public Sprite image;
    public int imageIndex = 1000;
    public InventoryItem(GameObject gameObject, string name, int number, Sprite image)
    {
        this.gameObject = gameObject;
        this.name = name;
        this.number = number;
        this.image = image;
    }
}
