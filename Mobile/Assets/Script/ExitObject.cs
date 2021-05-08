using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitObject : MonoBehaviour
{
    public GameObject text;
    public bool isTuto;
    private void Start()
    {
        EventManager.instance.InteractObject += Interact;
    }

    private void Interact(GameObject currentObject)
    {
        if(currentObject == gameObject || currentObject== transform.GetChild(0).gameObject)
        {
            if(SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().buildIndex == 9 || SceneManager.GetActiveScene().buildIndex ==0)
            {
                if (isTuto)
                    SaveManager.instance.SaveTuto();
                SaveManager.instance.LoadScene(1);
            }
            SaveManager.instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
