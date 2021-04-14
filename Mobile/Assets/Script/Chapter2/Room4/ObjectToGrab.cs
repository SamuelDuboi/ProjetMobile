﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToGrab : ObjectHandler
{

    private int cpt;

    public ParticleSystem waterSystem;
    public override void CollectObject(GameObject currentGameObject)
    {
        if (currentGameObject == HitBoxZoom.gameObject && interactifElement.inventory)
        {
            cpt++;
            interactifElement.interactionAnimator.SetTrigger("Shake");
            if (cpt > 4)
            {
                interactifElement.interactionAnimator.SetTrigger("Interact");
                InventoryManager.Instance.AddList(gameObject, interactifElement.nameInventory, interactifElement.inventoryTexture);
                TheWaterManager.instance.WaterUp();
                var main = waterSystem.main;
                var speed = waterSystem.main.startSpeed;
                main.startSize = new ParticleSystem.MinMaxCurve(2f, 2f);
                HitBoxZoom.gameObject.SetActive(false);
                StartCoroutine(WaterCaroutine(main));
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
}