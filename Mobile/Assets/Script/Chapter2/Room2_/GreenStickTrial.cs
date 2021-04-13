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
        var gear = InventoryManager.Instance.FindObject("BigGear", out number);
        if (gear)
        {
            InventoryManager.Instance.RemoveFromList("BigGear", 1);
            currentButtons.GetComponent<Image>().sprite = bigGear;
            currentButtons.interactable = false;
        }
    }
    public void ClickOnSmallGear(Button currentButtons)
    {
        int number;
        var gear = InventoryManager.Instance.FindObject("SmallGear", out number);
        if (gear)
        {
            InventoryManager.Instance.RemoveFromList("SmallGear", 1);
            currentButtons.GetComponent<Image>().sprite = smallgGear;
            currentButtons.interactable = false;
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
