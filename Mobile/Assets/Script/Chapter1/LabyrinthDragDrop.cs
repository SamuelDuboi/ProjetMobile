using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LabyrinthDragDrop : MonoBehaviour, IDragHandler
{
    private Vector3 initialPos;

    RectTransform[] wallRect;
    public GameObject[] walls;
    public GameObject end ;
    RectTransform endRect;

    RectTransform thisRect;
    public GameObject canvas;
    private void Start()
    {
        thisRect = transform as RectTransform;
        initialPos = transform.position;
        wallRect = new RectTransform[walls.Length];
        for (int i = 0; i < walls.Length; i++)
        {
            wallRect[i] = walls[i].transform as RectTransform;
        }
        endRect = end.transform as RectTransform;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.GetTouch(0).position;
        foreach (RectTransform rect in wallRect)
        {
            if(RectTransformUtility.RectangleContainsScreenPoint(rect, thisRect.position))
            {
                transform.position = initialPos;
                eventData.pointerDrag = null;
                return;
            }
        }
        if (RectTransformUtility.RectangleContainsScreenPoint(endRect, thisRect.position))
        {
            GetComponentInParent<ObjectHandler>().trialInstantiate = null;
            GetComponentInParent<ObjectHandler>().interactifElement.spawnNewTrial = false;
            GetComponentInParent<ObjectHandler>().Interact(GetComponentInParent<ObjectHandler>().HitBoxZoom.gameObject);
            Destroy(canvas);
            return;
        }
    }

}
