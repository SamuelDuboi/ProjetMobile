﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToGrab : ObjectHandler
{
    private bool destoyed;
    public override void Start()
    {
        base.Start();
        EventManager.instance.SwipeUp += Swipe;
    }

    private int cpt;

    public ParticleSystem waterSystem;
    public override void CollectObject(GameObject currentGameObject)
    {
        if (currentGameObject == HitBoxZoom.gameObject && interactifElement.inventory)
        {
            cpt++;
            var main = waterSystem.main;
            main.startSize = new ParticleSystem.MinMaxCurve(cpt*0.5f+0.2f, cpt*0.5f+0.2f);
            interactifElement.interactionAnimator.SetTrigger("Shake");
            if (cpt > 4)
            {
                interactifElement.interactionAnimator.SetTrigger("Interact");
                InventoryManager.Instance.AddList(gameObject, interactifElement.nameInventory, interactifElement.inventoryTexture);
                TheWaterManager.instance.WaterUp();
                var speed = waterSystem.main.startSpeed;
                main.startSize = new ParticleSystem.MinMaxCurve(2f, 2f);
                HitBoxZoom.gameObject.SetActive(false);
                StartCoroutine(WaterCaroutine(main));
                destoyed = true;
            }
        }
    }

    IEnumerator WaterCaroutine(ParticleSystem.MainModule mainModule)
    {
        float timer = 0;
        while (timer < 2f)
        {
            timer += 0.01f;
            mainModule.startSize = new ParticleSystem.MinMaxCurve(2f-timer, 2f-timer);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(transform.parent.gameObject);
    }

    private void Swipe(bool up)
    {
        if (!destoyed)
        {
            var main = waterSystem.main;
            if (EventManager.instance.uspideDown)
            {
                main.gravityModifier = new ParticleSystem.MinMaxCurve(-13f, -13f);
            }
            else
            {
                main.gravityModifier = new ParticleSystem.MinMaxCurve(13f, 13f);
            }
        }
    }
}
