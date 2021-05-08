using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBehavor : MonoBehaviour
{

    public void Quite()
    {
        SaveManager.instance.Quit();
    }
    public void PlayeTuto()
    {
        SaveManager.instance.LoadTuto(gameObject);
    }

}
