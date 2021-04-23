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

            // to anyone who read, I'm sorry for your eyes loss but I'm super lazy
            var _objectHandler = GetComponentInParent<ObjectHandler>();
            _objectHandler.trialInstantiate = null;
            EventManager.instance.OnDestroyTrial();

            _objectHandler.interactifElement.spawnNewTrial = false;
            _objectHandler.Interact(GetComponentInParent<ObjectHandler>().HitBoxZoom.gameObject);
          //  EventManager.instance.OnZoomOut();
            Destroy(gameObject);
        }
    }
}
