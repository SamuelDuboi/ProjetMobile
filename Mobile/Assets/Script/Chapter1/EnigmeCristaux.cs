using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmeCristaux : MonoBehaviour
{
    public GameObject[] cristaux;
    public bool[] cristalActif;
    public bool[] lockCristaux;
    public bool constellation1;
    public bool constellation4;

    public int countCristaux;
    public int inventoryCristaux;

    public int index = 1000;
    public string cristalName;
    public Sprite inventorySprite;

    public SoundReader sound;
    public ObjectHandler exitDoor;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.InteractObject += Cristaux;
        InventoryManager.Instance.FindObject(cristalName, out inventoryCristaux);        
        for (int i = 0; i < lockCristaux.Length; i++)
        {
            lockCristaux[i] = false;
        }
    }
    
    void Cristaux(GameObject gameObject)
    {
        InventoryManager.Instance.FindObject(cristalName, out inventoryCristaux);
        
        for (int i = 0; i < cristaux.Length; i++)
        {
            if (cristaux[i] == gameObject)
            {
                index = i;
                break;
            }
            else 
                index = 1000;
                
        }
        if(index != 1000)
        {
            sound.Play();
            if (cristalActif[index] == true && lockCristaux[index] == false)
            {
                cristaux[index].GetComponent<MeshRenderer>().enabled = false;
                cristalActif[index] = false;
                inventoryCristaux += 1;
                InventoryManager.Instance.AddList(cristaux[index], cristalName, inventorySprite, 1,sound);
            }
            else if (cristalActif[index] == false && lockCristaux[index] == false && inventoryCristaux > 0)
            {
                cristaux[index].GetComponent<MeshRenderer>().enabled = true;
                cristalActif[index] = true;
                inventoryCristaux -= 1;
                InventoryManager.Instance.RemoveFromList(cristalName,1);
            }
            Constellation1Check();
            Constellation4Check();
            if (constellation1 && constellation4)
            {
                exitDoor.interactifElement.interactionAnimator.SetTrigger("Interact");
                EventManager.instance.OnZoomOut();
                SaveManager.instance.SaveChapter1();
                this.enabled = false;
            }
            index = 1000;
        }
        
    }

    void Constellation1Check()
    {
        if (index >= 0 && index <= 3)
        {
            for (int i = 0; i < 4; i++)
            {
                if (cristalActif[i])
                {
                    countCristaux++;
                }
                else
                {
                    countCristaux = 0;
                    break;
                }
            }
            if (countCristaux == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    lockCristaux[i] = true;
                    constellation1 = true;
                }
                countCristaux = 0;
            }
        }
    }

    void Constellation4Check()
    {
        if (index >= 14 && index <= 17)
        {
            for (int i = 14; i < 18; i++)
            {
                if (cristalActif[i])
                {
                    countCristaux++;
                }
                else
                {
                    countCristaux = 0;
                    break;
                }
            }
            if (countCristaux == 4)
            {
                for (int i = 14; i < 18; i++)
                {
                    lockCristaux[i] = true;
                    constellation4 = true;
                }
                countCristaux = 0;
            }
        }
    }
    
}
