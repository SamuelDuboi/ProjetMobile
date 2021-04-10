using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRoom2 : MonoBehaviour
{
    public GameObject redCuve,greenCuve, blueCuve, yellowCuve;
    public ParticleSystem particleSystem;
    public bool red,green,yellow,blue;

    public float DistancePositionToChange;
    void Start()
    {
        EventManager.instance.SwipeUp += UpsideDown;
        blueCuve.SetActive(false);
        yellowCuve.SetActive(false);       
    }

    public virtual void UpsideDown(bool up)
    {
        if (EventManager.instance.uspideDown)
        {
            if(red && green)
            {
                redCuve.SetActive(false);
                greenCuve.SetActive(false);
                yellowCuve.SetActive(true);
                blueCuve.SetActive(true);
                if (!blue)
                {
                    yellowCuve.transform.position = new Vector3(yellowCuve.transform.position.x,
                                                                blueCuve.transform.position.y + DistancePositionToChange,
                                                                yellowCuve.transform.position.z);
                }
            }
            else if (!red)
            {
                greenCuve.SetActive(false);
                yellowCuve.SetActive(true);
                blueCuve.SetActive(true);
                if(!yellow && !blue)
                {
                    yellowCuve.transform.position = new Vector3(yellowCuve.transform.position.x,
                                                                blueCuve.transform.position.y - DistancePositionToChange,
                                                                yellowCuve.transform.position.z);
                    redCuve.transform.position = new Vector3(redCuve.transform.position.x,
                                                                blueCuve.transform.position.y - DistancePositionToChange*2,
                                                                redCuve.transform.position.z);
                }
                else if (!yellow)
                {
                    redCuve.transform.position = new Vector3(redCuve.transform.position.x,
                                                                yellowCuve.transform.position.y - DistancePositionToChange,
                                                                redCuve.transform.position.z);
                }
                else
                {
                    redCuve.transform.position = new Vector3(redCuve.transform.position.x,
                                                                yellowCuve.transform.position.y - DistancePositionToChange*2,
                                                                redCuve.transform.position.z);
                }
            }
        }
        else
        {

        }
    }
}
