using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBurned : MonoBehaviour
{

    public Animator burnedRope;
    private bool doOnce;
    public bool botomRope;
    public bool statueRope;
    private float timer;

    private void Start()
    {
        EventManager.instance.LightObject += LightOn;
    }
    public void LightOn(GameObject _gameObject)
    {
        if (gameObject == _gameObject && !doOnce)
        {
            if(botomRope && timer < 0.5f)
            {
                timer += Time.deltaTime;
                return;
            }
            if (statueRope)
                TipsManager.instance.changeIndex(1);
            doOnce = true;
            burnedRope.SetTrigger("Interact");
            EventManager.instance.OnZoomOut();
            if(gameObject.layer == 10)
                SaveManager.instance.SaveChapter1();

        }
    }

}
