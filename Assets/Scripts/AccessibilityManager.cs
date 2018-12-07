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
    private GameObject ControlType;

    public KeyCode[] Keys;
    public ButtonRemapping[] Buttons;
    public DropdownRemapping[] Dropdowns;
    public Button_TTS[] Button_TTS;
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

    public void CreateGameplay(int GameplayIndex)
    {
        Panel = GameObject.Find("GamePlayPanel");

        if(Panel != null)
        {
            ControlType = new GameObject("Gameplay Button");
            ControlType.transform.SetParent(Panel.transform);
        }
        else
        {
            PanelName = "GamePlayPanel";
            CreatePanel();
        }
    }

    public void CreateControls(int ControlIndex)
    {
        Panel = GameObject.Find("ControlsPanel");

        if (Panel != null)
        {
            ControlType = new GameObject("Controls Button");

            ControlType.transform.SetParent(Panel.transform);
        }
        else
        {
            PanelName = "ControlsPanel";
            CreatePanel();
        }

        switch(ControlIndex)
        {
            case 0:
                ControlType.transform.SetParent(Panel.transform);
                ControlType.AddComponent<Image>();
                ControlType.AddComponent<Button>();
                ControlType.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                ControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                ControlType.GetComponent<Button>().transition = Selectable.Transition.ColorTint;
                ControlType.AddComponent<ButtonRemapping>();
                ControlType.AddComponent<Button_TTS>();
                GameObject Text = new GameObject("Text");
                Text.transform.SetParent(ControlType.transform);
                Text.AddComponent<Text>();
                Text.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                Text.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                Text.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                Text.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                Text.GetComponent<Text>().color = new Color(0.5f,0.5f,0.5f,1.0f);
                Text.GetComponent<Text>().text = "Button";
                Text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;

                ControlType.GetComponent<Image>();

                break;
            case 1:
                ControlType.AddComponent<Dropdown>();
                ControlType.GetComponent<Dropdown>().transition = Selectable.Transition.None;
                ControlType.AddComponent<DropdownRemapping>();
                ControlType.AddComponent<Button_TTS>();
                break;
        }
    }

    public void CreateGraphics(int GraphicsIndex)
    {
        Panel = GameObject.Find("GraphicsPanel");

        if (Panel != null)
        {
            ControlType = new GameObject("Graphics Button");
            ControlType.transform.SetParent(Panel.transform);
        }
        else
        {
            PanelName = "GraphicsPanel";
            CreatePanel();
        }
    }

    public void CreateAudio(int AudioIndex)
    {
        Panel = GameObject.Find("AudioPanel");

        if (Panel != null)
        {
            ControlType = new GameObject("Audio Button");
            ControlType.transform.SetParent(Panel.transform);
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
