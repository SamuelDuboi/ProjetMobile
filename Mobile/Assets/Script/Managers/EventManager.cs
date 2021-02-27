using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
   // [HideInInspector] public bool cantRotate;
     [HideInInspector] public bool uspideDown;
     [HideInInspector] public CamDirection cuurrentCamDirection;
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

    public event Action<Vector3, float> ZoomIn;
    public event Action ZoomOut;

    public event Action<GameObject> CollectObject;
    public event Action<GameObject> InteractObject;
    // call in LightManager
    public event Action<GameObject> LightObject;
#endregion

    #region Invoke
    public void OnSwipeUp(bool up)
    {
        uspideDown = !uspideDown;

        SwipeUp.Invoke(up);
    }
    public void OnSwipeLeft()
    {
        SwipeLeft.Invoke();
    }
    public void OnSwipeRight()
    {
        SwipeRight.Invoke();
    }
    public void OnZoomIn(Vector3 position, float direction)
    {
        ZoomIn.Invoke(position, direction);
    }

    public void OnZoomOut()
    {
        ZoomOut.Invoke();
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
    #endregion
}
