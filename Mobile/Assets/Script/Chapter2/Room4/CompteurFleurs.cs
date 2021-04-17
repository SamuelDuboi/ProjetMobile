using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompteurFleurs : MonoBehaviour
{
    public int index;
    public GameObject[] lumiereAllumees;
    public int indexInArray;
    public CompteurManager compteurManager;
    public int solution;
    public bool isResolved;

    public void Compteur()
    {
        if (index != 1000)
        {
            if (index == 4)
            {
                index = 0;
            }
            else
            {
                index += 1;
            }
        }
        if (index == 0)
        {
            for (int i = 0; i < lumiereAllumees.Length; i++)
            {
                lumiereAllumees[i].SetActive(false);
            }
        }
        if (index > 0)
        {
            lumiereAllumees[index-1].SetActive(true);
        }

        if(index == solution)
        {
            isResolved = true;
            compteurManager.CompteurCheck(indexInArray);
        }
        else
        {
            isResolved = false;
            compteurManager.NotOK(indexInArray);

        }
    }
}
