using UnityEngine;

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
            #region RayCast
            var touch = Input.GetTouch(0);
            var ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit = new RaycastHit();
            //cast a ray to zoomable object if null, cast a ray to interactible object, if null try to unzoom
            LayerMask mask = 8;
            if (Physics.Raycast(ray, out hit,Mathf.Infinity, mask))
            {
                
                    float direction;
                    hit.collider.GetComponentInParent<ObjectHandler>().ChoseToZoom( out direction);
                    int angle = hit.collider.GetComponentInParent<ObjectHandler>().interactifElement.angle;
                    float orthographicSize= hit.collider.GetComponentInParent<ObjectHandler>().interactifElement.orthoGraphicSize;
                    if (direction != default)
                        EventManager.instance.OnZoomIn(hit.collider.transform.position, direction, angle,orthographicSize);
                    else
                        EventManager.instance.OnCollect(hit.collider.gameObject);               
                
            }
            else
            {
                mask = 1<<9;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {

                }
                else
                {

                    EventManager.instance.OnZoomOut();
                }
            }
            #endregion

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
                    }
                    else if (direction.x < -deadZone)
                    {
                        EventManager.instance.OnSwipeRight();
                    }
                }
                else
                {
                    if (direction.y > deadZone)
                    {
                        EventManager.instance.OnSwipeUp(true);
                    }
                    else if (direction.y < -deadZone)
                    {
                        EventManager.instance.OnSwipeUp(false);
                    }
                }
            }
                #endregion

        }
        #endregion

        #region Mouse
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit hit = new RaycastHit();
            LayerMask mask = 1<<8;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {

                float direction;
                hit.collider.GetComponentInParent<ObjectHandler>().ChoseToZoom( out direction);
                int angle = hit.collider.GetComponentInParent<ObjectHandler>().interactifElement.angle;
                float orthographicSize = hit.collider.GetComponentInParent<ObjectHandler>().interactifElement.orthoGraphicSize;
                if (direction != default)
                    EventManager.instance.OnZoomIn(hit.collider.transform.position, direction,angle, orthographicSize);
                else
                {
                    mask = 1 << 9;                       
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                    {
                        if(hit.collider.gameObject.tag == "Collectable")
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
                mask =1<< 9;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {
                    if (hit.collider.gameObject.tag == "Collectable")
                        EventManager.instance.OnCollect(hit.collider.gameObject);
                    else
                    {
                        EventManager.instance.OnInteract(hit.collider.gameObject);
                    }
                }
                else
                {
                    EventManager.instance.OnZoomOut();
                }
            }

        }
        #endregion
    }

}
