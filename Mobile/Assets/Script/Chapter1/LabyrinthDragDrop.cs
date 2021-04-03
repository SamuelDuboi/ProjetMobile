﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LabyrinthDragDrop : MonoBehaviour, IDragHandler
{
    private Vector3 initialPos;

    RectTransform[] wallRect;
    public GameObject[] walls;
    private void Start()
    {
        initialPos = transform.position;
        wallRect = new RectTransform[walls.Length];
        for (int i = 0; i < walls.Length; i++)
        {
            wallRect[i] = walls[i].transform as RectTransform;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.GetTouch(0).position;
        foreach (RectTransform rect in wallRect)
        {
            if(RectTransformUtility.RectangleContainsScreenPoint(rect, Input.GetTouch(0).position))
            {
                transform.position = initialPos;
                eventData.pointerDrag = null;
                return;
            }
        }
    }

}
