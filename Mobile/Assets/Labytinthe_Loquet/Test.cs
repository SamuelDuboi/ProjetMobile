using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test : MonoBehaviour
{
    float deltaX, deltaY;
    Rigidbody rb;
    bool moveAllowed = false;

    Vector3 prevPosition;
    private Event e;
    //private bool isclicked = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        MovingLoquet();
    }

    public void MovingLoquet()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log("EcranTouché");
            Touch touch = Input.GetTouch(0);
            Ray touchPos = Camera.main.ScreenPointToRay(touch.position);
            e = Event.current;
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Debug.Log("Phase de touché commence");
                    TouchBegan(touchPos,touch);
                    break;
                case TouchPhase.Moved:
                    TouchMoove(touchPos,touch);
                    break;
                case TouchPhase.Ended:
                    TouchEnd();
                    break;
            }
        }
        /*if (Input.GetMouseButtonDown(0))
        {
            isclicked = true;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Event.current.mousePosition);
            TouchBegan(mousePos);
        }

        if (isclicked == true)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Event.current.mousePosition);
            TouchMoove(mousePos);
        }

        else if (Input.GetMouseButtonUp(0))
        {
            isclicked = false;
            TouchEnd();
        }*/
    }

    private void TouchBegan(Ray touchPos, Touch touch)
    {
        Debug.Log("Entrée fonction");
        RaycastHit hit;
        if (Physics.Raycast(touchPos, out hit, Mathf.Infinity))
        {
            if(hit.collider.gameObject == gameObject)
            {
                prevPosition = touch.position;
               
                deltaX = touch.position.x - transform.position.x;
                deltaY = touch.position.y - transform.position.y;
                moveAllowed = true;
                
                GetComponent<SphereCollider>().sharedMaterial = null;
            }
            
        }
    }

    private void TouchMoove(Ray touchPos, Touch touch)
    {
        
        RaycastHit hit;
        if (Physics.Raycast(touchPos, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject == gameObject)
            {

                Vector3 startPos = Camera.main.ScreenToWorldPoint(touch.position);
                Vector3 endPos = Camera.main.ScreenToWorldPoint(prevPosition);

                Vector3 deltaPos = endPos - startPos;
                transform.Translate(touch.deltaPosition*Time.deltaTime);
                Debug.Log(touch.deltaPosition*Time.deltaTime);
                prevPosition = touch.position;
            }
        }
    }

    private void TouchEnd()
    {
        Debug.Log("Relachement");
        moveAllowed = false;
    }

}