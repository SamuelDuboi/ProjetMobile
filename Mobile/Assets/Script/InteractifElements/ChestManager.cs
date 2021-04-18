using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public bool dontDestroyHitBox;
    public Chest[] chests;
    public string initialNumber;
    public string finalNumbers;
    [Range(0, 1)]
    public float sensibilty = 0.5f;
    private bool[] results;
    public bool save;
    void Start()
    {
        for (int i = 0; i < chests.Length; i++)
        {
            chests[i].Init(int.Parse(initialNumber[i].ToString()), sensibilty,i, this);
        }
        results = new bool[chests.Length];
    }

 
    public void TryValidate(int index, int indexInArray)
    {
        if(index == int.Parse(finalNumbers[indexInArray].ToString()))
        {
            results[indexInArray] = true;
        }
        else
        {
            results[indexInArray] = false;
        }
        foreach (var result in results)
        {
            if (!result)
                return;
        }
         GetComponentInParent<ObjectHandler>().trialInstantiate = null;
         GetComponentInParent<ObjectHandler>().interactifElement.spawnNewTrial = false;
        if (dontDestroyHitBox)
        {
             GetComponentInParent<ObjectHandler>().interactifElement.onlyZoom = false;
             GetComponentInParent<ObjectHandler>().HitBoxZoom.enabled= true;
        }
        if (save)
            SaveManager.instance.SaveChapter2();
         GetComponentInParent<ObjectHandler>().Interact(GetComponentInParent<ObjectHandler>().HitBoxZoom.gameObject);
         Destroy(transform.parent.gameObject);
    }
}
