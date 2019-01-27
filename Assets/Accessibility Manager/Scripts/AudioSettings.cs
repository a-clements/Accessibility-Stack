using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public TTS[] TTS;
    public Toggle TextToSpeech;

    private void Awake()
    {
        TTS = FindObjectsOfType<TTS>();
        TextToSpeech = GameObject.Find("Scroll View/Viewport/Content/TextToSpeech").GetComponent<Toggle>();
    }


    private void OnEnable()
    {
        TextToSpeech.onValueChanged.AddListener(delegate { OnTextToSpeechToggle(); });
    }

    public void OnTextToSpeechToggle()
    {
        if (TextToSpeech.isOn == false)
        {
            foreach (TTS tts in TTS)
            {
                tts.enabled = false;
            }
        }
        else
        {
            foreach (TTS tts in TTS)
            {
                tts.enabled = true;
            }
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
