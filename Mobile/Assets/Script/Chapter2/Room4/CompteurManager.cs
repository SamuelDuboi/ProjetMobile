using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompteurManager : MonoBehaviour
{
    public bool[] compteur;
    private int cpt;
    private void Start()
    {
        EventManager.instance.ZoomOut += Unzoom;
    }
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
            EventManager.instance.ZoomOut -= Unzoom;
            _objectHandler.interactifElement.spawnNewTrial = false;
            _objectHandler.Interact(GetComponentInParent<ObjectHandler>().HitBoxZoom.gameObject);
          //  EventManager.instance.OnZoomOut();
            Destroy(gameObject);
        }
    }
    private void Unzoom()
    {
        var parent = GetComponentInParent<ObjectHandler>();
        if (parent)
        {
            parent.interactifElement.onlyZoom = false;
            parent.HitBoxZoom.enabled = true;
            parent.interactifElement.spawnNewTrial = true;
            EventManager.instance.ZoomOut -= Unzoom;
            Destroy(parent.trialInstantiate);
        }
    }
}
