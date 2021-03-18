using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExitObject : MonoBehaviour
{
    public GameObject text;
    private void Start()
    {
        EventManager.instance.InteractObject += Interact;
    }

    private void Interact(GameObject currentObject)
    {
        if(currentObject == gameObject)
        {
            text.gameObject.SetActive(true);
        }
    }
}
