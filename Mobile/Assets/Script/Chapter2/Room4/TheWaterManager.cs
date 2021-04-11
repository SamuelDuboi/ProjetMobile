using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWaterManager : MonoBehaviour
{
    public Animator waterAnim;
    public int currentLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.SwipeUp += UpsideDown;
        
    }

    public virtual void UpsideDown(bool up)
    {
        waterAnim.SetTrigger("UpsideDown");
    }

    public void WaterUp()
    {
        currentLevel++;
        waterAnim.SetInteger("Level", currentLevel);
    }
}
