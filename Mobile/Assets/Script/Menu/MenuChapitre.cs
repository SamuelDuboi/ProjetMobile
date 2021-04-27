using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuChapitre : ObjectHandler
{
    public int index;
    public bool quit;

    public override void Start()
    {
        base.Start();
    }
    public override void Interact(GameObject currentGameObject)
    {
        if (HitBoxZoom != null && currentGameObject == HitBoxZoom.gameObject)
        {
            if (quit)
                Application.Quit();

                SceneManager.LoadScene(index);
        }
    }
}

