﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : ObjectHandler
{
    public override void Start()
    {
        base.Start();
    }
    public override void Interact(GameObject currentGameObject)
    {
        if (HitBoxZoom != null && currentGameObject == HitBoxZoom.gameObject)
        {
            if (SaveManager.instance.debug == false)
            SaveManager.instance.debug = true;
            else if (SaveManager.instance.debug == true)
                SaveManager.instance.debug = false;
        }
    }
}