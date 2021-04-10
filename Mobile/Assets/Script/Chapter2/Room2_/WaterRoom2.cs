using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRoom2 : MonoBehaviour
{
    public WaterQuad[] waterQuads;
    public bool red,green,yellow,blue;

    public float distanceBewteenQuad;
    void Start()
    {
        EventManager.instance.SwipeUp += UpsideDown;
 
    }

    public virtual void UpsideDown(bool up)
    {
        Vector3 direction;
        if (EventManager.instance.uspideDown)
        {
            direction = Vector3.up;
            
        }
        else
        {
            direction = Vector3.down;
        }

        foreach (var quad in waterQuads)
        {
            quad.Move(direction, distanceBewteenQuad);
        }
    }
}
