using UnityEngine.UI;
using UnityEngine;

public class GreenStickTrial : MonoBehaviour
{
    public Sprite bigGear;
    public Sprite smallgGear;
    int numberOfGear=0;
    public void ClickOnBigGear(Button currentButtons)
    {
        int number;
        var gear = InventoryManager.Instance.FindObject("EngrenageGros", out number);
        if (gear)
        {
            InventoryManager.Instance.RemoveFromList("EngrenageGros", 1);
            currentButtons.GetComponent<Image>().sprite = bigGear;
            currentButtons.interactable = false;
            CheckNumberOfGear();
        }
    }
    public void ClickOnSmallGear(Button currentButtons)
    {
        int number;
        var gear = InventoryManager.Instance.FindObject("EngrenagePetit", out number);
        if (gear)
        {
            InventoryManager.Instance.RemoveFromList("EngrenagePetit", 1);
            currentButtons.GetComponent<Image>().sprite = smallgGear;
            currentButtons.interactable = false;
            CheckNumberOfGear();
        }
    }

    private void CheckNumberOfGear()
    {
        numberOfGear++;
        if(numberOfGear == 3)
        {
            GetComponentInParent<ObjectHandler>().trialInstantiate = null;
            GetComponentInParent<ObjectHandler>().interactifElement.spawnNewTrial = false;
            InventoryManager.Instance.AddList(gameObject, "LevierVert", default, 0);
            StartCoroutine(GetComponentInParent<ActivateStick>().ActivateWaterStick());
            Destroy(gameObject);
        }
    }
}
