using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutoDeviceManager : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;
    public float deadZone = 0.2f;
    public int phase;
    public bool stopAnim;

    void Update()
    {
        if (phase != 0)
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
                        if(phase != 6)
                        {
                            if (direction.x > deadZone)
                            {
                                if (phase == 1)
                                {
                                    phase++;
                                    stopAnim = true;
                                    EventManager.instance.OnSwipeLeft();

                                }
                                else if (phase > 3 && phase != 8 )
                                    EventManager.instance.OnSwipeLeft();
                                return;
                            }
                            else if (direction.x < -deadZone)
                            {

                                if (phase > 3 && phase != 8 )
                                    EventManager.instance.OnSwipeRight();
                                return;
                            }
                        }
                        
                    }
                    else
                    {
                        if (phase ==6 || phase>7 && phase != 9)
                        {
                            if (direction.y > deadZone)
                            {
                                if(phase == 6|| phase== 8)
                                {
                                    stopAnim = true;
                                    phase++;
                                }
                                EventManager.instance.OnSwipeUp(true);
                                return;
                            }
                            else if (direction.y < -deadZone)
                            {
                                if (phase == 6 || phase == 8)
                                {
                                    stopAnim = true;
                                    phase++;
                                }
                                EventManager.instance.OnSwipeUp(false);
                                return;
                            }
                        }

                    }
                    if (phase > 1)
                    {
                        #region RayCast
                        var ray = Camera.main.ScreenPointToRay(touch.position);
                        if (EventSystem.current.IsPointerOverGameObject())
                            return;

                        RaycastHit hit = new RaycastHit();
                        LayerMask mask = 1 << 8;

                        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                        {
                            Cams cams = null;

                            float orthographicSize = hit.collider.GetComponentInParent<ObjectHandler>().interactifElement.orthoGraphicSize;
                            if (!EventManager.instance.isZoomed)
                            {
                                hit.collider.GetComponentInParent<ObjectHandler>().ChoseToZoom(out cams);
                            }
                            if (cams != null)
                            {
                                if(phase == 2 )
                                {
                                    if (hit.collider.GetComponentInParent<ObjectHandler>().name == "Poignée")
                                    {
                                        stopAnim = true;
                                        EventManager.instance.OnZoomIn(cams, orthographicSize);
                                        phase++;
                                    }

                                }
                                else
                                {

                                    if (hit.collider.GetComponentInParent<ObjectHandler>().name == "Valise" && phase == 4)
                                    {
                                        FingerTipsManager.instance.zoomBack = true;
                                        stopAnim = false;
                                        EventManager.instance.OnZoomIn(cams, orthographicSize);
                                    }
                                    else if  (hit.collider.GetComponentInParent<ObjectHandler>().name == "Valise" && phase == 9)
                                    {
                                        stopAnim = false;
                                        EventManager.instance.OnZoomIn(cams, orthographicSize);
                                    }
                                    else if (hit.collider.GetComponentInParent<ObjectHandler>().name == "Commode" && phase == 11)
                                    {
                                        stopAnim = false;
                                        EventManager.instance.OnZoomIn(cams, orthographicSize);
                                    }
                                    else if (hit.collider.GetComponentInParent<ObjectHandler>().name == "Poignée" )
                                    {
                                        int number = 0;
                                        var _item = InventoryManager.Instance.FindObject("Part 2", out number);
                                        var _item2 = InventoryManager.Instance.FindObject("Part 1", out number);
                                        if(_item!= null && _item != null)
                                        {
                                            stopAnim = false;
                                            EventManager.instance.OnZoomIn(cams, orthographicSize);
                                        }
                                    }
                                    else if(phase>6 && phase != 9 && phase !=12)
                                        EventManager.instance.OnZoomIn(cams, orthographicSize);
                                }
                            }
                            else
                            {
                                if (phase > 3 && phase != 8)
                                {
                                    mask = 1 << 9;
                                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                                    {
                                        if (hit.collider.gameObject.tag == "Collectable")
                                        {
                                            EventManager.instance.OnCollect(hit.collider.gameObject);
                                            if(hit.collider.GetComponentInParent<ObjectHandler>().name == "Poignée 1")
                                            {
                                                EventManager.instance.ZoomOut += FingerTipsManager.instance.ZoomOutDoor;
                                            }
                                            else 
                                            {
                                                FingerTipsManager.instance.zoomBack = false;
                                            }
                                        }
                                        else
                                        {
                                            if (hit.collider.GetComponentInParent<ObjectHandler>().name == "Valise" && phase == 5)
                                            {
                                                stopAnim = false;
                                                EventManager.instance.OnInteract(hit.collider.gameObject);
                                            }
                                            else if (hit.collider.GetComponentInParent<ObjectHandler>().name == "Valise" && phase == 9)
                                            {
                                                stopAnim = false;
                                                EventManager.instance.OnInteract(hit.collider.gameObject);
                                            }
                                            else if (phase > 5 && phase != 11 && phase != 10 && phase != 9)
                                            {
                                                EventManager.instance.OnInteract(hit.collider.gameObject);
                                            }
                                        }
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
                                {
                                    if(phase == 10 && hit.collider.GetComponentInParent<ObjectHandler>().name == "Poignée 2")
                                    {
                                        phase++;
                                        stopAnim = true;
                                        EventManager.instance.OnCollect(hit.collider.gameObject);
                                        FingerTipsManager.instance.zoomBack = false;
                                    }
                                    if (phase > 11)
                                    {
                                        phase++;
                                        stopAnim = true;
                                        EventManager.instance.OnCollect(hit.collider.gameObject);
                                    }
                                }
                                else
                                {
                                    if (hit.collider.GetComponentInParent<ObjectHandler>().name == "Valise" && phase == 5)
                                    {
                                        stopAnim = false;
                                        EventManager.instance.OnInteract(hit.collider.gameObject);
                                    }
                                    if (hit.collider.GetComponentInParent<ObjectHandler>().name == "Valise" && phase == 9)
                                    {
                                        stopAnim = false;
                                        EventManager.instance.OnInteract(hit.collider.gameObject);
                                    }
                                    else if (phase !=8 && phase>6  && phase != 10 && phase !=9 )
                                    {
                                        EventManager.instance.OnInteract(hit.collider.gameObject);
                                    }
                                    else
                                    {

                                    }
                                }
                            }

                        }
                        return;
                    }
                    #endregion
                }

                #endregion

            }
            #endregion

        }
    }
}
