using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public TTS[] TTS;
    public Toggle TextToSpeech;
    public Slider SpeechVolume;

    private int volume = 0;

    private void Awake()
    {
        TTS = FindObjectsOfType<TTS>();
        TextToSpeech = GameObject.Find("Scroll View/Viewport/Content/TextToSpeech").GetComponent<Toggle>();
        SpeechVolume = GameObject.Find("Scroll View/Viewport/Content/SpeechVolume").GetComponent<Slider>();
    }

    private void OnEnable()
    {

        if (TextToSpeech != null)
        {
            TextToSpeech.onValueChanged.AddListener(delegate { OnTextToSpeechToggle(); });
        }

        if(SpeechVolume != null)
        {
            SpeechVolume.onValueChanged.AddListener(delegate { OnSpeechVolumeChange(); });
        }
    }

    void Start()
    {
        UIManager.ManagerInstance.SpeechVolume = SpeechVolume.value * 10;
    }

    public void OnSpeechVolumeChange()
    {
        UIManager.ManagerInstance.SpeechVolume = SpeechVolume.value * 10;

        if(SpeechVolume.value == 0)
        {
            TextToSpeech.isOn = false;
            OnTextToSpeechToggle();
        }
        else
        {
            TextToSpeech.isOn = true;
            OnTextToSpeechToggle();
        }
    }

    public void OnTextToSpeechToggle()
    {
        if (TextToSpeech.isOn == false)
        {
            foreach (TTS tts in TTS)
            {
                if(tts.enabled == true)
                {
                    tts.enabled = false;
                }
            }
        }
        else
        {
            foreach (TTS tts in TTS)
            {
                if (tts.enabled == false)
                {
                    tts.enabled = true;
                }
            }
        }
    }


}
