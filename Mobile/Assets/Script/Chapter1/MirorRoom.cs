using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirorRoom : ObjectHandler
{
    public MeshRenderer[] mesh;
    public override void Start()
    {
        base.Start();

        StartCoroutine(wait());
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(0.1f);
        OnSwipe();
        EventManager.instance.SwipeLeft += OnSwipe;
        EventManager.instance.SwipeRight += OnSwipe;
    }
 private void OnSwipe()
    {
        StartCoroutine(waitForSwipe());
    }
    IEnumerator waitForSwipe()
    {

        yield return new WaitForSeconds(0.3f);
        mesh[0].enabled = false;
        mesh[1].enabled = false;
        foreach (var cams in interactifElement.cams)
        {
            if (EventManager.instance.cuurrentCamDirection == cams.camDirection)
            {
                mesh[0].enabled = true;
                mesh[1].enabled = true;
                break;
            }
        }
    }
}
