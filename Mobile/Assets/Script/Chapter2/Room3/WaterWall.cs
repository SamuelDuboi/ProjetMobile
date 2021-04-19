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
    public bool hasWater;
    public GameObject water;

    public ObjectHandler trappe;
    public void ChangeSide(GameObject self, bool goodSide, SpriteRenderer spriteToScale, int number, float angle, SpriteRenderer spriteToRestart, float sizeToReceize, bool turnLeft)
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
                break;
        }

    }

   private IEnumerator Move(GameObject self, SpriteRenderer[] spriteToScale, float angle, bool canMoveSprite, SpriteRenderer spriteToRestart, float sizeToReceize, bool turnLeft)
    {
        float _angle= self.transform.localRotation.eulerAngles.z;
        Debug.Log(_angle);
        float currentAngle = 0;
        float direction = 1;
        if (!turnLeft)
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
}
