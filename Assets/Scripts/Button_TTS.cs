using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Button_TTS : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void OnMouseOver()
    {
        if (this.enabled == true)
        {
            AccessibilityManager.ManagerInstance.Speak(transform.GetChild(0).GetComponent<Text>().text);
        }
    }
}
