using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthDragDrop : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;
    private int deadZone = 100;

    public float speed;
    public GameObject canvas;
    private Rigidbody2D Rigidbody2D;

    private void Start()
    {
        EventManager.instance.ZoomOut += Unzoom;
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {

            var touch = Input.GetTouch(0);


            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
                endPos = touch.position;
            }

            if (touch.phase == TouchPhase.Ended )
            {
                endPos = touch.position;
                var direction = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);

                //check in wich direction the player swapped
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {

                    if (direction.x > deadZone)
                    {
                        Rigidbody2D.velocity = Vector2.right * speed;
                    }
                    else if (direction.x < -deadZone)
                    {
                        Rigidbody2D.velocity = Vector2.left * speed;

                    }

                }
                else
                {

                    if (direction.y > deadZone)
                    {
                        Rigidbody2D.velocity = Vector2.up * speed;
                    }
                    else if (direction.y < -deadZone)
                    {
                        Rigidbody2D.velocity = Vector2.down * speed;

                    }
                }

            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Win")
        {
            EventManager.instance.OnDestroyTrial();
            GetComponentInParent<ObjectHandler>().trialInstantiate = null;
            GetComponentInParent<ObjectHandler>().interactifElement.spawnNewTrial = false;
            GetComponentInParent<ObjectHandler>().Interact(GetComponentInParent<ObjectHandler>().HitBoxZoom.gameObject);
            EventManager.instance.ZoomOut -= Unzoom;
            Destroy(canvas);
        }
    }

    private void Unzoom()
    {
        var parent = GetComponentInParent<ObjectHandler>();
        if (parent)
        {
            parent.interactifElement.onlyZoom = false;
            parent.HitBoxZoom.enabled = false;
            parent.interactifElement.spawnNewTrial = true;
            EventManager.instance.ZoomOut -= Unzoom;
            Destroy(parent.trialInstantiate);
        }
    }
}
