using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Speech.Synthesis;
using SpeechLib;

public class AccessibilityManager : MonoBehaviour
{
    public static AccessibilityManager ManagerInstance = null;
    //SpeechSynthesizer Voice = new SpeechSynthesizer();
    SpVoice Voice = new SpVoice();

    public KeyCode[] Keys;
    public ButtonRemapping[] Buttons;
    public DropdownRemapping[] Dropdowns;
    public Button_TTS[] Button_TTS;

    private void Awake()
    {
        if(ManagerInstance == null)
        {
            ManagerInstance = this;
        }
        
        if(ManagerInstance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        Buttons = FindObjectsOfType<ButtonRemapping>();
        Dropdowns = FindObjectsOfType<DropdownRemapping>();
        Button_TTS = FindObjectsOfType<Button_TTS>();
    }

    public void Speak(string text)
    {
        Voice.Speak(text);
    }
}
