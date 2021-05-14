using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWall : MonoBehaviour
{
    public bool firstGood;
    public bool secondGood;
    public bool thirdGood;
    private List<SpriteRenderer> spritesArray;
    private List<SpriteRenderer> spritesArrayToClose;
    private List<float> sizeArrayToClose;
    public WaterIntesection[] waterIntesections;
    public bool hasWater;
    public GameObject water;

    public ObjectHandler trappe;
    public void ChangeSide(GameObject self, bool goodSide, SpriteRenderer spriteToScale, int number, float angle, SpriteRenderer spriteToRestart, float sizeToReceize, bool turnLeft)
    {
        
        spritesArray = new List<SpriteRenderer>();
        spritesArrayToClose = new List<SpriteRenderer>();
        sizeArrayToClose = new List<float>();
        spritesArray.Add(spriteToScale);
        for (int i = number; i < waterIntesections.Length; i++)
        {
            spritesArray.Add(waterIntesections[i].CheckIfMove());
        }
        spritesArrayToClose.Add(spriteToRestart);
        sizeArrayToClose.Add(sizeToReceize);
        for (int i = number; i < waterIntesections.Length; i++)
        {
            float outNumber = 0;
            spritesArrayToClose.Add(waterIntesections[i].CheckIfDontMove(out outNumber));
            sizeArrayToClose.Add( outNumber);
        }
        switch (number)
        {
            case 1:                
                StartCoroutine( Move(self, spritesArray.ToArray(), angle,true, spriteToRestart, sizeToReceize,turnLeft));
                firstGood = goodSide;
                break;
            case 2:

                StartCoroutine(Move(self, spritesArray.ToArray(), angle, firstGood,spriteToRestart,sizeToReceize,turnLeft));
                secondGood = goodSide;
                break;
            case 3:

                StartCoroutine(Move(self, spritesArray.ToArray(), angle, firstGood, spriteToRestart, sizeToReceize, turnLeft));
                break;
            case 4:
                if (firstGood && secondGood)
                {
                    StartCoroutine(Move(self, spritesArray.ToArray(), angle, true, spriteToRestart, sizeToReceize,turnLeft));
                    thirdGood = true;
                    InventoryManager.Instance.AddList(gameObject, trappe.NameToAddIfAnimToAdd, default, 1);
                    trappe.Interact(trappe.HitBoxZoom.gameObject);
                    water.SetActive(true);
                }
                else
                    StartCoroutine( Move(self, spritesArray.ToArray(), angle, false, spriteToRestart, sizeToReceize,turnLeft));
                break;

            default:
                StartCoroutine(Move(self, spritesArray.ToArray(), angle, false, spriteToRestart, sizeToReceize, turnLeft));
                break;
        }

    }

   private IEnumerator Move(GameObject self, SpriteRenderer[] spriteToScale, float angle, bool canMoveSprite, SpriteRenderer spriteToRestart, float sizeToReceize, bool turnLeft)
    {
        float _angle= self.transform.localRotation.eulerAngles.z;
        Debug.Log(_angle);
        float currentAngle = 0;
        float direction = 1;
        bool doOnce = false;
        if (!turnLeft)
            direction = -1;
        while (currentAngle < angle)
        {
            currentAngle += 1f;
            self.transform.Rotate(Vector3.forward * direction, 1f, Space.Self);
            if(currentAngle >angle*0.5f && !doOnce)
            {
                doOnce = true;
                StartCoroutine(MoveSpriteUp(spritesArrayToClose.ToArray()));
            }
            yield return new WaitForSeconds(0.01f);
        }        
        if (canMoveSprite && hasWater)
        {            
             StartCoroutine(  MoveSprite(spriteToScale));
           
        }
    }
    private IEnumerator MoveSprite(SpriteRenderer[] spriteToScale)
    {
        foreach (var sprite in spriteToScale)
        { 
            if(sprite!= null)
            {
                while (sprite.size.y > 0.1f)
                {
                    sprite.size -= new Vector2(0, 0.1f);
                    yield return new WaitForSeconds(0.01f);
                }
            }           
       }
    }
    private IEnumerator MoveSpriteUp(SpriteRenderer[] spriteToScale)
    {
        for (int i = spriteToScale.Length-1; i >= 0; i--)
        {
            if (spriteToScale[i] != null)
            {
                while (spriteToScale[i].size.y <= sizeArrayToClose[i])
                {
                    spriteToScale[i].size += new Vector2(0, 0.1f);
                    yield return new WaitForSeconds(0.01f);
                }
            }
        } 
    }
    private IEnumerator MoveSprite(SpriteRenderer spriteToScale)
    {

        if (spriteToScale != null)
        {
            while (spriteToScale.size.y > 0.1f)
            {
                spriteToScale.size -= new Vector2(0, 0.1f);
                yield return new WaitForSeconds(0.01f);
            }
        }

    }
    public void ActivateWater()
    {
        hasWater = true;
        if (waterIntesections[0].isLeft)
           StartCoroutine( MoveSprite(waterIntesections[0].right));

    }
}
