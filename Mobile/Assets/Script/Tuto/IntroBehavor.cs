using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class IntroBehavor : MonoBehaviour
{
    public FingerTipsManager tipsManager;
    public GameObject buttons;
    public VideoPlayer[] intro;
    private bool doOnce;
    public GameObject pass;
    private IEnumerator Start()
    {
        if (SaveManager.instance.skipIntro)
        {
            tipsManager.canStart = true;
            SaveManager.instance.LoadTuto(gameObject);
            yield break;
        }
        else
        {
            intro[0].gameObject.SetActive(true);
            SaveManager.instance.skipIntro = true;
            yield return new WaitUntil(() => intro[0].time >= intro[0].length-0.1f);
            if (pass.activeSelf)
                pass.SetActive(false);
            intro[0].gameObject.SetActive(false);
            buttons.SetActive(true);
        }
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                intro[0].Play();
            }
            if (touch.phase == TouchPhase.Ended && !pass.activeSelf && intro[0].gameObject.activeSelf && SaveManager.instance.hasDoneTuto)
            {
                pass.SetActive(true);
            }
        }
    }
    public void Quite()
    {
        if (!doOnce)
            StartCoroutine(FadeQuite());
    }
    public void PlayeTuto()
    {
        if(!doOnce)
        StartCoroutine(FadePlay());
    }
    IEnumerator FadePlay()
    {
        doOnce = true;
        intro[1].gameObject.SetActive(true);
        buttons.SetActive(false);
        yield return new WaitUntil(() => intro[1].time >= intro[1].length-0.1f);
        tipsManager.canStart = true;
        SaveManager.instance.LoadTuto(gameObject);
    }
    IEnumerator FadeQuite()
    {
        doOnce = true;
        intro[2].gameObject.SetActive(true);
        buttons.SetActive(false);
        yield return new WaitUntil(() => intro[2].time >= intro[2].length-0.1f);
        SaveManager.instance.Quit();
    }
    public void Pass()
    {
        tipsManager.canStart = true;
        SaveManager.instance.LoadTuto(gameObject);
    }
}
