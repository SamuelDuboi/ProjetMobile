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
        if (left != null)
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
                if (left != null)
                {
                    waterWall.ChangeSide(gameObject, goodSide, left, number, angleToTurn, right, initialRightSize, true);
                    if (right != null)
                        right.size = new Vector2(right.size.x, initialLeftSize);
                }
               
            }
            else
            {
                if(right != null)
                {
                    waterWall.ChangeSide(gameObject, goodSide, right, number, angleToTurn, null,0, false);
                    if(left!=null)
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
                    if(left!=null)
                    return left;
                }
            }
        }
       
        return null;
    }
}
