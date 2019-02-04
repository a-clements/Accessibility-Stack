using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public TTS[] TTS;
    public Toggle TextToSpeech;
    public Slider SpeechVolume;
    public Slider MasterVolume;
    public Slider MusicVolume;
    public Slider SFXVolume;
    public Slider AmbientVolume;
    public AudioSource[] Sources;

    private void Awake()
    {
        TTS = FindObjectsOfType<TTS>();

        if(GameObject.Find("Scroll View/Viewport/Content/TextToSpeech") != null)
        {
            TextToSpeech = GameObject.Find("Scroll View/Viewport/Content/TextToSpeech").GetComponent<Toggle>();
            TextToSpeech.onValueChanged.AddListener(delegate { OnTextToSpeechToggle(); });
        }

        if(GameObject.Find("Scroll View/Viewport/Content/SpeechVolume") != null)
        {
            SpeechVolume = GameObject.Find("Scroll View/Viewport/Content/SpeechVolume").GetComponent<Slider>();
            SpeechVolume.onValueChanged.AddListener(delegate { OnSpeechVolumeChange(); });
            SpeechVolume.value = 5;
        }

        if (GameObject.Find("Scroll View/Viewport/Content/MasterVolume") != null)
        {
            MasterVolume = GameObject.Find("Scroll View/Viewport/Content/MasterVolume").GetComponent<Slider>();
            MasterVolume.onValueChanged.AddListener(delegate { OnMasterVolumeChange(); });
            MasterVolume.value = 0.5f;
        }

        if (GameObject.Find("Scroll View/Viewport/Content/MusicVolume") != null)
        {
            MusicVolume = GameObject.Find("Scroll View/Viewport/Content/MusicVolume").GetComponent<Slider>();
            MusicVolume.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
            MusicVolume.value = 0.5f;
        }

        if (GameObject.Find("Scroll View/Viewport/Content/SfxVolume") != null)
        {
            SFXVolume = GameObject.Find("Scroll View/Viewport/Content/SfxVolume").GetComponent<Slider>();
            SFXVolume.onValueChanged.AddListener(delegate { OnSFXVolumeChange(); });
            SFXVolume.value = 0.5f;
        }

        if (GameObject.Find("Scroll View/Viewport/Content/AmbientVolume") != null)
        {
            AmbientVolume = GameObject.Find("Scroll View/Viewport/Content/AmbientVolume").GetComponent<Slider>();
            AmbientVolume.onValueChanged.AddListener(delegate { OnAmbientVolumeChange(); });
            AmbientVolume.value = 0.5f;
        }
    }

    private void OnEnable()
    {
    }

    void Start()
    {

    }

    public void OnMasterVolumeChange()
    {

    }

    public void OnMusicVolumeChange()
    {

    }

    public void OnSFXVolumeChange()
    {

    }

    public void OnAmbientVolumeChange()
    {

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
