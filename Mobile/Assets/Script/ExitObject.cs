using System.Collections;
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
