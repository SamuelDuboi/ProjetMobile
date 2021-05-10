using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class FingerTipsManager : MonoBehaviour
{
    public static FingerTipsManager instance;
    public GameObject[] upDownFinger;
    public GameObject[] leftRightFinger;
    public Image doorFinger;
    public Image returnFinger;
    public Image chestFinger;
    public Image item1Finger;
    public Image paintingFinger;
    public ObjectHandler chest;

    public bool canSwipDown;
    public int paintingNumber;
    public TextMeshProUGUI textMeshPro;

    public float speed;
    public float mindistance =10;
    public TutoDeviceManager tutoDeviceManager;
    public bool zoomBack;
    public bool canStart;
    private void Awake()
    {
        instance = this;
    }
    private IEnumerator Start()
    {
        EventManager.instance.InteractObject += OpenChest;
        EventManager.instance.ZoomOut += ZoomOutDoor;
        EventManager.instance.ZoomIn += MoveCam;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => canStart);
        textMeshPro.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        textMeshPro.gameObject.SetActive(false);
        tutoDeviceManager.phase++;
        MoveLeftRight("Balayez l’écran de gauche à droite pour faire pivoter la salle.");
    }


    private void MoveLeftRight(string text)
    {
        tutoDeviceManager.stopAnim = false;
        StartCoroutine(MoveLeftRightCorutine(text));
    }

    IEnumerator MoveLeftRightCorutine (string text)
    {
        var initialPos = leftRightFinger[0].transform.position;
        leftRightFinger[0].SetActive(true);
        float timer =0;
        bool doOnce = false;
        while (!tutoDeviceManager.stopAnim)
        {
            timer += 0.01f;
            leftRightFinger[0].transform.Translate(Vector3.right * speed, Space.Self);
            yield return new WaitForSeconds(0.01f);
            if(Mathf.Abs(leftRightFinger[0].transform.position.x - leftRightFinger[1].transform.position.x) < mindistance)
            {
                leftRightFinger[0].transform.position = initialPos;
            }
            if(timer>5f && !doOnce)
            {
                textMeshPro.gameObject.SetActive(true);
                textMeshPro.text = text;
                doOnce = true;
            }
        }
        leftRightFinger[0].transform.position = initialPos;
        leftRightFinger[0].SetActive(false);
        if (doOnce)
            textMeshPro.gameObject.SetActive(false);

        yield return new  WaitUntil(() => !EventManager.instance.cantDoZoom);
        if(tutoDeviceManager.phase == 2)
        {
           StartCoroutine( DoorPopup("Il faut sortir par cette porte."));
        }
    }

    private IEnumerator DoorPopup(string text)
    {
        tutoDeviceManager.stopAnim = false;

        textMeshPro.gameObject.SetActive(true);
        textMeshPro.text = text;
        doorFinger.gameObject.SetActive(true);
        float timer = 0;
        bool doOnce = false;
        while (!tutoDeviceManager.stopAnim)
        {
            yield return new WaitForSeconds(0.3f);
            timer += 0.3f;
            yield return new WaitForSeconds(0.3f);
            timer += 0.3f;
         
            if (timer > 5f && !doOnce)
            {
                textMeshPro.gameObject.SetActive(true);
                textMeshPro.text += "\n\n\n" +
                    "Touchez la porte pour zoomer.";
                doOnce = true;
            }
        }

        doorFinger.gameObject.SetActive(false);
        textMeshPro.gameObject.SetActive(false);
        yield return new WaitUntil(() => !EventManager.instance.cantDoZoom);
        StartCoroutine(ReturnPopup());
    }

    private IEnumerator ReturnPopup( )
    {

        tutoDeviceManager.stopAnim = false;

        textMeshPro.gameObject.SetActive(true);
        textMeshPro.text = "La porte ne possède pas de poignée, il va falloir la trouver.";
        returnFinger.gameObject.SetActive(true);

        while (!tutoDeviceManager.stopAnim)
        {
            yield return new WaitForSeconds(0.3f);
        }
        returnFinger.gameObject.SetActive(false);
        textMeshPro.gameObject.SetActive(false);
        yield return new WaitUntil(() => !EventManager.instance.cantDoZoom);
        textMeshPro.gameObject.SetActive(true);
        tutoDeviceManager.stopAnim = false;
        textMeshPro.text = "Quelque chose se trouve sûrement dans la valise posée sur le lit.";

    }

    public void ZoomOutDoor()
    {
        tutoDeviceManager.stopAnim = true;
        tutoDeviceManager.phase++;
        if (tutoDeviceManager.phase == 6)
            StartCoroutine(SwipeUp());
        else if (tutoDeviceManager.phase == 8)
        {
            canSwipDown = true;
        }
        else if (tutoDeviceManager.phase == 12)
        {
            tutoDeviceManager.phase++;
            textMeshPro.gameObject.SetActive(true);
            textMeshPro.text = "Il faut maintenant retrouver la deuxième partie de la poignée.";
        }
        else if (tutoDeviceManager.phase == 14)
        {

            tutoDeviceManager.phase++;
            textMeshPro.gameObject.SetActive(true);
            textMeshPro.text = "Vous pouvez maintenant ouvrir la porte pour sortir d’ici.";
        }
        EventManager.instance.ZoomOut -= ZoomOutDoor;
    }
    private void MoveCam(Cams cams, float orthogrphicSize, GameObject currentObject)
    {
        if (cams != null && tutoDeviceManager.phase == 4)
        {
            textMeshPro.gameObject.SetActive(false);
            StartCoroutine(ChestPopup());
        }

    }

    private IEnumerator ChestPopup()
    {
        yield return new WaitUntil(() => !EventManager.instance.cantDoZoom);

        tutoDeviceManager.phase ++;
        chestFinger.gameObject.SetActive(true);

        while (!tutoDeviceManager.stopAnim)
        {
            yield return new WaitForSeconds(0.3f);
        }
        chestFinger.gameObject.SetActive(false);
    }

    private void OpenChest(GameObject currentObject)
    {
        if (currentObject == chest.HitBoxZoom.gameObject && tutoDeviceManager.phase == 5)
        {
            EventManager.instance.ZoomOut += ZoomOutDoor;
            tutoDeviceManager.stopAnim = true;           
            zoomBack = false;
            EventManager.instance.InteractObject -= OpenChest;
        }
    }

    private IEnumerator SwipeUp()
    {

        tutoDeviceManager.stopAnim = false;
        var initialPos = upDownFinger[0].transform.position;
        upDownFinger[0].SetActive(true);
        float timer = 0;
        textMeshPro.gameObject.SetActive(true);
        textMeshPro.text = "Le code doit se trouver dans la pièce."; 
        bool doOnce = false;
        while (!tutoDeviceManager.stopAnim)
        {
            timer += 0.01f;
            upDownFinger[0].transform.Translate(Vector3.up * speed, Space.Self);
            yield return new WaitForSeconds(0.01f);
            if (Mathf.Abs(upDownFinger[0].transform.position.y - upDownFinger[1].transform.position.y) < mindistance)
            {
                upDownFinger[0].transform.position = initialPos;
            }
            if (timer > 5f && !doOnce)
            {
                textMeshPro.gameObject.SetActive(true);
                textMeshPro.text = "Balayez l'écran de haut en bas pour retourner la salle.";
                doOnce = true;
            }
        }
        upDownFinger[0].transform.position = initialPos;
        upDownFinger[0].SetActive(false);
        if (doOnce)
            textMeshPro.gameObject.SetActive(false);

        yield return new WaitUntil(() => !EventManager.instance.cantDoZoom);
        if (tutoDeviceManager.phase == 7)
        {
            StartCoroutine(SwipDown());
        }
    }

    public void numberUp()
    {
        paintingNumber++;
        if (paintingNumber==2)
        {
            EventManager.instance.ZoomOut += ZoomOutDoor;
        }
    }

    IEnumerator SwipDown()
    {
        textMeshPro.gameObject.SetActive(true);
        textMeshPro.text = "Des chiffres sont écrits sur les murs mais il semble en manquer deux.";
        StartCoroutine(TextDesapear());
        yield return new WaitUntil(() => canSwipDown);
        tutoDeviceManager.stopAnim = false;
        var initialPos = upDownFinger[1].transform.position;
        upDownFinger[1].SetActive(true);
        float timer = 0;
        textMeshPro.gameObject.SetActive(false);
        bool doOnce = false;
        while (!tutoDeviceManager.stopAnim)
        {
            timer += 0.01f;
            upDownFinger[1].transform.Translate(Vector3.down * speed, Space.Self);
            yield return new WaitForSeconds(0.01f);
            if (Mathf.Abs(upDownFinger[1].transform.position.y - upDownFinger[0].transform.position.y) < mindistance)
            {
                upDownFinger[1].transform.position = initialPos;
            }
            if (timer > 5f && !doOnce)
            {
                textMeshPro.gameObject.SetActive(true);
                textMeshPro.text = "Balayez l'écran de haut en bas pour retourner la salle.";
                doOnce = true;
            }
        }
        upDownFinger[1].transform.position = initialPos;
        upDownFinger[1].SetActive(false);
        if (doOnce)
            textMeshPro.gameObject.SetActive(false);
    }
    IEnumerator TextDesapear()
    {
        yield return new WaitForSeconds(5.0f);
        textMeshPro.text = string.Empty;
    }
    public void startCollect()
    {
        StartCoroutine(CollectItem());
    }
    IEnumerator CollectItem()
    {
        zoomBack = true;
        tutoDeviceManager.stopAnim = false;
        tutoDeviceManager.phase++;
        textMeshPro.gameObject.SetActive(true);
        textMeshPro.text = "Touchez l’objet pour le récupérer.";
        item1Finger.gameObject.SetActive(true);

        while (!tutoDeviceManager.stopAnim)
        {
            yield return new WaitForSeconds(0.3f);
        }
        item1Finger.gameObject.SetActive(false);
        textMeshPro.gameObject.SetActive(false);
        yield return new WaitUntil(() => !zoomBack);
        textMeshPro.gameObject.SetActive(true);
        tutoDeviceManager.stopAnim = false;
        EventManager.instance.ZoomOut += ZoomOutDoor;
        textMeshPro.text = "L’objet se trouve maintenant dans l’inventaire.";
    }
}

