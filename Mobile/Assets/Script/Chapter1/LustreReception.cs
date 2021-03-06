using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LustreReception : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private bool doOnce;
    private void Start()
    {
        EventManager.instance.LightObject += LightOn;
    }
    public void LightOn(GameObject _gameObject)
    {
        if(gameObject == _gameObject && !doOnce)
        {
            doOnce = true;
            lineRenderer.enabled = true;
            InventoryManager.Instance.AddList(gameObject);
        }
    }
}
