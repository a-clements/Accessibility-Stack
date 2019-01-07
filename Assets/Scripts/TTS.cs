using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TTS : MonoBehaviour, IPointerEnterHandler
{
    private void Start()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.enabled == true)
        {
            AccessibilityManager.Speak(this.transform.GetComponentInChildren<Text>().text);
        }
    }
}
