using UnityEngine.UI;
using UnityEngine;

public class GreenStickTrial : MonoBehaviour
{
    public Sprite bigGear;
    public Sprite smallgGear;
    public Sprite MidlegGear;
    public Sprite UiGrand;
    public Sprite UIpetit;
    int numberOfGear=0;
    int numberOfsmallGear=0;
    public ObjectHandler parent;


    private void Start()
    {
        parent = GetComponentInParent<ObjectHandler>();
        parent.trialInstantiate = null;
        EventManager.instance.ZoomOut += Unzoom;
    }
    public void ClickOnBigGear(Button currentButtons)
    {
        int number;
        var gear = InventoryManager.Instance.FindObject("EngrenageGros", out number);
        if (gear)
        {
            InventoryManager.Instance.RemoveFromList("EngrenageGros", 1);
            currentButtons.GetComponent<Image>().sprite = bigGear;
            currentButtons.GetComponent<Image>().type = Image.Type.Simple;
            currentButtons.interactable = false;
            CheckNumberOfGear(false);
        }
    }
    public void ClickMidleGear(Button currentButtons)
    {
        int number;
        var gear = InventoryManager.Instance.FindObject("EngrenageGros", out number);
        if (gear)
        {
            InventoryManager.Instance.RemoveFromList("EngrenageGros", 1);
            currentButtons.GetComponent<Image>().sprite = MidlegGear;
            currentButtons.GetComponent<Image>().type = Image.Type.Simple;
            currentButtons.interactable = false;
            CheckNumberOfGear(false);
        }
    }
    public void ClickOnSmallGear(Button currentButtons)
    {
        int number;
        var gear = InventoryManager.Instance.FindObject("EngrenagePetit", out number);
        if (gear)
        {
            InventoryManager.Instance.RemoveFromList("EngrenagePetit", 1);
            currentButtons.GetComponent<Image>().type = Image.Type.Simple;
            currentButtons.GetComponent<Image>().sprite = smallgGear;
            currentButtons.interactable = false;
            CheckNumberOfGear(true);
        }
    }

    private void CheckNumberOfGear(bool small)
    {
        if (!small)
            numberOfGear++;
        else
            numberOfsmallGear++;
        if(numberOfGear == 2 && numberOfsmallGear == 1)
        {
            GetComponentInParent<ObjectHandler>().trialInstantiate = null;
            EventManager.instance.OnDestroyTrial();

            GetComponentInParent<ObjectHandler>().interactifElement.spawnNewTrial = false;
            InventoryManager.Instance.AddList(gameObject, "LevierVert", default, 0);
            GetComponentInParent<ActivateStick>().ActivateStickLunch();
            EventManager.instance.ZoomOut -= Unzoom;
            Destroy(gameObject);
        }
    }
    private void Unzoom()
    {
        parent.interactifElement.onlyZoom = false;
        EventManager.instance.OnDestroyTrial();
        parent.HitBoxZoom.enabled = true;
        parent.interactifElement.spawnNewTrial = true;
        if (numberOfsmallGear == 1)
            InventoryManager.Instance.AddList(parent.gameObject, "EngrenagePetit", UIpetit);
        if(numberOfGear>0)
            InventoryManager.Instance.AddList(parent.gameObject, "EngrenageGros", UiGrand,numberOfGear);
        EventManager.instance.ZoomOut -= Unzoom;
        Destroy(gameObject);
    }
}
