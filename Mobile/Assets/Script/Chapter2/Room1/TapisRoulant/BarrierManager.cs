using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierManager : MonoBehaviour
{
    public Animator[] bariersAnimators;

    private void PressLeft()
    {
        bariersAnimators[0].SetBool("Up", true);
        bariersAnimators[1].SetBool("Up", false);
        bariersAnimators[2].SetBool("Up", false);
    }
    private void PressMiddle()
    {
        bariersAnimators[0].SetBool("Up", false);
        bariersAnimators[1].SetBool("Up", true);
        bariersAnimators[2].SetBool("Up", false);
    }
    private void PressRight()
    {
        bariersAnimators[0].SetBool("Up", false);
        bariersAnimators[1].SetBool("Up", false);
        bariersAnimators[2].SetBool("Up", true);
    }

    public void PressButon(int index)
    {
        switch (index)
        {
            case 1:
                PressLeft();
                break;
            case 2:
                PressMiddle();
                break;
            case 3:
                PressRight();
                break;

            default:
                break;
        }
    }
}
