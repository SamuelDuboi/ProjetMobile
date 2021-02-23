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
    public event Action<bool> SwipeUp;
    public event Action SwipeLeft;
    public event Action SwipeRight;

    public event Action<Vector3, float> ZoomIn;
    public event Action ZoomOut;

    public event Action<GameObject> CollectObject;
    public event Action<GameObject> InteractObject;
#endregion

    #region Invoke
    public void OnSwipeUp(bool up)
    {
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
    #endregion
}
