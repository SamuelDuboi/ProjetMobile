using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LustreReception : MonoBehaviour
{
    private bool doOnce;
    public Collider main;
    public ObjectHandler bookHandler;

    private void Start()
    {
        EventManager.instance.LightObject += LightOn;
    }
    public void LightOn(GameObject _gameObject)
    {
        if(gameObject == _gameObject && !doOnce)
        {
            doOnce = true;
            InventoryManager.Instance.AddList(gameObject,"room1Final",default);
            EventManager.instance.OnZoomOut();
            main.enabled = false;
            bookHandler.enabled = true;
        }
    }
}
