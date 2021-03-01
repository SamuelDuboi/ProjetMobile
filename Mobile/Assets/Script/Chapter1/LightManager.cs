using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(LineRenderer))]
public class LightManager : MonoBehaviour
{
    private float maxStepDistance = 200;
    public int maxReflectionCount = 5;
    private LineRenderer lineRenderer;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.InteractObject += LunchCastLight;
        EventManager.instance.SwipeLeft += LunchCastOnSwipeSide;
        EventManager.instance.SwipeUp += LunchCastOnSwipeUp;
        EventManager.instance.SwipeRight += LunchCastOnSwipeSide;
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

    private void LunchCastOnSwipeSide()
    {
        StartCoroutine(WaitToCastLight());

    }
    private void LunchCastOnSwipeUp(bool right)
    {
        StartCoroutine(WaitToCastLight());

    }

    private void LunchCastLight(GameObject currentObject = null)
    {
        StartCoroutine(WaitToCastLight());
    }
   private IEnumerator WaitToCastLight() 
    {
        timer = 0;
        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            lineRenderer.positionCount = 0;
            lineRenderer.positionCount = 1;
            CastLight(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);
            yield return new WaitForEndOfFrame();
        }
        
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
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount-2, startingPostion);
                lineRenderer.SetPosition(lineRenderer.positionCount-1, hit.point);
                EventManager.instance.OnLightOn(gameObject);
                return;
            }
            else
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 2, startingPostion);
                lineRenderer.SetPosition(lineRenderer.positionCount-1, hit.point);
                return;
            }

        }
        else
        {
            return;
        }
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 2, startingPostion);
        lineRenderer.SetPosition(lineRenderer.positionCount-1, startingPostion);
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
