using System.Collections;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject main;
    public WallBehavior[] walls;
    public Animator animator;
    private bool cantTurn;
    [SerializeField] public bool upsideDownEditor;

    [SerializeField] public CamDirection currentDirection;

    private bool cantRotate;
    private void Start()
    {
        EventManager.instance.SwipeUp += LunchRotate;
        EventManager.instance.SwipeLeft += TurnLeft;
        EventManager.instance.SwipeRight += TurnRight;
        EventManager.instance.cuurrentCamDirection = currentDirection;

    }

    private void TurnLeft()
    {
        LunchTurn(false);
    }
    private void TurnRight()
    {
        LunchTurn(true);
    }
    public void LunchTurn(bool right)
    {

        float rotation = 90;
        if (!right)
            rotation = -90;

        if (upsideDownEditor)
        {

            if (!right)
                rotation = 90;
            else
                rotation = -90;

            right = !right;
        }
        StartCoroutine(Turn(right, rotation));
    }


    private IEnumerator Turn(bool right, float rotation)
    {

        if (!cantTurn)
        {
            cantTurn = true;
            Vector3 rotationToGo = new Vector3(main.transform.eulerAngles.x, main.transform.eulerAngles.y + rotation, main.transform.eulerAngles.z);

            #region WallTranslation

            if (!right)
            {
                if ((int)currentDirection > 1)
                {
                    if (currentDirection == CamDirection.SouthWest)
                    {
                        StartCoroutine(walls[(int)currentDirection].ChangePosition(true));
                        StartCoroutine(walls[(int)currentDirection - 2].ChangePosition(false));

                        currentDirection = CamDirection.SouthEast;
                    }
                    else
                    {
                        StartCoroutine(walls[(int)currentDirection].ChangePosition(true));
                        StartCoroutine(walls[2 - (int)currentDirection].ChangePosition(false));
                        currentDirection++;
                    }
                }
                else
                {
                    StartCoroutine(walls[(int)currentDirection].ChangePosition(true));
                    StartCoroutine(walls[(int)currentDirection + 2].ChangePosition(false));
                    if (currentDirection == CamDirection.SouthWest)
                    {
                        currentDirection = CamDirection.SouthEast;
                    }
                    else
                        currentDirection++;

                }

            }
            else
            {
                if ((int)currentDirection < 3)
                {

                    StartCoroutine(walls[(int)currentDirection + 1].ChangePosition(true));
                    if (currentDirection == CamDirection.SouthEast)
                    {
                        StartCoroutine(walls[3 - (int)currentDirection].ChangePosition(false));
                    }
                    else
                        StartCoroutine(walls[(int)currentDirection - 1].ChangePosition(false));


                    if (currentDirection == CamDirection.SouthEast)
                    {
                        currentDirection = CamDirection.SouthWest;
                    }
                    else
                        currentDirection--;

                }
                else
                {
                    StartCoroutine(walls[3 - (int)currentDirection].ChangePosition(true));
                    StartCoroutine(walls[(int)currentDirection - 1].ChangePosition(false));
                    currentDirection--;

                }

            }
            #endregion
            EventManager.instance.cuurrentCamDirection = currentDirection;

            for (int i = 0; i < 50; i++)
            {
                main.transform.Rotate(new Vector3(0, rotation / 50, 0), Space.World);
                //   light.transform.Rotate(new Vector3(0, rotation / 100, 0));


                yield return new WaitForSeconds(0.01f);
            }
            cantTurn = false;
            main.transform.rotation = Quaternion.Euler(rotationToGo);
        }

        EventManager.instance.cantDoZoom = false;
    }

    public void LunchRotate(bool up)
    {
        if (!cantRotate)
        {
            StartCoroutine(Rotate(up));
        }
        else
            EventManager.instance.cantDoZoom = false;
    }

    private IEnumerator Rotate(bool up)
    {
        cantRotate = true;
        if (up)
        {
            animator.SetTrigger("Up");
        }
        else
        {
            animator.SetTrigger("Down");
        }
        yield return new WaitForSeconds(0.2f);
        var timer = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(timer - 0.2f);
        cantRotate = false;
        EventManager.instance.cantDoZoom = false;
    }

    public void TurnEditor(float angle)
    {

        Vector3 rotationToGo = new Vector3(main.transform.eulerAngles.x, main.transform.eulerAngles.y - angle, main.transform.eulerAngles.z);

        if (angle == 90)
            TurRightEditor();
        else
            TurnLeftEditor();

        main.transform.Rotate(new Vector3(0, -angle, 0), Space.World);
        main.transform.rotation = Quaternion.Euler(rotationToGo);


    }

    private void TurRightEditor()
    {
        if ((int)currentDirection > 1)
        {
            if (currentDirection == CamDirection.SouthWest)
            {
                walls[(int)currentDirection - 2].ChangePositionEditor(false);
                walls[(int)currentDirection].ChangePositionEditor(true);

                currentDirection = CamDirection.SouthEast;
            }
            else
            {
                walls[(int)currentDirection].ChangePositionEditor(true);

                walls[2 - (int)currentDirection].ChangePositionEditor(false);
                currentDirection++;
            }
        }
        else
        {
            walls[(int)currentDirection].ChangePositionEditor(true);
            walls[(int)currentDirection + 2].ChangePositionEditor(false);
            if (currentDirection == CamDirection.SouthWest)
            {
                currentDirection = CamDirection.SouthEast;
            }
            else
                currentDirection++;

        }
    }
    private void TurnLeftEditor()
    {

        if ((int)currentDirection < 3)
        {

            walls[(int)currentDirection + 1].ChangePositionEditor(true);
            if (currentDirection == CamDirection.SouthEast)
            {
                walls[3 - (int)currentDirection].ChangePositionEditor(false);
            }
            else
                walls[(int)currentDirection - 1].ChangePositionEditor(false);


            if (currentDirection == CamDirection.SouthEast)
            {
                currentDirection = CamDirection.SouthWest;
            }
            else
                currentDirection--;

        }
        else
        {
            walls[(int)currentDirection - 1].ChangePositionEditor(false);
            walls[3 - (int)currentDirection].ChangePositionEditor(true);
            currentDirection--;

        }


    }
}
