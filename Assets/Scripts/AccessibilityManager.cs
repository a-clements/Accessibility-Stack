using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Speech.Synthesis; //native functionality does not work in unity so it has to be done with the speechlib.
using SpeechLib;
using UnityEngine.UI;

public class AccessibilityManager : MonoBehaviour
{
    public static AccessibilityManager ManagerInstance = null;
    //SpeechSynthesizer Voice = new SpeechSynthesizer();
    SpVoice Voice = new SpVoice();
    private GameObject Panel;
    private GameObject Button;

    public KeyCode[] Keys;
    public ButtonRemapping[] Buttons;
    public DropdownRemapping[] Dropdowns;
    public Button_TTS[] Button_TTS;

    private void Awake()
    {
        if (ManagerInstance == null)
        {
            ManagerInstance = this;
        }

        if (ManagerInstance != this)
        {
            Destroy(gameObject);
        }
    }

    public void OnEnable()
    {
        Buttons = FindObjectsOfType<ButtonRemapping>();
        Dropdowns = FindObjectsOfType<DropdownRemapping>();
        Button_TTS = FindObjectsOfType<Button_TTS>();
    }

    public void CreateGamePlayPanel()
    {
        Canvas canvas;
        canvas = FindObjectOfType<Canvas>();
        Panel = new GameObject("GameplayPanel");
        Panel.transform.SetParent(canvas.transform);
        Panel.AddComponent<Image>();
        Panel.transform.localPosition = new Vector3(0, 0, 0);
        Panel.GetComponent<RectTransform>().anchorMax = canvas.transform.GetChild(0).GetComponent<RectTransform>().anchorMax;
        Panel.GetComponent<RectTransform>().anchorMin = canvas.transform.GetChild(0).GetComponent<RectTransform>().anchorMin;
        Panel.GetComponent<Image>().rectTransform.sizeDelta = canvas.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta;
        Panel.GetComponent<Image>().sprite = canvas.transform.GetChild(0).GetComponent<Image>().sprite;
        Panel.GetComponent<Image>().type = Image.Type.Sliced;
        Panel.GetComponent<Image>().color = canvas.transform.GetChild(0).GetComponent<Image>().color;
    }

    public void CreateControlsPanel()
    {
        Canvas canvas;
        canvas = FindObjectOfType<Canvas>();
        Panel = new GameObject("ControlsPanel");
        Panel.transform.SetParent(canvas.transform);
        Panel.AddComponent<Image>();
        Panel.transform.localPosition = new Vector3(0, 0, 0);
        Panel.GetComponent<RectTransform>().anchorMax = canvas.transform.GetChild(0).GetComponent<RectTransform>().anchorMax;
        Panel.GetComponent<RectTransform>().anchorMin = canvas.transform.GetChild(0).GetComponent<RectTransform>().anchorMin;
        Panel.GetComponent<Image>().rectTransform.sizeDelta = canvas.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta;
        Panel.GetComponent<Image>().sprite = canvas.transform.GetChild(0).GetComponent<Image>().sprite;
        Panel.GetComponent<Image>().type = Image.Type.Sliced;
        Panel.GetComponent<Image>().color = canvas.transform.GetChild(0).GetComponent<Image>().color;
    }

    public void CreateGraphicsPanel()
    {
        Canvas canvas;
        canvas = FindObjectOfType<Canvas>();
        Panel = new GameObject("GraphicsPanel");
        Panel.transform.SetParent(canvas.transform);
        Panel.AddComponent<Image>();
        Panel.transform.localPosition = new Vector3(0, 0, 0);
        Panel.GetComponent<RectTransform>().anchorMax = canvas.transform.GetChild(0).GetComponent<RectTransform>().anchorMax;
        Panel.GetComponent<RectTransform>().anchorMin = canvas.transform.GetChild(0).GetComponent<RectTransform>().anchorMin;
        Panel.GetComponent<Image>().rectTransform.sizeDelta = canvas.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta;
        Panel.GetComponent<Image>().sprite = canvas.transform.GetChild(0).GetComponent<Image>().sprite;
        Panel.GetComponent<Image>().type = Image.Type.Sliced;
        Panel.GetComponent<Image>().color = canvas.transform.GetChild(0).GetComponent<Image>().color;
    }

    public void CreateAudioPanel()
    {
        Canvas canvas;
        canvas = FindObjectOfType<Canvas>();
        Panel = new GameObject("AudioPanel");
        Panel.transform.SetParent(canvas.transform);
        Panel.AddComponent<Image>();
        Panel.transform.localPosition = new Vector3(0, 0, 0);
        Panel.GetComponent<RectTransform>().anchorMax = canvas.transform.GetChild(0).GetComponent<RectTransform>().anchorMax;
        Panel.GetComponent<RectTransform>().anchorMin = canvas.transform.GetChild(0).GetComponent<RectTransform>().anchorMin;
        Panel.GetComponent<Image>().rectTransform.sizeDelta = canvas.transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta;
        Panel.GetComponent<Image>().sprite = canvas.transform.GetChild(0).GetComponent<Image>().sprite;
        Panel.GetComponent<Image>().type = Image.Type.Sliced;
        Panel.GetComponent<Image>().color = canvas.transform.GetChild(0).GetComponent<Image>().color;
    }

    public void Speak(string text)
    {
        Voice.Speak(text);
    }
}
