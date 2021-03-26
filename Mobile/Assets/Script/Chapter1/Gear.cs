using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public GameObject[] gears;
    public GameObject[] gearsChild;
    public GameObject[] lights;
    private GameObject _gameObject;
    public float _sensitivity = 1f;
    private Vector3 _rotation = Vector3.zero;

    public string initiNumber;
    public string endNumber;

    private bool down;
    private void Start()
    {
      /*  transform.position = new Vector3(Camera.main.transform.position.x - Camera.main.transform.forward.x*0.5f, Camera.main.transform.position.y - Camera.main.transform.forward.y*0.5f, Camera.main.transform.position.z - Camera.main.transform.forward.z*0.5f);
        transform.localRotation = Quaternion.Euler(Camera.main.transform.localRotation.eulerAngles );*/
        for (int i = 0; i < gears.Length; i++)
        {
            int number = int.Parse(initiNumber[i].ToString());
            gearsChild[i].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 360 *( number )/ 10));
        }
        GetComponentInParent<ObjectHandler>().interactifElement.spawnNewTrial = true;

    }

    public void ButtonPress( GameObject button)
    {
        if (down)
            return;
        for (int i = 0; i < gears.Length; i++)
        {
            if (gears[i] == button)
            {
                if(_gameObject != gearsChild[i])
                {
                    _gameObject = gearsChild[i];
                    lights[i].SetActive(true);
                }/*
                else
                {
                    _gameObject = null;
                    lights[i].SetActive(false);
                }*/

            }
            else
                lights[i].SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (_gameObject == null)
                return;
            if (touch.phase == TouchPhase.Moved)
            {
                down = true;
                // apply rotation
                _rotation.z = (touch.deltaPosition.x + touch.deltaPosition.y) * _sensitivity;

                // rotate
                _gameObject.transform.Rotate(_rotation, Space.Self);

            }
            if(touch.phase== TouchPhase.Ended)
            {
                down = false;
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
