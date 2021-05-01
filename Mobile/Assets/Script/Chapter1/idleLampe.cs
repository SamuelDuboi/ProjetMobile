using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleLampe : Singleton<idleLampe>
{
    //public LustreReception lustreReception;

    private void Awake()
    {
        CreateSingleton();
    }
    // Start is called before the first frame update
    void Start()
    {
       gameObject.transform.Translate(-30f, 8.547f, 0);
    }


}
