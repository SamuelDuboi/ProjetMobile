using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Chest : MonoBehaviour
{

    public Transform childTransform;
    private int index;
    private float initialPos;
    private float positionMovement;


    private int inedxInArray;
    private ChestManager chestManager;
 


    public void Init(int _index, float _sensibility, int indexInArray, ChestManager chestManager)
    {
        this.chestManager = chestManager;
        inedxInArray = indexInArray;
        index = _index;
        var rect = childTransform as RectTransform;
        positionMovement = rect.sizeDelta.x / 3 * Screen.width/1080;
        initialPos = childTransform.position.x;
        childTransform.position = new Vector3(childTransform.position.x - index * positionMovement / 10, childTransform.position.y, childTransform.position.z);
    }


  


    public void MoveUp()
    {
        index++;
        if (index == 10)
            index = 0;
        childTransform.position = new Vector3(initialPos - index * positionMovement / 10, childTransform.position.y, childTransform.position.z);
        chestManager.TryValidate(index, inedxInArray);
    }
    public void MoveDown()
    {
        index--;
        if (index == -1)
            index = 9;
        childTransform.position = new Vector3(initialPos - index * positionMovement / 10, childTransform.position.y, childTransform.position.z);
        chestManager.TryValidate(index, inedxInArray);
    }
}
