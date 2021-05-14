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
                   /* if (right != null)
                        right.size = new Vector2(right.size.x, initialLeftSize);*/
                }
                else
                {
                    waterWall.ChangeSide(gameObject, goodSide, left, 6, angleToTurn, right, initialRightSize, true);
                }
               
            }
            else
            {
                if(right != null)
                {
                    waterWall.ChangeSide(gameObject, goodSide, right, number, angleToTurn, left, initialLeftSize, false);
                    /*if(left!=null)
                    left.size = new Vector2(left.size.x, initialLeftSize);*/
                }
                else
                {
                    waterWall.ChangeSide(gameObject, goodSide, right, 6, angleToTurn, left, initialLeftSize, false);
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
    public SpriteRenderer CheckIfDontMove(out float size)
    {
        if (interSectionParent != null)
        {
            if (interSectionParent.isLeft != interactIfLeft)
            {
                if (isLeft)
                {
                    if (right != null)
                    {
                        size = initialRightSize;
                        return right;
                    }
                }
                else
                {
                    if (left != null)
                    {
                        size = initialLeftSize;
                        return left;
                    }
                }
            }
        }
        size = 0;
        return null;
    }
    public void Actualise()
    {

    }
}
