using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Chest : MonoBehaviour, IDragHandler, IEndDragHandler
{

    public Transform childTransform;
    private int index;
    private int initialIndex;
    private float initialPos;
    private float positionMovement;
    private float sensibility;
    private float previusXPos;
    private float currentEventDataX;

    private int inedxInArray;
    private ChestManager chestManager;
    public void OnDrag(PointerEventData eventData)
    {
        if(Input.touchCount>0)
            childTransform.position = new Vector3(childTransform.position.x + eventData.delta.x* sensibility, Input.GetTouch(0).position.y, childTransform.position.z);
        else
        {
            currentEventDataX = eventData.delta.x;
            childTransform.position = new Vector3(childTransform.position.x+ eventData.delta.x*sensibility, childTransform.position.y, childTransform.position.z);
            if( Mathf.Abs(childTransform.position.x - initialPos )> positionMovement)
            {
                childTransform.position = new Vector3(initialPos, childTransform.position.y);
                index = initialIndex;
            }
            if(Mathf.Abs(childTransform.position.x - previusXPos) > positionMovement / 10)
            {
                previusXPos = childTransform.position.x;

                if (eventData.delta.x> 0)
                {
                    index--;
                    if (index == -1)
                        index = 9;

                }
                else
                {
                    index++;
                    if (index == 10)
                        index = 0;
                }

            }
        }
    }


    public void Init(int _index, float _sensibility, int indexInArray, ChestManager chestManager)
    {
        this.chestManager = chestManager;
        inedxInArray = indexInArray;
        index = _index;
        sensibility = _sensibility;
        var rect = childTransform as RectTransform;
        positionMovement = rect.sizeDelta.x / 3;
        childTransform.position = new Vector3(childTransform.position.x - index * positionMovement / 10, childTransform.position.y, childTransform.position.z);
        initialPos = childTransform.position.x;
        previusXPos = initialPos;
        initialIndex = index;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentEventDataX < 0)
        {
            if (Mathf.Abs(childTransform.position.x) > Mathf.Abs(previusXPos) + positionMovement / 20)
            {
                childTransform.position = new Vector2(previusXPos, childTransform.position.y);
            }
            else
            {
                index++;
                childTransform.position = new Vector2(previusXPos - positionMovement / 10, childTransform.position.y);
                previusXPos = childTransform.position.x;
                if (index == 10)
                    index = 0;
            }
        }
        else
        {
            if (Mathf.Abs(childTransform.position.x) < Mathf.Abs(previusXPos) + positionMovement / 20)
            {
                childTransform.position = new Vector2(previusXPos, childTransform.position.y);

            }
            else
            {
                index--;
                childTransform.position = new Vector2(previusXPos + positionMovement / 10, childTransform.position.y);
                previusXPos = childTransform.position.x;
                if (index == -1)
                    index = 9;
            }

        }
        chestManager.TryValidate(index, inedxInArray);
      
    }
}
