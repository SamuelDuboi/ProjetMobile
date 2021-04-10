using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterQuad : MonoBehaviour
{

    public void Move(Vector3 direction, float distanceBewteenQuad)
    {
        int layer = 1 << 10;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, layer))
        {
            transform.position = new Vector3(transform.position.x, hit.transform.position.y + direction.y * distanceBewteenQuad, transform.position.z);
        }
    }
}
