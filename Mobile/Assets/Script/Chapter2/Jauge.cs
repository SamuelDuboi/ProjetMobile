using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jauge : MonoBehaviour
{
    public GameObject[] aiguille;
    public float timer = 0;
    private bool jaugesResolved = false;
    public int index;
    public int[] jaugeIndex;
    private void Start()
    {
        EventManager.instance.ZoomOut += Unzoom;
    }
    public void AiguilleRotation(GameObject button)
    {
        if (timer == 0 && jaugesResolved == false)
            StartCoroutine(RotateAiguille(button));
    }

    private IEnumerator RotateAiguille(GameObject button)
    {
        for (int i = 0; i < aiguille.Length; i++)
        {
            if (aiguille[i] == button)
            {
                index = i;
                break;
            }
            else
            {
                index = 1000;
            }
        }
        if (index != 1000)
        {
            if (jaugeIndex[index] == 8)
            {
                jaugeIndex[index] = 0;
                while (timer <= 0.20f)
                {
                    timer += 0.01f;
                    button.transform.Rotate(Vector3.forward, 159.6f / 20f);
                    yield return new WaitForSeconds(0.01f);
                }
            }
            else
            {
                jaugeIndex[index] += 1;
                while (timer <= 0.20f)
                {
                    timer += 0.01f;
                    button.transform.Rotate(Vector3.forward, -19.95f / 20f);
                    yield return new WaitForSeconds(0.01f);
                }
            }
            timer = 0f;
        }
        JaugeChecker();
    }

    public void JaugeChecker()
    {
        if(jaugeIndex[0] == 4 && jaugeIndex[1] == 2 && jaugeIndex[2] == 7)
        {
            GetComponentInParent<ObjectHandler>().trialInstantiate = null;
            EventManager.instance.OnDestroyTrial();

            GetComponentInParent<ObjectHandler>().interactifElement.spawnNewTrial = false;
            InventoryManager.Instance.AddList(gameObject, "LevierBleu", default, 0);
            StartCoroutine(GetComponentInParent<ActivateStick>().ActivateWaterStick());
            EventManager.instance.ZoomOut -= Unzoom;
            Destroy(gameObject);

        }
    }

    private void Unzoom()
    {
        var parent = GetComponentInParent<ObjectHandler>();
        if (parent)
        {
            parent.interactifElement.onlyZoom = false;
            parent.HitBoxZoom.enabled = true;
            parent.interactifElement.spawnNewTrial = true;
            EventManager.instance.ZoomOut -= Unzoom;
            Destroy(parent.trialInstantiate);
        }
    }
}
