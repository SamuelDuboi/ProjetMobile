using System.Collections;
using UnityEngine;

public class CamBehavior : MonoBehaviour
{
    private bool cantRotate;

    private float timer;
    private Vector3 position;
    private float direction;
    private int currentAngle;

    private Vector3 initialPos;
    public AnimationCurve camCurve;
    public float horizontalFoV = 60f;
    Camera cam;
    private float initialOrthographicSize;

    private void Start()
    {
        EventManager.instance.ZoomIn += MoveCam;
        EventManager.instance.ZoomOut += UnZoom;        
        SetCamFov();
        initialPos = cam.transform.position;
        initialOrthographicSize = cam.orthographicSize;
    }

    private void SetCamFov()
    {
        cam = GetComponent<Camera>();
        float halfWidth = Mathf.Tan(0.5f * horizontalFoV * Mathf.Deg2Rad);

        float halfHeight = halfWidth * Screen.height / Screen.width;

        float verticalFoV = 2.0f * Mathf.Atan(halfHeight) * Mathf.Rad2Deg;

        cam.orthographicSize = verticalFoV;
    }
    private void MoveCam(Vector3 position, float direction, int angle, float orthogrphicSize)
    {

        if (!cantRotate && direction != default && !EventManager.instance.isZoomed)
        {
            ZoomLunch(position, direction, angle, orthogrphicSize);
        }
        else 
            EventManager.instance.cantDoZoom = false;
    }
    private void UnZoom()
    {
        if (EventManager.instance.isZoomed)
        {
            StartCoroutine(ZoomCoroutine(position, direction, -1, currentAngle));
        }
        else
            EventManager.instance.cantDoZoom = false;
    }


    private void ZoomLunch(Vector3 _position, float direction, int angle, float orhtoGraphicSzze)
    {
        cantRotate = true;
        this.position = _position;
        this.direction = direction;
        currentAngle = angle;
        StartCoroutine(ZoomCoroutine(position, direction, 1,angle, orhtoGraphicSzze ));
    }

