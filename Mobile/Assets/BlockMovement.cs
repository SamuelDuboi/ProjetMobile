
using UnityEngine;

public class BlockMovement : StateMachineBehaviour
{

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!EventManager.instance.cantDoZoom)
        {
            Debug.Log(animator.transform.parent.name);
            EventManager.instance.cantDoZoom = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (EventManager.instance.cantDoZoom)
            EventManager.instance.cantDoZoom = false;
    }


}
