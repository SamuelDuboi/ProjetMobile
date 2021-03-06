using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaikeTrial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.InteractObject += Interact;
    }

    private void Interact(GameObject game)
    {
        EventManager.instance.InteractObject -= Interact;
        Destroy(gameObject);
    }
}
