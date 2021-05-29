using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWaterManager : MonoBehaviour
{
    public static TheWaterManager instance;


    public Animator waterAnim;
    public int currentLevel = 0;
    public GameObject water;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            Debug.LogError("TheWaterManager has been destroy");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.SwipeUp += UpsideDown;
    }

    public virtual void UpsideDown(bool up)
    {
        waterAnim.SetTrigger("UpsideDown");
    }

    public void WaterUp()
    {
        currentLevel++;
        waterAnim.SetInteger("Level", currentLevel);
        
    }
    public void LoadMenu()
    {
        StartCoroutine(WaitLoadMenu());
    }
    IEnumerator WaitLoadMenu()
    {
        yield return new WaitForSeconds(2f);

        SaveManager.instance.SaveChapter2();
        SaveManager.instance.LoadScene(1);

    }
}
