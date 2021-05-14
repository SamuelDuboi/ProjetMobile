using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : ObjectHandler
{
    public int rotation;
    private int currentRotation;
    [HideInInspector] public bool isGood;
    public Cristal previusCristal;
    public Material lightedMat;
    private Material initMat;
    public override void Start()
    {
        initMat = GetComponent<MeshRenderer>().material;
        EventManager.instance.InteractObject += Interact;
        if (rotation == 0)
            isGood = true;
    }
    public override void Interact(GameObject currentGameObject)
    {
        if ( currentGameObject == gameObject)
        {
            soundR.Play();
            currentRotation += 1;
            if (currentRotation == 4)
                currentRotation = 0;
            transform.Rotate(new Vector3(0, 0, -90)); 
            if (currentRotation == rotation)
            {

                if (previusCristal != null)
                    if (!previusCristal.isGood)
                    {
                        GetComponent<MeshRenderer>().material = initMat;
                        return;
                    }
                GetComponent<MeshRenderer>().material = lightedMat;
                isGood = true;
            }
            else
            {
                isGood = false;
                GetComponent<MeshRenderer>().material = initMat;

            }
        }

    }
}
