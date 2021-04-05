using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiLight : MonoBehaviour
{
    public MeshRenderer numberLighted;
    private bool doOnce;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.LightObject += LightOn;
    }

   private void LightOn (GameObject gameObject)
    {
        if(gameObject = this.gameObject)
        {
            if (!doOnce)
            {
                numberLighted.enabled = true;
                doOnce = true;
            }
        }
    }
}
