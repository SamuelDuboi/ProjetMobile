using UnityEngine;
using System.Collections;
public class MenuChapitre : ObjectHandler
{
    public int index;
    public bool quit;
    public bool isPlaying;
    public override void Start()
    {
        base.Start();
    }
    public override void Interact(GameObject currentGameObject)
    {
        if (HitBoxZoom != null && currentGameObject == HitBoxZoom.gameObject && !isPlaying)
        {
            isPlaying = true;
            soundR.Play();
            StartCoroutine(WaitForSound());
        }
    }

    IEnumerator WaitForSound()
    {
        yield return new WaitForSeconds(1f);
        if (quit)
            Application.Quit();
        if (index == 0)
            SaveManager.instance.SaveTuto(false);
        SaveManager.instance.LoadScene(index);
    }
}

