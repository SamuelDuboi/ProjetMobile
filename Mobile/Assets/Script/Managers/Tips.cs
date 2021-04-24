using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewTips", menuName ="Tips")]
public class Tips : ScriptableObject
{

    public List<Tip> tips;

    public bool tipsDone;


    public void Reset()
    {
        foreach (Tip tip in tips)
        {
            tip.done = false;
        }
    }

    public void TryTips()
    {
        for (int i = 0; i < tips.Count; i++)
        {
            if (!tips[i].done)
            {
                if(EventManager.instance.)
                return;
            }
        }
    }    
}
