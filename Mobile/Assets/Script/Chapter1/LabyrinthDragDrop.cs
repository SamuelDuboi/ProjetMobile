using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthDragDrop : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;
    private int deadZone = 100;
    private Vector2 rayDirection;

    public float distance;
    bool cantMove;
    public GameObject canvas;
    bool IsMovingX;
    bool IsMovingY;
    Vector2 currentDirection;

    private void Start()
    {
        EventManager.instance.ZoomOut += Unzoom;
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
                        RayCast(Vector2.right);
                    }
                    else if (direction.x < -deadZone)
                    {
                        RayCast(Vector2.left);

                    }

                }
                else
                {

                    if (direction.y > deadZone)
                    {
                        RayCast(Vector2.up);
                    }
                    else if (direction.y < -deadZone)
                    {
                        RayCast(Vector2.down);

                    }
                }

            }
        }
        if (IsMovingX)
        {
            cantMove = true;
            transform.Translate(currentDirection * 80*Time.deltaTime);
           
        }
        else if (IsMovingY)
        {
            cantMove = true;
            transform.Translate(currentDirection * 80 * Time.deltaTime);                 
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsMovingX = false;
        IsMovingY = false;
        cantMove = false;
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
    private void RayCast(Vector2 direction)
    {
        if (!cantMove)
        {
            
                currentDirection = direction;
                if (direction.x != 0)
                {
                    IsMovingX = true;
                }
                else
                {
                    IsMovingY = true;
                }
            
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
