using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrintheManager : MonoBehaviour
{
    public GameObject[] tuiles;
    public int index;
    public int[] indexRotate;
    public float timer = 0;
    private bool labyrintheResolved = false;

    public void Start()
    {
        for (int i = 0; i < indexRotate.Length; i++)
        {
            indexRotate[i] = 0;
        }
    }

    public void StartRotation(GameObject button)
    {
        if (timer == 0)
        StartCoroutine(RotateTouche(button));
    }
    private IEnumerator RotateTouche(GameObject button)
    {
        for (int i = 0; i < tuiles.Length; i++)
        {
            if (tuiles[i] == button)
            {
                index = i;
                break;
            }
            else
            {
                index = 1000;
            }
        }
        if (index != 1000)
        {
            if (indexRotate[index] == 3)
            {
                indexRotate[index] = 0;
            }
            else
            {
                indexRotate[index] += 1;
            }
            timer = 0f;
        }
        while (timer <= 0.20f)
        {
            timer += 0.01f;
            button.transform.Rotate(Vector3.forward, 90f/20f);
            yield return new WaitForSeconds(0.01f);
        }
        timer = 0;
        CheckSolution1();
        CheckSolution2();
    }

    public void CheckSolution1()
    {
        if (indexRotate[0] == 0 && indexRotate[1] == 1 && indexRotate[2] == 0 && (indexRotate[3] == 1 || indexRotate[3] == 3) && indexRotate[4] == 3 && indexRotate[5] == 0 && indexRotate[6] == 2 && indexRotate[7] == 0 && indexRotate[8] == 3 && (indexRotate[9] == 1 || indexRotate[9] == 3) && indexRotate[10] == 0 && indexRotate[11] == 0 && indexRotate[12] == 0 && indexRotate[13] == 1 && indexRotate[14] == 3 && indexRotate[15] == 0 && indexRotate[16] == 2 && indexRotate[17] == 3 && indexRotate[18] == 0 && indexRotate[19] == 1 && (indexRotate[20] == 0 || indexRotate[20] == 2) && indexRotate[21] == 2 && indexRotate[22] == 0 && indexRotate[23] == 0 && (indexRotate[24] == 0 || indexRotate[24] == 2) && (indexRotate[25] == 0 || indexRotate[25] == 2))
        {
            Debug.Log("Gagné");
            labyrintheResolved = true;
        }
    }
    public void CheckSolution2()
    {
        if (indexRotate[0] == 0 && indexRotate[1] == 1 && indexRotate[4] == 0 && indexRotate[7] == 1 && indexRotate[8] == 3 && (indexRotate[9] == 1 || indexRotate[9] == 3) && indexRotate[10] == 1 && indexRotate[11] == 0 && indexRotate[12] == 1 && indexRotate[13] == 0 && indexRotate[14] == 1 && indexRotate[15] == 0 && indexRotate[16] == 0 && indexRotate[17] == 2 && (indexRotate[20] == 1 || indexRotate[20] == 3) && indexRotate[21] == 0 && indexRotate[22] == 2 && indexRotate[23] == 1 && (indexRotate[25] == 0 || indexRotate[25] == 2) && (indexRotate[26] == 0 || indexRotate[26] == 2) && indexRotate[27] == 0 && (indexRotate[28] == 0 || indexRotate[28] == 2) && (indexRotate[29] == 1 || indexRotate[29] == 3) && indexRotate[30] == 0 )
        {
            Debug.Log("Gagné");
            labyrintheResolved = true;
        }
    }
}
