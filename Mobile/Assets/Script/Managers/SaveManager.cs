
using UnityEngine;

using System.Xml;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
public class SaveManager : MonoBehaviour
{

    public bool hasDoneTuto;
    public int chapter1Number;
   public int chapter2Number;
    public static SaveManager instance;
    public string path;

    public Button[] chap1Button;
    public Button[] chap2Button;
    private void Awake()
    {
        if(instance== null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(SaveManager.instance);
            instance = this;
            DontDestroyOnLoad(instance);

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
        xmlDocument.Save(Application.dataPath + "/DataXML.text");
        if (File.Exists(Application.dataPath + "/DataXML.text"))
        {
            Debug.Log("file save");
        }

    }
    private void LoadByXML()
    {
        if (File.Exists(Application.dataPath + "/DataXML.text"))
        {
            path = Application.dataPath;
            Save save = new Save();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Application.dataPath + "/DataXML.text");

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
       
            chapter2Number = SceneManager.GetActiveScene().buildIndex+1 ;

        SaveByXML();
    }

    public void SaveTuto()
    {
        hasDoneTuto = true;
        SaveByXML();
    }

    public void LoadChapter1()
    {
        LoadByXML();
        for (int i = 1; i < chap1Button.Length; i++)
        {
            if(i != chapter1Number - 1)
            {
                chap1Button[i].interactable = false;
            }
        }
    }
    public void LoadChapter2()
    {
        LoadByXML();
        for (int i = 5; i < chap2Button.Length; i++)
        {
            if (i != chapter1Number - 5)
            {
                chap2Button[i].interactable = false;
            }
        }
    }
    public void LoadTuto()
    {
        LoadByXML();
        if (hasDoneTuto)
            SceneManager.LoadScene("Menu");
        else
            SceneManager.LoadScene("Tuto");
    }
}


