using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnInteract : MonoBehaviour
{
   

    private void Start()
    {
        EventManager.instance.ZoomOut += Interact;
    }

    private void Interact()
    {
        gameObject.SetActive(false);
    }
}
