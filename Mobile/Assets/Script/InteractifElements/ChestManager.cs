using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public bool dontDestroyHitBox;
    public Chest[] chests;
    public string initialNumber;
    public string finalNumbers;
    [Range(0, 1)]
    public float sensibilty = 0.5f;
    private bool[] results;
    public bool save;
    public bool destroyHitBoxParent;
    public GameObject colliderGO;
    public SoundReader soundReader;
    public bool isTuto;
    [HideInInspector] public bool cantAct;
    void Start()
    {
        for (int i = 0; i < chests.Length; i++)
        {
            chests[i].Init(int.Parse(initialNumber[i].ToString()), sensibilty,i, this);
        }
        results = new bool[chests.Length];
        EventManager.instance.ZoomOut += Unzoom;
        if (isTuto)
            if (FingerTipsManager.instance.tutoDeviceManager.phase > 8)
                cantAct = false;
            else
                cantAct = true;
      /*  if(destroyHitBoxParent)
            GetComponentInParent<ObjectHandler>().HitBoxZoom.gameObject.SetActive(false);*/
    }

 
    public void TryValidate(int index, int indexInArray)
    {
        if (!cantAct)
        {
            if(index == int.Parse(finalNumbers[indexInArray].ToString()))
            {
                results[indexInArray] = true;
            }
            else
            {
                results[indexInArray] = false;
            }
            foreach (var result in results)
            {
                if (!result)
                    return;
            }
            if(soundReader!= null)
            soundReader.Play();
            StartCoroutine(waitForSoun());
        }
    }

    IEnumerator waitForSoun()
    {
        yield return new WaitForSeconds (1f);
        GetComponentInParent<ObjectHandler>().trialInstantiate = null;
        GetComponentInParent<ObjectHandler>().interactifElement.spawnNewTrial = false;
        if (dontDestroyHitBox)
        {
            GetComponentInParent<ObjectHandler>().interactifElement.onlyZoom = false;
            GetComponentInParent<ObjectHandler>().HitBoxZoom.enabled = true;
        }
        if (save)
            if (SaveManager.instance != null)
                SaveManager.instance.SaveChapter2();
            else Debug.LogError("No instance of save Manager");
        /*  if (destroyHitBoxParent)
          {
              GetComponentInParent<ObjectHandler>().HitBoxZoom.gameObject.SetActive(true);
              Destroy(GetComponentInParent<ObjectHandler>().HitBoxZoom);

          }*/
        GetComponentInParent<ObjectHandler>().Interact(GetComponentInParent<ObjectHandler>().HitBoxZoom.gameObject);
        EventManager.instance.ZoomOut -= Unzoom;
        EventManager.instance.OnDestroyTrial();
        if (FingerTipsManager.instance != null)
        {
            FingerTipsManager.instance.startCollect();
        }
        Destroy(transform.parent.gameObject);
    }

    private void Unzoom()
    {
        var parent = GetComponentInParent<ObjectHandler>();
        parent.interactifElement.onlyZoom = false;
        /*if (destroyHitBoxParent)
        {
            GetComponentInParent<ObjectHandler>().HitBoxZoom.gameObject.SetActive(true);

        }*/
        parent.HitBoxZoom.enabled = true;
        parent.interactifElement.spawnNewTrial = true;
        EventManager.instance.ZoomOut -= Unzoom;
        EventManager.instance.OnDestroyTrial();
        Destroy(parent.trialInstantiate);
    }
}
