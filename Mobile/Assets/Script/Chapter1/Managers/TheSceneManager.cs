using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheSceneManager : MonoBehaviour
{
    public void ReloadScene()
    {
        SaveManager.instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(int index)
    {
        SaveManager.instance.LoadScene(index);
    }

    public void LoadMenu()
    {
        SaveManager.instance.LoadScene(1);
    }

    public void Quite()
    {
        Application.Quit();
    }
}
