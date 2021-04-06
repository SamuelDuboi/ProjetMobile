using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    public Animator waterAnim;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.SwipeUp += UpsideDown;
    }

   public virtual void UpsideDown(bool up)
    {
        waterAnim.SetTrigger("UpsideDown");
    }
}
