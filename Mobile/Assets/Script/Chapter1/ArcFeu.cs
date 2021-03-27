using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcFeu : MonoBehaviour
{
    public bool tap;
    public bool swipeLeft = true;
    public bool swipeRight = true;

    public Vector2 startTouch, swipeDelta;

    public Vector2 SwipeDelta;
    public bool SwipeLeft;
    public bool SwipeRight;
    public bool isDragging = false;

    public float startTime, deltaTime, speed;

    public float swipeCount;

    private void Update()
    {
        tap = false;

        #region Inputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                startTime = Time.time;
                isDragging = true;
                tap = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDragging = false;
                Reset();
            }
        }
        #endregion

        swipeDelta = Vector2.zero;
        if (isDragging)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
        }

        if (swipeDelta.magnitude > 200)
        {
            float x = swipeDelta.x;

            deltaTime = Time.time - startTime;
            speed = 200 / deltaTime;

            if (x < 0 && speed > 1000 && swipeRight == true)
            {
                Debug.Log("Swipe à gauche");
                Debug.Log(speed);
                swipeLeft = true;
                swipeRight = false;
                swipeCount += 1;
                startTouch = Input.touches[0].position;
                startTime = Time.time;
            }
            else if (x > 0 && speed > 100 && swipeLeft == true)
            {
                Debug.Log("Swipe à droite");
                Debug.Log(speed);
                swipeRight = true;
                swipeLeft = false;
                swipeCount += 1;
                startTouch = Input.touches[0].position;
                startTime = Time.time;
            }
        }

        if(swipeCount == 20)
        {
            Debug.Log("C'est gagné");
            Reset();
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
        startTime = 0;
        swipeLeft = true;
        swipeRight = true;
        if(swipeCount < 20)
        {
            swipeCount = 0;
        }
}
}
