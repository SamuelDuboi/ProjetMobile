﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWaterManager : MonoBehaviour
{
    public static TheWaterManager instance;


    public Animator waterAnim;
    public int currentLevel = 0;
    public GameObject water;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            Debug.LogError("TheWaterManager has been destroy");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.SwipeUp += UpsideDown;
        EventManager.instance.ZoomIn += ZoomOn;
        EventManager.instance.ZoomOut += ZoomOut;
    }

    public virtual void UpsideDown(bool up)
    {
        waterAnim.SetTrigger("UpsideDown");
    }

    public void WaterUp()
    {
        currentLevel++;
        waterAnim.SetInteger("Level", currentLevel);
    }
    private void ZoomOn(Cams cam, float value)
    {
        water.layer = 9;
    }
    private void ZoomOut()
    {
        water.layer = 8;
    }
}
