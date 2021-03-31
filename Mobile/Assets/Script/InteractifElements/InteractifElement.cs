using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractifElement : MonoBehaviour
{
    public Animator interactionAnimator;
    public bool isLInkedToWall;
    public GameObject wallLinked;


    public bool zoom;
    public bool onlyZoom;


    public List<Cams> cams = new List<Cams>();
    public float orthoGraphicSize = 3;

    public bool hasLinkGameObject;
    public List<string> ObjectoOpen;


    public bool spawnNewTrial;
    public GameObject TrialGameObjects;

    public List<GameObject> objectToActive;
    public List<GameObject> objectToInteract;

    public bool inventory;
    public string nameInventory;
    public Texture2D inventoryTexture;
    public bool needToBeAssemble;
    public GameObject otherAssemblingObject;
    public GameObject assembledGameObjetc;

    public bool popup, popInteract, PopupAfterAnim;
    public string text;
    public float timePopup;
    public void AddList<T>(List<T> list)
    {
        if (list == null)
        {
            list = new List<T>();
        }

        list.Add(default);
    }

    public void AddList(List<Cams> list)
    {
        if (list == null)
        {
            list = new List<Cams>();
        }

        list.Add( new Cams());
    }
    public void RemoveFromList<T>(List<T> list)
    {
        list.RemoveAt(list.Count - 1);
    }
}

[System.Serializable]
public class Cams
{
    [SerializeField] public CamDirection camDirection ;
    [SerializeField] public GameObject cam ;
    [SerializeField] public GameObject current ;
    public Cams()
    {
        camDirection = CamDirection.SouthEast;
        cam = null;
        current = null;
    }
}



