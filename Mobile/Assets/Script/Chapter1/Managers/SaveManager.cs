
using UnityEngine;
using System.Collections;
using System.Xml;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
public class SaveManager : MonoBehaviour
{

    public bool hasDoneTuto;
    public int chapter1Number =1;
    public int chapter2Number = 5;
    public static SaveManager instance;
    public string path;
    public bool debug;
    public GameObject[] chap1Button;
    public GameObject[] chap2Button;
    public Image FadePanel;
    public GameObject loading;
    private void Awake()
    {
        if(instance== null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("Instance Already existe");
        }
    }

    private void SaveByXML()
    {
        Save save = CreatSave();
        XmlDocument xmlDocument = new XmlDocument();


        XmlElement rooot = xmlDocument.CreateElement("Save");
        rooot.SetAttribute("FileName", "File_01");

        //save index of chapter 1
        XmlElement chapter1SceneIndex = xmlDocument.CreateElement("Chapter1Index");
        chapter1SceneIndex.InnerText = save.Chapter1SceneIndex.ToString();
        rooot.AppendChild(chapter1SceneIndex);

        //save index of chapter 2

        XmlElement chapter2SceneIndex = xmlDocument.CreateElement("Chapter2Index");
        chapter2SceneIndex.InnerText = save.Chapter2SceneIndex.ToString();
        rooot.AppendChild(chapter2SceneIndex);

        //saveTuto
        XmlElement hasDoneTuto = xmlDocument.CreateElement("hasDoneTuto");
        hasDoneTuto.InnerText = save.hasDoneTuto.ToString();
        rooot.AppendChild(hasDoneTuto);


        xmlDocument.AppendChild(rooot);
        xmlDocument.Save(Application.persistentDataPath + "/DataXML.text");
        if (File.Exists(Application.persistentDataPath + "/DataXML.text"))
        {
            Debug.Log("file save");
        }

    }
    private void LoadByXML()
    {
        if (File.Exists(Application.persistentDataPath + "/DataXML.text"))
        {
            path = Application.persistentDataPath;
            Save save = new Save();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Application.persistentDataPath + "/DataXML.text");

            // set chapter1 index
            XmlNodeList chapterIndex = xmlDocument.GetElementsByTagName("Chapter1Index");
            chapter1Number = int.Parse(chapterIndex[0].InnerText);
            save.Chapter1SceneIndex = chapter1Number;

            //set chapter2 index
            XmlNodeList chapter2Index = xmlDocument.GetElementsByTagName("Chapter2Index");
            chapter2Number = int.Parse(chapter2Index[0].InnerText);
            save.Chapter1SceneIndex = chapter2Number;

            //set chapter 3 index
            XmlNodeList hasDoneTutoXML = xmlDocument.GetElementsByTagName("hasDoneTuto");
            hasDoneTuto =bool.Parse( hasDoneTutoXML[0].InnerText);
            save.Chapter1SceneIndex = chapter2Number;
        }
        else
        {
            Debug.LogError("there is no save in this instance");
        }
    }

    private Save CreatSave()
    {
        Save save = new Save();
        save.hasDoneTuto = hasDoneTuto;
        save.Chapter1SceneIndex = chapter1Number;
        save.Chapter2SceneIndex = chapter2Number;
        return save;
    }

    public void SaveChapter1()
    {
       if(SceneManager.GetActiveScene().buildIndex != 4)
            chapter1Number = SceneManager.GetActiveScene().buildIndex +1;
        SaveByXML();
    }

    public void SaveChapter2()
    {
        if (SceneManager.GetActiveScene().buildIndex != 8)
            chapter2Number = SceneManager.GetActiveScene().buildIndex+1 ;

        SaveByXML();
    }

    public void SaveTuto(bool save = true)
    {
        hasDoneTuto = save;
        SaveByXML();
    }

    public void LoadChapter1( GameObject[] listChap1)
    {
        LoadByXML();
        if (debug)
        {
            for (int i = 0; i < listChap1.Length; i++)
            {
                listChap1[i].SetActive(true);
                
            }
            return;
        }
        listChap1[0].SetActive(true);
        for (int i = 1; i < listChap1.Length; i++)
        {
            if(i < chapter1Number - 1)
            {
                listChap1[i].SetActive(true); ;
            }
        }
    }
    public void LoadChapter2(GameObject[] listChap2)
    {
        LoadByXML();
        if (debug)
        {
            for (int i = 0; i < listChap2.Length; i++)
            {
                listChap2[i].SetActive(true);
            }
            return;
        }
        listChap2[0].SetActive(true);
        for (int i = 1; i < listChap2.Length; i++)
        {
            if (i < chapter2Number -5)
            {
                listChap2[i].SetActive(true);
            }
        }
    }

    public void LoadScene(int index)
    {
        FadePanel.gameObject.SetActive(true);
        StartCoroutine(LoadSceneCoroutine(index));
    }
    public void LoadScene(string name)
    {
        FadePanel.gameObject.SetActive(true);
        StartCoroutine(LoadSceneCoroutine(name));
    }
    IEnumerator LoadSceneCoroutine(int index)
    {
        float timer = 0;
        while (timer < 0.5f)
        {
            FadePanel.color += new Color(0, 0, 0, 0.02f);
            timer += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        loading.SetActive(true);
        SoundManager.instance.ClearSound();

        var scen = SceneManager.LoadSceneAsync(index);
        scen.completed += LoadScenCompleted;

    }
    IEnumerator LoadSceneCoroutine(string name)
    {
        float timer = 0;
        FadePanel.color = new Color(0,0,0,0);

        while (timer < 0.5f)
        {
            FadePanel.color += new Color(0, 0, 0, 0.02f);
            timer += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        loading.SetActive(true);
        SoundManager.instance.ClearSound();
        var scen = SceneManager.LoadSceneAsync(name);
        scen.completed += LoadScenCompleted;

    }
    public void LoadTuto(GameObject pannel)
    {
        LoadByXML();
        if (hasDoneTuto)
            LoadScene("Menu3D");
        else
        {
            pannel.SetActive(false);
            AsyncOperation defaut = default;
            FadePanel.gameObject.SetActive(true);
            LoadScenCompleted(defaut);
        }
            
    }

    public void Quit()
    {
        Application.Quit();
    }

    void LoadScenCompleted( AsyncOperation operation)
    {
        if(operation!= null)
        operation.completed -= LoadScenCompleted;
        StartCoroutine(LoadScenCompledtedCooutine());
    }
    IEnumerator LoadScenCompledtedCooutine()
    {
        yield return new WaitForSeconds(1);
        loading.SetActive(false);
        float timer = 0.5f;
       // FadePanel.color = Color.black;
        while (timer > 0)
        {
            FadePanel.color -= new Color(0, 0, 0, 0.02f);
            timer -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        FadePanel.gameObject.SetActive(false);
        
    }
}


