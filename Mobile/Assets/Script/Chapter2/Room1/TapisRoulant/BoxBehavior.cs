using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehavior : MonoBehaviour
{
    private bool canMove;
    private bool goDown;
    private LayerMask mask;
    private float speed;
    private LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = GetComponentInParent<LevelManager>();      
        mask = 1 << 10;
        goDown = true;
        canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        speed = levelManager.speed;
        if (canMove)
        {
            if (!Physics.Raycast(transform.position, Vector3.right, 4.0f, mask))
            {
                transform.Translate(Vector3.up * 0.01f * speed);

                var layerMask = 1 << 11;

                if(!Physics.Raycast(transform.position, Vector3.right, 4.0f, layerMask))
                {
                    return;
                }
                else
                {
                    InventoryManager.Instance.AddList(gameObject, levelManager.name, default, 1);
                    levelManager.DestroyAll();
                    Destroy(gameObject);
                }
            }
            else 
            {
                Destroy(gameObject);
            }
        }
        else if (goDown)
        {
            if (!Physics.Raycast(transform.position, Vector3.down,  .01f, mask)) 
            {
                transform.Translate(Vector3.back * 0.01f*speed*5);
                Debug.DrawRay(transform.position, Vector3.down, Color.red, 1f);
            }
            else
            {
                goDown = false;
                canMove = true;
            }
        }
    }

   
}
