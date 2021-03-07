using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LustreReception : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private bool doOnce;
    public Cristal[] cristals;
    private void Start()
    {
        EventManager.instance.LightObject += LightOn;
    }
    public void LightOn(GameObject _gameObject)
    {
        if(gameObject == _gameObject && !doOnce)
        {
            foreach (var cristal in cristals)
            {
                if (!cristal.isGood)
                    return;
            }
            doOnce = true;
            lineRenderer.enabled = true;
            InventoryManager.Instance.AddList(gameObject);
            EventManager.instance.OnZoomOut();
        }
    }
}
