using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRoom2 : MonoBehaviour
{
    public SpriteRenderer[] waterQuads;
    public bool[] open ;
    public Collider exitCollider;
    public GameObject hundredKG;
    public int[] numbers;
    void Start()
    {
        EventManager.instance.SwipeUp += UpsideDown;
        for (int i = 0; i < numbers.Length; i++)
        {
            waterQuads[i * 2].size = new Vector2(waterQuads[i * 2].size.x, 0.03f * numbers[i]);
            waterQuads[i * 2 + 1].size = new Vector2(waterQuads[i * 2 + 1].size.x, 0.03f * numbers[i]);
        }
    }

    public virtual void UpsideDown(bool up)
    {
        if (EventManager.instance.uspideDown)
        {
            for (int i = 0; i < 2; i++)
            {
                if (open[i])
                {
                    numbers[i + 1] += numbers[i];
                    numbers[i] = 0;
                }
            }
           if( numbers[2] >0)
            { 
                //activate water
            }
            numbers[3] += numbers[2];
            numbers[2] = 0;

            
            for (int i = 3; i < 5; i++)
            {
                if (open[i-1])
                {
                    numbers[i + 1] += numbers[i];
                    numbers[i] = 0;
                }
            } 

        }
        else
        {
            for (int i = 5; i >3; i--)
            {
                if (open[i-2])
                {
                    numbers[i -1] += numbers[i];
                    numbers[i] = 0;
                }
            }
            if (numbers[3] > 0)
            {
                //activate water
            }
            numbers[2] += numbers[3];
            numbers[3] = 0;

            for (int i = 2; i >0; i--)
            {
                if (open[i-1])
                {
                    numbers[i -1] += numbers[i];
                    numbers[i] = 0;
                }
            }
        }
        if (numbers[5] == 4)
        {
            exitCollider.enabled = true;
            hundredKG.SetActive(true);
        }
        else
        {
            if (!hundredKG.activeSelf)
            {
                exitCollider.enabled = false;

                hundredKG.SetActive(false);
            }
        }
        for (int i = 0; i < numbers.Length; i++)
        {
            waterQuads[i * 2].size = new Vector2(waterQuads[i * 2].size.x, 0.03f * numbers[i]);
            waterQuads[i * 2 +1].size = new Vector2(waterQuads[i * 2+1].size.x, 0.03f * numbers[i]);
        }
    }

    public void ActivateBool(int index )
    {
        open[index] = !open[index];
        UpsideDown(true);
    }
}
