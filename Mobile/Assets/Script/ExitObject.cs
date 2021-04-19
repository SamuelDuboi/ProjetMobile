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
            if(SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 8)
            {
                SceneManager.LoadScene(0);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
