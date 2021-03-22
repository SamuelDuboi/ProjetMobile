using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmeCristaux : MonoBehaviour
{
    public GameObject[] cristaux;
    public bool[] cristalActif;
    public bool[] lockCristaux;
    public bool constellation2;
    public bool constellation3;
    public bool constellation6;

    public int countCristaux;
    public int inventoryCristaux;

    public int index = 1000;
    public string cristalName;
    public Texture2D texture2D;

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
        }
        if(index != 1000)
        {
            if (cristalActif[index] == true && lockCristaux[index] == false)
            {
                cristaux[index].GetComponent<MeshRenderer>().enabled = false;
                cristalActif[index] = false;
                inventoryCristaux += 1;
                InventoryManager.Instance.AddList(cristaux[index], cristalName, texture2D);
            }
            else if (cristalActif[index] == false && lockCristaux[index] == false && inventoryCristaux > 0)
            {
                cristaux[index].GetComponent<MeshRenderer>().enabled = true;
                cristalActif[index] = true;
                inventoryCristaux -= 1;
                InventoryManager.Instance.RemoveFromList(cristalName,1);
            }
            Constellation2Check();
            Constellation3Check();
            Constellation6Check();
            if (constellation2 && constellation3 && constellation6)
            {
                Debug.Log("Gagné");
                this.enabled = false;
            }
            index = 1000;
        }
        
    }

    void Constellation2Check()
    {
        if (index >= 7 && index <= 11)
        {
            for (int i = 7; i < 12; i++)
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
            if (countCristaux == 5)
            {
                for (int i = 7; i < 12; i++)
                {
                    lockCristaux[i] = true;
                    constellation2 = true;
                }
                countCristaux = 0;
            }
        }
    }

    void Constellation3Check()
    {
        if (index >= 12 && index <= 18)
        {
            for (int i = 12; i < 19; i++)
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
            if (countCristaux == 7)
            {
                for (int i = 12; i < 19; i++)
                {
                    lockCristaux[i] = true;
                    constellation3 = true;
                }
                countCristaux = 0;
            }
        }
    }
    void Constellation6Check()
    {
        if (index >= 38 && index <= 44)
        {
            for (int i = 38; i < 45; i++)
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
            if (countCristaux == 7)
            {
                for (int i = 38; i < 45; i++)
                {
                    lockCristaux[i] = true;
                    constellation3 = true;
                }
                countCristaux = 0;
            }
        }
    }
}
