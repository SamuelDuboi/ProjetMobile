using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject[] tuiles;
    public int index;
    public int[] indexRotate;
    public float timer = 0;
    private bool puzzleResolved = false;

    public void Start()
    {
        EventManager.instance.ZoomOut += Unzoom;
        for (int i = 0; i < indexRotate.Length; i++)
        {
            indexRotate[i] = 0;
        }
    }

    public void StartRotationPuzzle(GameObject button)
    {
        if (timer == 0 && puzzleResolved == false)
            StartCoroutine(RotateTouchePuzzle(button));
    }

    private IEnumerator RotateTouchePuzzle(GameObject button)
    {
        for (int i = 0; i < tuiles.Length; i++)
        {
            if (tuiles[i] == button)
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
            if (indexRotate[index] == 3)
            {
                indexRotate[index] = 0;
            }
            else
            {
                indexRotate[index] += 1;
            }
            timer = 0f;
        }
        while (timer <= 0.09f)
        {
            timer += 0.01f;
            button.transform.Rotate(Vector3.forward, -(90f / 10f));
            yield return new WaitForSeconds(0.01f);
        }
        timer = 0;

        PuzzleSolution();
    }

    void PuzzleSolution()
    {
        if (indexRotate[0] == 2 && indexRotate[1] == 0 && (indexRotate[2] == 1 || indexRotate[2] == 3) && indexRotate[3] == 1)
        {
            puzzleResolved = true;
            GetComponentInParent<ObjectHandler>().trialInstantiate = null;
            EventManager.instance.OnDestroyTrial();

            GetComponentInParent<ObjectHandler>().interactifElement.spawnNewTrial = false;
            InventoryManager.Instance.AddList(GetComponentInParent<ObjectHandler>().gameObject, GetComponentInParent<ObjectHandler>().NameToAddIfAnimToAdd, default, 1);
            GetComponentInParent<ObjectHandler>().Interact(GetComponentInParent<ObjectHandler>().HitBoxZoom.gameObject);
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
