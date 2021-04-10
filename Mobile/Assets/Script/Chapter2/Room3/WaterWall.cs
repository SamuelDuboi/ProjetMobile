using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWall : MonoBehaviour
{
    public bool firstGood;
    public bool secondGood;
    public bool thirdGood;
    private List<SpriteRenderer> spritesArray;
    public WaterIntesection[] waterIntesections;

    public void ChangeSide(GameObject self, bool goodSide, SpriteRenderer spriteToScale, int number, float angle, SpriteRenderer spriteToRestart, float sizeToReceize)
    {
        spritesArray = new List<SpriteRenderer>();
        spritesArray.Add(spriteToScale);
        for (int i = number; i < waterIntesections.Length; i++)
        {
            spritesArray.Add(waterIntesections[i].CheckIfMove());
        }
        switch (number)
        {
            case 1:                
                StartCoroutine( Move(self, spritesArray.ToArray(), angle,true, spriteToRestart, sizeToReceize));
                firstGood = goodSide;
                break;
            case 2:

                StartCoroutine(Move(self, spritesArray.ToArray(), angle, firstGood,spriteToRestart,sizeToReceize));
                secondGood = goodSide;
                break;
            case 3:
                if (firstGood && secondGood)
                {
                    StartCoroutine(Move(self, spritesArray.ToArray(), angle, true, spriteToRestart, sizeToReceize));
                }
                else
                    StartCoroutine( Move(self, spritesArray.ToArray(), angle, false, spriteToRestart, sizeToReceize));
                break;

            default:
                break;
        }

    }

   private IEnumerator Move(GameObject self, SpriteRenderer[] spriteToScale, float angle, bool canMoveSprite, SpriteRenderer spriteToRestart, float sizeToReceize)
    {
        float _angle= self.transform.localRotation.eulerAngles.z;
        Debug.Log(_angle);
        float currentAngle = 0;
        float direction = 1;
        if (self.transform.localRotation.eulerAngles.z >300)
            direction = -1;
        while (currentAngle < angle)
        {
            currentAngle += 1f;
            self.transform.Rotate(Vector3.forward * direction, 1f, Space.Self);
            yield return new WaitForSeconds(0.01f);
        }
        if (spriteToRestart != null)
        {
            spriteToRestart.size = new Vector2(spriteToRestart.size.x, sizeToReceize);
        }
        if (canMoveSprite)
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
}
