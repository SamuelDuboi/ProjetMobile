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

        EventManager.instance.ZoomOut += Unzoom;
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
            EventManager.instance.OnDestroyTrial();
            GetComponentInParent<ObjectHandler>().trialInstantiate = null;
            GetComponentInParent<ObjectHandler>().interactifElement.spawnNewTrial = false;
            GetComponentInParent<ObjectHandler>().Interact(GetComponentInParent<ObjectHandler>().HitBoxZoom.gameObject);
            Destroy(canvas);
            return;
        }
    }

    private void Unzoom()
    {
        var parent = GetComponentInParent<ObjectHandler>();
        if (parent)
        {
            parent.interactifElement.onlyZoom = false;
            /*if (destroyHitBoxParent)
            {
                GetComponentInParent<ObjectHandler>().HitBoxZoom.gameObject.SetActive(true);

            }*/
            parent.HitBoxZoom.enabled = true;
            parent.interactifElement.spawnNewTrial = true;

        }
        EventManager.instance.ZoomOut -= Unzoom;
        EventManager.instance.OnDestroyTrial();
        Destroy(parent.trialInstantiate);
    }

}
