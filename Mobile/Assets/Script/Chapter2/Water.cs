using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    public Animator waterAnim;
    public bool blockUp;
    public bool blockDown;
    public ObjectHandler interactifElement;
    bool doOnce;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.SwipeUp += UpsideDown;
        waterAnim.SetBool("BlockDown", blockDown);
        waterAnim.SetBool("BlockUp", blockUp);
    }

    public virtual void UpsideDown(bool up)
    {
        waterAnim.SetTrigger("UpsideDown");
        if(interactifElement != null && !doOnce)
        {
            doOnce = true;
            InventoryManager.Instance.AddList(gameObject, interactifElement.name,default, 1);
            interactifElement.Interact(interactifElement.HitBoxZoom.gameObject);
        }
    }

    public virtual void BlockUp()
    {
        blockUp = !blockUp;
        waterAnim.SetBool("BlockUp", blockUp); 
    }
    public virtual void BlockDown()
    {
        blockDown = !blockDown;
        waterAnim.SetBool("BlockDown", blockDown);
    }
}