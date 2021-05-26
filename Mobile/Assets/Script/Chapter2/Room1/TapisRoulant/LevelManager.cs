﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float speed;
    private GameObject currentObject;
    public GameObject box;

    [HideInInspector] public ZoomOnTapis zoomOnTapis;

    public string name;


    // Update is called once per frame
    void Update()
    {
        if(currentObject == null)
        {
            currentObject = Instantiate(box, transform);
        }
    }

    public void ChangeSpeed (float speed)
    {
        this.speed = speed;
    }

    public void DestroyAll()
    {
        zoomOnTapis.Destry();
        Destroy(zoomOnTapis.barrier);
        zoomOnTapis.interactifElement.onlyZoom = true;
        zoomOnTapis.HitBoxZoom.enabled = false;
        zoomOnTapis.done = true;
        zoomOnTapis.StartActivateStick();
        Destroy(this);
    }
}
