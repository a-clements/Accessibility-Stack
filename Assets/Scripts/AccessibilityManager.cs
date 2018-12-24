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
    private Button ButtonControlType;
    private Dropdown DropdownControlType;
    public Button ButtonPrefab;
    public Dropdown DropdownPrefab;

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
        Canvas canvas;
        canvas = FindObjectOfType<Canvas>();

        if (Panel == null)
        {
            PanelName = "GamePlayPanel";
            CreatePanel();
        }
    }

    public void CreateControls(int ControlIndex)
    {
        Panel = GameObject.Find("ControlsPanel");
        Canvas canvas;
        canvas = FindObjectOfType<Canvas>();

        if (Panel == null)
        {
            PanelName = "ControlsPanel";
            CreatePanel();
        }

        switch (ControlIndex)
        {
            case 0:
                ButtonControlType = Instantiate(ButtonPrefab, transform.position, transform.rotation);
                ButtonControlType.transform.SetParent(Panel.transform);
                ButtonControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                ButtonControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                ButtonControlType.gameObject.AddComponent<ButtonRemapping>();
                ButtonControlType.gameObject.AddComponent<Button_TTS>();
                break;

            case 1:
                DropdownControlType = Instantiate(DropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.transform.SetParent(Panel.transform);
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                DropdownControlType.gameObject.AddComponent<DropdownRemapping>();
                DropdownControlType.gameObject.AddComponent<Button_TTS>();
                break;
        }
    }

    public void CreateGraphics(int GraphicsIndex)
    {
        Panel = GameObject.Find("GraphicsPanel");
        Canvas canvas;
        canvas = FindObjectOfType<Canvas>();

        if (Panel == null)
        {
            PanelName = "GraphicsPanel";
            CreatePanel();
        }
    }

    public void CreateAudio(int AudioIndex)
    {
        Panel = GameObject.Find("AudioPanel");
        Canvas canvas;
        canvas = FindObjectOfType<Canvas>();

        if (Panel == null)
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
