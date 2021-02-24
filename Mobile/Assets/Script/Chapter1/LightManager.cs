using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LightManager : MonoBehaviour
{
    public GameObject light;
    private float maxStepDistance = 200;
    public int maxReflectionCount = 5;
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.InteractObject += LunchCastLight;
        lineRenderer = GetComponent<LineRenderer>();
        LunchCastLight(gameObject);
    }
    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.ArrowHandleCap(0, this.transform.position + this.transform.forward * 0.25f, this.transform.rotation, 0.5f, EventType.Repaint);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.25f);
        DrawPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);
    }

    private void LunchCastLight(GameObject currentObject)
    {
        StartCoroutine(WaitToCastLight());
    }
   private IEnumerator WaitToCastLight() 
    {
        yield return new WaitForSeconds(0.5f);
        CastLight(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);
    }

    private void CastLight(Vector3 position, Vector3 direction, int reflexionRemaining)
    {
        if (reflexionRemaining == 0)
        {
            return;
        }

        Vector3 startingPostion = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxStepDistance))
        {
            if (hit.collider.gameObject.tag == "Miror")
            {
                direction = Vector3.Reflect(direction, hit.normal);
                position = hit.point;
            }
            else if (hit.collider.gameObject.tag == "Reception")
            {
                var currentlight = Instantiate(light, position, transform.rotation);
                currentlight.GetComponent<LineRenderer>().SetPosition(0, startingPostion);
                currentlight.GetComponent<LineRenderer>().SetPosition(1, hit.point);
                EventManager.instance.OnLightOn(gameObject);
                return;
            }
            else
            {
                var currentlight = Instantiate(light, position, transform.rotation);
                currentlight.GetComponent<LineRenderer>().SetPosition(0, startingPostion);
                currentlight.GetComponent<LineRenderer>().SetPosition(1, hit.point);
                return;
            }

        }
        else
        {
            return;
        }
        var _light = Instantiate(light, position, transform.rotation);
        _light.GetComponent<LineRenderer>().SetPosition(0, startingPostion);
        _light.GetComponent<LineRenderer>().SetPosition(1, position);
        CastLight(position, direction, reflexionRemaining - 1);
    }
    


    private void DrawPattern( Vector3 position, Vector3 direction, int reflexionRemaining)
    {
        if(reflexionRemaining == 0)
        {
            return;
        }

        Vector3 startingPostion = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, maxStepDistance))
        {
            if (hit.collider.gameObject.tag == "Miror")
            {
                direction = Vector3.Reflect(direction, hit.normal);
                position = hit.point;
            }
            else if( hit.collider.gameObject.tag == "Reception")
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(startingPostion, hit.point);
                Gizmos.color = Color.green;
                Gizmos.DrawCube(hit.point, Vector3.one*0.2f);
                return;
            }
            else
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(startingPostion, hit.point);
                return;

            }
                         
        }
        else
        {
            position += direction * maxStepDistance;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startingPostion, position );
        DrawPattern(position, direction, reflexionRemaining - 1);
    }
}
