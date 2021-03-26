using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    [SerializeField] private bool isWallUp;
    private bool upsideDown;
    private void Start()
    {
        EventManager.instance.SwipeUp += LunchChangeRoomRotation;
    }
    public IEnumerator ChangePosition(bool up)
    {
        float multiplicateur = 1;
        if (up)
        {
            isWallUp = false;
            multiplicateur = -1;
        }
        else
        {
            isWallUp = true;
        }
        if (!upsideDown)
        {
            for (int i = 0; i < 50; i++)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + multiplicateur * (7f * i / 50) / 10f, transform.localPosition.z);
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            for (int i = 0; i < 50; i++)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - multiplicateur * (7f * i / 50) / 10f, transform.localPosition.z);
                yield return new WaitForSeconds(0.01f);

            }

        }
    }

    public void ChangePositionEditor(bool up)
    {
        float multiplicateur = 1;
        if (up)
        {
            isWallUp = false;
            multiplicateur = -1;
        }
        else
        {
            isWallUp = true;
        }
        if (!upsideDown)
        {

            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + multiplicateur * 17.15f, transform.localPosition.z);

        }
        else
        {

            transform.localPosition = new Vector3(transform.localPosition.x,0, transform.localPosition.z);

        }
    }

    public void LunchChangeRoomRotation(bool up)
    {
        upsideDown = !upsideDown;

        if (isWallUp)
        {
            StartCoroutine(ChangeRoomRotation());
        }


    }

    private IEnumerator ChangeRoomRotation()
    {
        yield return new WaitForSeconds(0.5f);
        if (upsideDown)
        {
            for (int i = 0; i < 50; i++)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 2 * (7f * i / 50) / 10f, transform.localPosition.z);
            }
        }
        else
        {
            for (int i = 0; i < 50; i++)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 2 * (7f * i / 50) / 10f, transform.localPosition.z);
            }

        }
    }

}
