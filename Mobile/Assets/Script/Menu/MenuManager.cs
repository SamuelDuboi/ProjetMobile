using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public bool chapitre1;
    public GameObject[] doors;

    private void Start()
    {
        if (chapitre1)
        {
            SaveManager.instance.LoadChapter1(doors);
        }
        else
        {
            SaveManager.instance.LoadChapter2(doors);
        }
    }
}
