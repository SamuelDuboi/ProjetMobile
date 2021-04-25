﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ExitObject : MonoBehaviour
{
    public GameObject text;
    private void Start()
    {
        EventManager.instance.InteractObject += Interact;
    }

    private void Interact(GameObject currentObject)
    {
        if(currentObject == gameObject || currentObject== transform.GetChild(0).gameObject)
        {
            if(SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().buildIndex == 9 || SceneManager.GetActiveScene().buildIndex ==0)
            {
                SceneManager.LoadScene(1);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
