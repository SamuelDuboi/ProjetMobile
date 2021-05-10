using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightSwitch : MonoBehaviour
{
    public Texture2D allume;
    public Texture2D eteint;

    private LightmapData[] lightmaps1= new LightmapData[1];
    private LightmapData[] lightmaps2 = new LightmapData[1];


    void Start()
    {
        lightmaps1[0] = new LightmapData();
        lightmaps1[0].lightmapColor = allume;

        lightmaps2[0] = new LightmapData();
        lightmaps2[0].lightmapColor = eteint;
        LightmapSettings.lightmaps = lightmaps1;
    }

    
    public void ActiveLight()
    {
        LightmapSettings.lightmaps = lightmaps2;
    }
    public void UnactiveLight()
    {
        LightmapSettings.lightmaps = lightmaps1;
    }
}