    IEnumerator ZoomCoroutine(Vector3 _position, float direction, float multiplicator, int angle =45, float FinalOrthographicZoom = 100)
    {
      //  _position = new Vector3(_position.x - 1, _position.y - 1, _position.z - 1);
        Vector3 directionVector = Vector3.down;
        cam.nearClipPlane = -17;

        //Vector3 positionToGo = new Vector3((-0.2f - _position.x), 5 - _position.y, 1 - _position.z);
        Vector3 positionToGo = new Vector3(_position.x -cam.transform.position.x, _position.y - cam.transform.position.y, _position.z - cam.transform.position.z);
        if (Mathf.Abs(direction) == 2)
        {
            direction = direction * 0.5f;
            directionVector = Vector3.right;
            cam.nearClipPlane = -3.5f;
          /* _position = new Vector3(_position.x - 1, _position.y - 4, _position.z - 4);
             while (timer < 0.75f)
              {
                  timer += Time.deltaTime; 
                  // multiply by 2 to compsate the loss of the cam curve, not an exact value but good enough
                  cam.transform.RotateAround(_position, directionVector, 120*Time.deltaTime* camCurve.Evaluate(timer / .75f) * multiplicator * direction);
                  cam.orthographicSize -= camCurve.Evaluate(timer / .75f) * 10 * Time.deltaTime/.75f * multiplicator;
                  cam.transform.position = Vector3.MoveTowards(transform.position, _position, 17 * Time.deltaTime / .75f * multiplicator);
                  yield return new WaitForFixedUpdate();
              }

           // StartCoroutine(Translate(positionToGo, multiplicator));
            //StartCoroutine(Rotate(directionVector, angle, multiplicator, direction, orthographicSizeToChange));
            timer = 0;

            EventManager.instance.isZoomed = !EventManager.instance.isZoomed;
            cantRotate = false;
            EventManager.instance.cantDoZoom = false;
            yield break;*/
        }
        float orthographicSizeToChange = 0;
        if (FinalOrthographicZoom != 100)
            orthographicSizeToChange = initialOrthographicSize - FinalOrthographicZoom;
        else
            orthographicSizeToChange = initialOrthographicSize - cam.orthographicSize;

        //StartCoroutine(Translate(positionToGo, multiplicator));
        //StartCoroutine(Rotate(directionVector, angle, multiplicator, direction, orthographicSizeToChange));
        angle = angle / 3;
        if (multiplicator < 0)
            positionToGo = new Vector3(initialPos.x - cam.transform.position.x, initialPos.y - cam.transform.position.y, initialPos.z - cam.transform.position.z);
        else
            initialPos = cam.transform.position;
        while (timer < 0.5)
        {
            if (timer > 0.1f && timer < .2f)
            {
                angle += 1;
            }
            else if (timer > 0.2f && timer < .3f)
            {
                angle += 2;
            }
           
            else if (timer > .4f)
                angle -= 3;
            
            timer += 0.01f;
            // multiply by 2 to compsate the loss of the cam curve, not an exact value but good enough
            //cam.transform.RotateAround(_position, directionVector, angle/75f * 2*camCurve.Evaluate(timer / .75f) * multiplicator * direction);

            cam.transform.Rotate(directionVector,angle/33f* multiplicator * direction, Space.World );
            cam.orthographicSize -= orthographicSizeToChange / 50*2* camCurve.Evaluate(timer / .5f)  * multiplicator;
            //cam.transform.position -= positionToGo /75f * multiplicator;
            cam.transform.Translate (positionToGo/50f, Space.World);

           // cam.transform.Translate (0,0,-0.1f, Space.Self);
            yield return new WaitForSeconds(0.01f);
        }

        // try with bool
      /*  Debug.Log(cam.transform.position);
        Debug.Log(_position);
        bool stop = false;
        int i= 0;
        while (!stop)
        {
            if (timer > 0.1f && timer < .2f)
            {
                angle += 1;
            }
            else if (timer > 0.2f && timer < .3f)
            {
                angle += 2;
            }

            else if (timer > .4f)
                angle -= 3;

            timer += 0.01f;
            // multiply by 2 to compsate the loss of the cam curve, not an exact value but good enough
            //cam.transform.RotateAround(_position, directionVector, angle/75f * 2*camCurve.Evaluate(timer / .75f) * multiplicator * direction);
            i++;
            cam.transform.Rotate(directionVector, angle / 33f * multiplicator * direction, Space.World);
            cam.orthographicSize -= orthographicSizeToChange / 50 * 2 * camCurve.Evaluate(timer / .5f) * multiplicator;
            //cam.transform.position -= positionToGo /75f * multiplicator;
          
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, _position, 20* 0.01f);
            if (Mathf.Abs(cam.transform.position.x) <= Mathf.Abs(_position.x) + 0.5f && Mathf.Abs(cam.transform.position.x) >= Mathf.Abs(_position.x) - 0.5f &&
                Mathf.Abs(cam.transform.position.y) <= Mathf.Abs(_position.y) + 0.5f && Mathf.Abs(cam.transform.position.y) >= Mathf.Abs(_position.y) - 0.5f&&
                Mathf.Abs(cam.transform.position.z) <= Mathf.Abs(_position.z) + 0.5f && Mathf.Abs(cam.transform.position.z) >= Mathf.Abs(_position.z) - 0.5f)
            {
                stop = true;
                Debug.Log(i);
            }
            // cam.transform.Translate (0,0,-0.1f, Space.Self);
            yield return new WaitForSeconds(0.01f);
        }*/
        timer = 0;
        EventManager.instance.isZoomed = !EventManager.instance.isZoomed;
        cantRotate = false;
        EventManager.instance.cantDoZoom = false;
    }
  /*  IEnumerator Rotate(Vector3 directionVector, int angle, float multiplicator, float direction, float orthographicSizeToChange)
    {
        
        float timer = 0;
        while (timer < 0.75f)
        {
            timer += 0.01f;
            // multiply by 2 to compsate the loss of the cam curve, not an exact value but good enough
            //cam.transform.RotateAround(_position, directionVector, angle/75f * 2*camCurve.Evaluate(timer / .75f) * multiplicator * direction);
            cam.transform.Rotate(directionVector, angle / 75f * 2f * camCurve.Evaluate(1-(timer / .75f)) * multiplicator * direction, Space.World);
            cam.orthographicSize -= orthographicSizeToChange / 75 * 2 * camCurve.Evaluate(1-(timer / .75f)) * multiplicator;
            yield return new WaitForSeconds(0.01f);
        }
    }
    private bool doOnce;
    IEnumerator Translate(Vector3 positionToGo, float multiplicator)
    {
        if(multiplicator<0 && !doOnce)
        {
            StartCoroutine(TranslateWorld(positionToGo, multiplicator));
            yield break;
        }    
        float timer = 0;
        while (timer < 0.75f)
        {
            if (!doOnce && timer > 0.5f)
                StartCoroutine(TranslateWorld(positionToGo, multiplicator));
            timer += 0.01f;
            cam.transform.Translate(positionToGo / 75f * 2f * camCurve.Evaluate(timer / .75f) * multiplicator, Space.World);
            yield return new WaitForSeconds(0.01f);
        }
        doOnce = false;
    }
    IEnumerator TranslateWorld(Vector3 positionToGo, float multiplicator)
    {
        doOnce = true;
        float timer = 0;
        while (timer < 0.25f)
        {
            timer += 0.01f;
            cam.transform.Translate(0, 0, -0.1f*multiplicator, Space.Self);
            yield return new WaitForSeconds(0.01f);
        }
        timer = 0;
        if (multiplicator < 0)
            StartCoroutine(Translate(positionToGo, multiplicator));
    }*/

}
