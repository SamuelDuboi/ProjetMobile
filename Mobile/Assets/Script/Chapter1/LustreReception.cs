using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LustreReception : MonoBehaviour
{
    private bool doOnce;
    public ObjectHandler main;
    public ObjectHandler bookHandler;
    public Material newMat;
    public Cristal[] cristals;
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
            EventManager.instance.ZoomOut -= main.UnZoom;
            main.HitBoxZoom.enabled = false;
            main.enabled = false;
            foreach (var cristal in cristals)
            {
                cristal.GetComponent<MeshRenderer>().material = newMat;
                EventManager.instance.InteractObject -= cristal.Interact;
                cristal.enabled = false;
            }
            bookHandler.enabled = true;
        }
    }
}
