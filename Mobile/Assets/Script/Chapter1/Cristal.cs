using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : ObjectHandler
{
    public int rotation;
    private int currentRotation;
    [HideInInspector] public bool isGood;
    float multiplicator = 1;
    public override void Start()
    {
        EventManager.instance.InteractObject += Interact;
        transform.Rotate(new Vector3(0, 0, rotation*45));
        if (rotation == 0)
            isGood = true;
    }
    public override void Interact(GameObject currentGameObject)
    {
        if ( currentGameObject == gameObject)
        { 
            
            currentRotation += (int)multiplicator;
            transform.Rotate(new Vector3(0, 0, -90));
            if (currentRotation == rotation)
            {
                isGood = true;
                multiplicator = -1;
            }
            else
            {
                isGood = false;
                if (currentRotation == 0)
                    multiplicator = 1;
            }
        }

    }
}
