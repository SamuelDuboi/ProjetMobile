using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBehavor : MonoBehaviour
{
    public FingerTipsManager tipsManager;

    public void Quite()
    {
        SaveManager.instance.Quit();
    }
    public void PlayeTuto()
    {
        tipsManager.canStart = true;
        SaveManager.instance.LoadTuto(gameObject);
    }

}
