using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBurned : MonoBehaviour
{

    public Animator burnedRope;
    private bool doOnce;
    private void Start()
    {
        EventManager.instance.LightObject += LightOn;
    }
    public void LightOn(GameObject _gameObject)
    {
        if (gameObject == _gameObject && !doOnce)
        {
            doOnce = true;
            burnedRope.SetTrigger("Interact");
        }
    }
}
