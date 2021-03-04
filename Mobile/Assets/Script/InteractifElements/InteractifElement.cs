using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractifElement : MonoBehaviour
{
    public Animator interactionAnimator;
    public bool isLInkedToWall;
    public GameObject wallLinked;


    public bool zoom;
    public bool zoomFromUp;


    public List<CamDirection> leftCam = new List<CamDirection>();
    public List<CamDirection> rightCam = new List<CamDirection>();

    public bool hasLinkGameObject;
    public List<GameObject> ObjectoOpen;


    public bool spawnNewTrial;
    public GameObject TrialGameObjects;

    public List<GameObject> objectToActive;
    public List<GameObject> objectToInteract;

    public bool inventory;
    public bool needToBeAssemble;
    public GameObject otherAssemblingObject;
    public GameObject assembledGameObjetc;
    public void AddList<T>(List<T> list)
    {
        if (list == null)
        {
            list = new List<T>();
        }

        list.Add(default);
    }

    public void RemoveFromList<T>(List<T> list)
    {
        list.RemoveAt(list.Count - 1);
    }
}


