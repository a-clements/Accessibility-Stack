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
    [HideInInspector] public int PanelNumber;
    [HideInInspector] public string PanelName;

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

    public void CreatePanel()
    {
        Canvas canvas;
        canvas = FindObjectOfType<Canvas>();

        if (GameObject.Find(PanelName) == null)
        {
            Panel = new GameObject(PanelName);
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
        else
        {
            Debug.Log("Panel " + PanelName + " already exists.");
        }
    }

    public void CreateGameplay()
    {
        Panel = GameObject.Find("GamePlayPanel");

        if(Panel != null)
        {
            Button = new GameObject("Gameplay Button");
            Button.transform.SetParent(Panel.transform);
        }
        else
        {
            PanelName = "GamePlayPanel";
            CreatePanel();
        }
    }

    public void CreateControls()
    {
        Panel = GameObject.Find("ControlsPanel");

        if (Panel != null)
        {
            Button = new GameObject("Controls Button");
            Button.transform.SetParent(Panel.transform);
        }
        else
        {
            PanelName = "ControlsPanel";
            CreatePanel();
        }
    }

    public void CreateGraphics()
    {
        Panel = GameObject.Find("GraphicsPanel");

        if (Panel != null)
        {
            Button = new GameObject("Graphics Button");
            Button.transform.SetParent(Panel.transform);
        }
        else
        {
            PanelName = "GraphicsPanel";
            CreatePanel();
        }
    }

    public void CreateAudio()
    {
        Panel = GameObject.Find("AudioPanel");

        if (Panel != null)
        {
            Button = new GameObject("Audio Button");
            Button.transform.SetParent(Panel.transform);
        }
        else
        {
            PanelName = "AudioPanel";
            CreatePanel();
        }
    }

    public void Speak(string text)
    {
        Voice.Speak(text);
    }
}
