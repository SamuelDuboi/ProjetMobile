using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public GameObject[] gears;
    private GameObject _gameObject;
    public float _sensitivity = 1f;
    private Vector3 _rotation = Vector3.zero;

    public string initiNumber;
    public string endNumber;

    private void Start()
    {
        transform.position = new Vector3(Camera.main.transform.position.x - Camera.main.transform.forward.x*0.5f, Camera.main.transform.position.y - Camera.main.transform.forward.y*0.5f, Camera.main.transform.position.z - Camera.main.transform.forward.z*0.5f);
        transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles);
        for (int i = 0; i < gears.Length; i++)
        {
            int number = int.Parse(initiNumber[i].ToString());
            gears[i].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 360 *( number -1)/ 10));
        }
        GetComponentInParent<ObjectHandler>().interactifElement.spawnNewTrial = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            var ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit = new RaycastHit();
            //cast a ray to zoomable object if null, cast a ray to interactible object, if null try to unzoom
            LayerMask mask = 1<<9;
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                if(hit.collider.gameObject == gameObject)
                {
                    GetComponentInParent<ObjectHandler>().interactifElement.spawnNewTrial = true;
                    GetComponentInParent<ObjectHandler>().trialInstantiate = null;
                    Destroy(gameObject);
                }
                for (int i = 0; i < gears.Length; i++)
                {
                    if(gears[i] == hit.collider.gameObject)
                    {
                        _gameObject = gears[i].transform.GetChild(0).gameObject;
                        break;
                    }
                }
            }
            if(touch.phase == TouchPhase.Moved)
            {

                // apply rotation
                _rotation.z = (touch.deltaPosition.x + touch.deltaPosition.y) * _sensitivity;

                // rotate
                _gameObject.transform.Rotate(_rotation, Space.Self);

            }
            if(touch.phase== TouchPhase.Ended)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (_gameObject.transform.localRotation.eulerAngles.z >= 360 *i/10 && _gameObject.transform.localRotation.eulerAngles.z < 360 *(i + 1)/10) 
                    {
                        _gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 360 * i / 10));
                        break;
                    }
                }
                for (int i = 0; i < gears.Length; i++)
                {
                    if(gears[i].transform.localRotation.eulerAngles.z != 360 * (int.Parse(endNumber[i].ToString()) - 1) / 10)
                    {
                        Debug.Log(i.ToString() + gears[i].transform.localRotation.eulerAngles.z);
                        Debug.Log(i.ToString() + 360 * (int.Parse(endNumber[i].ToString())-1) / 10);
                        return ;
                    }
                    
                }
                GetComponentInParent<ObjectHandler>().trialInstantiate = null;
                GetComponentInParent<ObjectHandler>().Interact(GetComponentInParent<ObjectHandler>().gameObject);
                GetComponentInParent<ObjectHandler>().interactifElement.spawnNewTrial = false;
                Destroy(gameObject);

            }
        }
    }
}
