using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public GameObject[] gears;
    public GameObject[] gearsChild;
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
            gearsChild[i].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 360 *( number )/ 10));
            Debug.Log(gearsChild[i].transform.localRotation.eulerAngles.z);
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
                        _gameObject = gearsChild[i];
                        break;
                    }
                }
            }
            if(touch.phase == TouchPhase.Moved)
            {
                if (_gameObject == null)
                    return;
                // apply rotation
                _rotation.z = (touch.deltaPosition.x + touch.deltaPosition.y) * _sensitivity;

                // rotate
                _gameObject.transform.Rotate(_rotation, Space.Self);

            }
            if(touch.phase== TouchPhase.Ended)
            {
                for (int i = 0; i <10; i++)
                {

                    if (_gameObject.transform.localRotation.eulerAngles.z >= 360 *i/10 && _gameObject.transform.localRotation.eulerAngles.z < 360 *(i + 1)/10) 
                    {
                        _gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 360 * i / 10));
                        break;
                    }                    
                }
                for (int i = 0; i < gears.Length; i++)
                {
                    if(gearsChild[i].transform.localRotation.eulerAngles.z != 360 * (int.Parse(endNumber[i].ToString())) / 10)
                    {
                        if(gearsChild[i].transform.localRotation.eulerAngles.z >= 360 * (int.Parse(endNumber[i].ToString())) / 10 -1 && gearsChild[i].transform.localRotation.eulerAngles.z <= 360 * (int.Parse(endNumber[i].ToString())) / 10 + 1)
                        {
                          
                        }
                        else
                        {
                            Debug.Log(i + "" + gearsChild[i].transform.localRotation.eulerAngles.z);
                            Debug.Log(i + "" + 360 * (int.Parse(endNumber[i].ToString())) / 10);
                            return;
                        }

                    }
                    
                }
                GetComponentInParent<ObjectHandler>().trialInstantiate = null;
                GetComponentInParent<ObjectHandler>().interactifElement.spawnNewTrial = false;
                GetComponentInParent<ObjectHandler>().Interact(GetComponentInParent<ObjectHandler>().HitBoxZoom.gameObject);
                Destroy(gameObject);

            }
        }
    }
}
