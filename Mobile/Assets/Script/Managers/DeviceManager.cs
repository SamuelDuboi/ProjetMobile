using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
public class DeviceManager : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;
    public float deadZone = 0.2f;


    void Update()
    {
        #region Finger
        if (Input.touchCount > 0)
        {

            var touch = Input.GetTouch(0);


            #region Swipe
            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
                endPos = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                endPos = touch.position;
                var direction = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);

                //check in wich direction the player swapped
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    if (direction.x > deadZone)
                    {
                        EventManager.instance.OnSwipeLeft();
                        return;
                    }
                    else if (direction.x < -deadZone)
                    {
                        EventManager.instance.OnSwipeRight();
                        return;
                    }
                }
                else
                {
                    if (direction.y > deadZone)
                    {
                        EventManager.instance.OnSwipeUp(true);
                        return;
                    }
                    else if (direction.y < -deadZone)
                    {
                        EventManager.instance.OnSwipeUp(false);
                        return;
                    }
                }
                #region RayCast
                var ray = Camera.main.ScreenPointToRay(touch.position);
                if (EventSystem.current.IsPointerOverGameObject())
                    return;

                RaycastHit hit = new RaycastHit();
                LayerMask mask = 1 << 8;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {
                    Cams cams = null;
                    var _objecthandler = hit.collider.GetComponentInParent<ObjectHandler>();
                    float orthographicSize = _objecthandler.interactifElement.orthoGraphicSize;
                    
                        _objecthandler.ChoseToZoom(out cams);
                    
                    if (cams != null)
                        EventManager.instance.OnZoomIn(cams, orthographicSize, _objecthandler.gameObject);
                    else
                    {
                        mask = 1 << 9;
                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                        {
                            if (hit.collider.gameObject.tag == "Collectable")
                                EventManager.instance.OnCollect(hit.collider.gameObject);
                            else
                            {
                                EventManager.instance.OnInteract(hit.collider.gameObject);
                            }

                        }
                    }


                }
                else
                {
                    mask = 1 << 9;
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                    {

                        if (hit.collider.gameObject.tag == "Collectable")
                            EventManager.instance.OnCollect(hit.collider.gameObject);
                        else
                        {
                            EventManager.instance.OnInteract(hit.collider.gameObject);
                        }
                    }/*
                else
                {
                    EventManager.instance.OnZoomOut();
                }*/
                }
                return;
                #endregion
            }
            #endregion

        }
        #endregion

        #region Mouse
        if (Input.GetMouseButtonUp(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();
            LayerMask mask = 1 << 8;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                Cams cams = null;

                var _objecthandler = hit.collider.GetComponentInParent<ObjectHandler>();
                float orthographicSize = _objecthandler.interactifElement.orthoGraphicSize;
                
                    _objecthandler.ChoseToZoom(out cams);
                if (cams != null)
                    EventManager.instance.OnZoomIn(cams, orthographicSize, _objecthandler.gameObject);
                else
                {
                    mask = 1 << 9;
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                    {
                        if (hit.collider.gameObject.tag == "Collectable")
                            EventManager.instance.OnCollect(hit.collider.gameObject);
                        else
                        {
                            EventManager.instance.OnInteract(hit.collider.gameObject);
                        }

                    }
                }


            }
            else
            {
                mask = 1 << 9;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {

                    if (hit.collider.gameObject.tag == "Collectable")
                        EventManager.instance.OnCollect(hit.collider.gameObject);
                    else
                    {
                        EventManager.instance.OnInteract(hit.collider.gameObject);
                    }
                }/*
                else
                {
                    EventManager.instance.OnZoomOut();
                }*/
            }

        }
        #endregion


    }

}
