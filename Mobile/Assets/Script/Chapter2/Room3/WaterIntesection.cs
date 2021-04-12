using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterIntesection : ObjectHandler
{
    public WaterWall waterWall;
    public bool goodSide;
    public float angleToTurn;
    public int number;
    public bool isLeft;
    public SpriteRenderer left;
    public SpriteRenderer right;
    private float initialLeftSize;
    private float initialRightSize;
    public WaterIntesection interSectionParent;
    public bool interactIfLeft;
    public override void Start()
    {
        base.Start();
        initialLeftSize = left.size.y;
        if(right != null)
        initialRightSize = right.size.y;
        if (number == 1)
            right.size = new Vector2(right.size.x, 0.1f);
    }
    public override void Interact(GameObject currentGameObject)
    {
        if (HitBoxZoom != null && currentGameObject == HitBoxZoom.gameObject)
        {
            goodSide = !goodSide;
            if (isLeft)
            {
                 waterWall.ChangeSide(gameObject, goodSide,left,number, angleToTurn, right, initialRightSize);
            }
            else
            {
                if(right != null)
                {
                    waterWall.ChangeSide(gameObject, goodSide, right, number, angleToTurn, null,0);
                    left.size = new Vector2(left.size.x, initialLeftSize);
                }
            }
            isLeft = !isLeft;

        }
    }
    public SpriteRenderer CheckIfMove()
    {
        if(interSectionParent != null)
        {
            if(interSectionParent.isLeft == interactIfLeft)
            {
                if (isLeft)
                {
                    if(right!= null)
                    return right;
                }
                else
                {
                    return left;
                }
            }
        }
       
        return null;
    }
}
