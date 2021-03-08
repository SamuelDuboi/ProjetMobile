using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupBehavior : MonoBehaviour
{
    public TextMeshProUGUI text;
    private void Start()
    {
        EventManager.instance.Popup += Popup;
    }
    public void Popup(string _text, float time)
    {
        text.enabled= true;

        text.text = _text;
        StartCoroutine(PopupFade(time));
    }
    
    private IEnumerator PopupFade(float time)
    {
        yield return new WaitForSeconds(time);
        text.text = string.Empty;
        text.enabled = false;
    }
}
