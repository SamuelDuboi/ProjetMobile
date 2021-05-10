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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            LightmapSettings.lightmaps = lightmaps2;
        }
    }
    /*  void Update()
      {
            if(*La trappe se ferme*)
              {
              LightmapSettings.lightmaps = lightmaps2;
              }
          else
              {

              }
          if (*La trappe se reouvre *)
              {
              LightmapSettings.lightmaps = lightmaps1;
              }
          else
              {

              }

      }
      */
}
