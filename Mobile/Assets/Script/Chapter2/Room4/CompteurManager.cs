using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompteurManager : MonoBehaviour
{
    public bool[] compteur;
    private int cpt;
  
    public void NotOK(int index)
    {
        if (compteur[index])
        {
             cpt--;
        }
        compteur[index] = false;
    }

    public void CompteurCheck(int index)
    {
        compteur[index] = true;
        cpt++;
        if (compteur.Length ==  cpt)
        {
            Debug.Log("Gagné");
            GetComponentInParent<ObjectHandler>().trialInstantiate = null;
            GetComponentInParent<ObjectHandler>().interactifElement.spawnNewTrial = false;
            Destroy(gameObject);
        }
    }
}
