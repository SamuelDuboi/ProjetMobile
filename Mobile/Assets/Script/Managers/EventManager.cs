using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
   // [HideInInspector] public bool cantRotate;
     [HideInInspector] public bool uspideDown;
     [HideInInspector] public CamDirection cuurrentCamDirection;
    [HideInInspector] public bool cantDoZoom;
    [HideInInspector] public bool isZoomed;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    #region Actions
   //call in DeviceManager
    public event Action<bool> SwipeUp;
    public event Action SwipeLeft;
    public event Action SwipeRight;

    public event Action<Vector3, float, int, float> ZoomIn;
    public event Action  ZoomOut;

    public event Action<GameObject> CollectObject;
    public event Action<GameObject> InteractObject;
    // call in LightManager
    public event Action<GameObject> LightObject;

    public event Action<string, float> Popup;
#endregion

    #region Invoke
    public void OnSwipeUp(bool up)
    {
        if (!cantDoZoom && !isZoomed)
        {
            uspideDown = !uspideDown;
            cantDoZoom = true;
            SwipeUp.Invoke(up);
        }

    }
    public void OnSwipeLeft()
    {
        if (!cantDoZoom && !isZoomed)
        {
            cantDoZoom = true;

            SwipeLeft.Invoke();           
        }
    }
    public void OnSwipeRight()
    {
        if (!cantDoZoom && !isZoomed)
        {
            cantDoZoom = true;

            SwipeRight.Invoke();
        }
    }
    public void OnZoomIn(Vector3 position, float direction, int angle, float orthographicSize)
    {
        if (!cantDoZoom && !isZoomed)
        {
            cantDoZoom = true;

            ZoomIn.Invoke(position, direction, angle, orthographicSize);
        }
    }

    public void OnZoomOut()
    {
        if (!cantDoZoom)
        {
            cantDoZoom = true;

            ZoomOut.Invoke();
        }
    }
    public void OnCollect(GameObject currentObjects)
    {
        CollectObject.Invoke(currentObjects);
    }
    public void OnInteract(GameObject currentObjects)
    {
        InteractObject.Invoke(currentObjects);
    }
    public void OnLightOn(GameObject currentObject)
    {
        if(LightObject != null)
        LightObject.Invoke(currentObject);
    }
    public void OnPopup(string text, float time)
    {
        Popup.Invoke(text, time);
    }
    #endregion
}
