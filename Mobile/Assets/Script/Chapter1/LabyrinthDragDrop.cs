using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthDragDrop : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;
    private int deadZone = 100;
    private Vector2 rayDirection;

    RaycastHit2D hit;
    public float distance;
    bool cantMove;
    public GameObject canvas;

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
    }
    private void RayCast(Vector2 direction)
    {
        hit=  Physics2D.Raycast(transform.position, direction);
        if (hit && !cantMove)
        {
            if (direction.x != 0)
                StartCoroutine(MoveX(direction, hit));
            else
            {
                 StartCoroutine(MoveY(direction, hit));
            }
        }
    }
    IEnumerator MoveX(Vector2 direction, RaycastHit2D hit)
    {
        cantMove = true;
        while(Mathf.Abs( Mathf.Abs(transform.position.x) - Mathf.Abs( hit.collider.transform.position.x)) > distance)
        {
            transform.Translate(direction*15);
            yield return new WaitForSeconds(0.01f);
        }
        cantMove = false;
    }
    IEnumerator MoveY(Vector2 direction, RaycastHit2D hit)
    {
        cantMove = true;

        while (Mathf.Abs( Mathf.Abs(transform.position.y) - Mathf.Abs(hit.point.y)) > distance)
        {
            
            transform.Translate(direction*15);
            yield return new WaitForSeconds(0.01f);
        }
        if(hit.collider.gameObject.name == "Win")
        {
            EventManager.instance.OnDestroyTrial();
            GetComponentInParent<ObjectHandler>().trialInstantiate = null;
            GetComponentInParent<ObjectHandler>().interactifElement.spawnNewTrial = false;
            GetComponentInParent<ObjectHandler>().Interact(GetComponentInParent<ObjectHandler>().HitBoxZoom.gameObject);
            Destroy(canvas);
        }
        cantMove = false;
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
