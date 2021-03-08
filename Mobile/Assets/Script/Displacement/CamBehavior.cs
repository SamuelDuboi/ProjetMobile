using System.Collections;
using UnityEngine;

public class CamBehavior : MonoBehaviour
{
    private bool cantRotate;

    private float timer;
    private Vector3 position;
    private float direction;


    public AnimationCurve camCurve;
    public float horizontalFoV = 60f;
    Camera cam;

    private void Start()
    {
        EventManager.instance.ZoomIn += MoveCam;
        EventManager.instance.ZoomOut += UnZoom;

        SetCamFov();        
    }

    private void SetCamFov()
    {
        cam = GetComponent<Camera>();
        float halfWidth = Mathf.Tan(0.5f * horizontalFoV * Mathf.Deg2Rad);

        float halfHeight = halfWidth * Screen.height / Screen.width;

        float verticalFoV = 2.0f * Mathf.Atan(halfHeight) * Mathf.Rad2Deg;

        cam.orthographicSize = verticalFoV;
    }
    private void MoveCam(Vector3 position, float direction)
    {

        if (!cantRotate && direction != default && !EventManager.instance.isZoomed)
        {
            ZoomLunch(position, direction);
        }
        else 
            EventManager.instance.cantDoZoom = false;
    }
    private void UnZoom()
    {
        if (EventManager.instance.isZoomed)
        {
            StartCoroutine(ZoomCoroutine(position, direction, -1));
        }
        else
            EventManager.instance.cantDoZoom = false;
    }


    private void ZoomLunch(Vector3 _position, float direction)
    {
        cantRotate = true;
        this.position = _position;
        this.direction = direction;
        StartCoroutine(ZoomCoroutine(position, direction, 1));
    }


    IEnumerator ZoomCoroutine(Vector3 _position, float direction, float multiplicator)
    {
        _position = new Vector3(_position.x - 1, _position.y - 1, _position.z - 1);
        Vector3 directionVector = Vector3.down;
        if (Mathf.Abs(direction) == 2)
        {
            direction = direction * 0.5f;
            directionVector = Vector3.right;
            _position = new Vector3(_position.x - 1, _position.y - 4, _position.z - 4);
        }


        while (timer < 0.75f)
        {
            timer += Time.fixedDeltaTime;
            Camera.main.transform.RotateAround(_position, directionVector, 120f * Time.deltaTime * camCurve.Evaluate(timer / .75f) * multiplicator * direction);
            Camera.main.orthographicSize -= camCurve.Evaluate(timer / .75f) * 10f * Time.fixedDeltaTime / 0.75f * multiplicator;
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position,_position, 17f * Time.fixedDeltaTime / 0.75f * multiplicator);
            yield return new WaitForFixedUpdate();
        }
        timer = 0;

        EventManager.instance.isZoomed = !EventManager.instance.isZoomed;
        cantRotate = false;
        EventManager.instance.cantDoZoom = false;
    }
}
