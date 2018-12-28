using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
    private Toggle ToggleControlType;
    private Slider SliderControlType;
    private Text TextControlType;
    private Image ImageControlType;
    public Canvas WindowSize;


    public Button ControlButtonPrefab;

    public Dropdown ControlDropdownPrefab;
    public Dropdown DifficultyDropdownPrefab;
    public Dropdown SpeedDropdownPrefab;
    public Dropdown ResolutionDropdownPrefab;
    public Dropdown AntiAliasingDropdownPrefab;
    public Dropdown TextureQualityDropdownPrefab;
    public Dropdown VSyncDropdownPrefab;

    public Toggle FullscreenTogglePrefab;

    public Slider DifficultySliderPrefab;
    public Slider SpeedSliderPrefab;

    public Text TextPrefab;

    public Image ImagePrefab;

    public GameObject PanelPrefab;

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
        WindowSize = FindObjectOfType<Canvas>();
    }

    public void CreatePanel()
    {
        WindowSize = FindObjectOfType<Canvas>();

        if (GameObject.Find(PanelName) == null)
        {
            Panel = Instantiate(PanelPrefab, transform.position, transform.rotation);
            Panel.name = PanelName;
            Panel.transform.SetParent(WindowSize.transform);
            Panel.transform.localPosition = new Vector3(0, 0, 0);
            Panel.GetComponent<RectTransform>().anchorMax = WindowSize.transform.GetChild(0).GetComponent<RectTransform>().anchorMax;
            Panel.GetComponent<RectTransform>().anchorMin = WindowSize.transform.GetChild(0).GetComponent<RectTransform>().anchorMin;
            Panel.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        }
        else
        {
            Debug.Log("Panel " + PanelName + " already exists.");
        }
    }

    public void CreateGameplay(int GameplayIndex)
    {
        Panel = GameObject.Find("GamePlayPanel");

        if (Panel == null)
        {
            PanelName = "GamePlayPanel";
            CreatePanel();
        }

        switch (GameplayIndex)
        {
            case 0:
                SliderControlType = Instantiate(DifficultySliderPrefab, transform.position, transform.rotation);
                SliderControlType.name = "Difficulty";
                SliderControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0).transform);
                SliderControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                SliderControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 20);
                SliderControlType.maxValue = 4;
                SliderControlType.wholeNumbers = true;
                break;

            case 1:
                DropdownControlType = Instantiate(DifficultyDropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.name = "Difficulty";
                DropdownControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0).transform);
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                break;

            case 2:
                SliderControlType = Instantiate(SpeedSliderPrefab, transform.position, transform.rotation);
                SliderControlType.name = "Speed";
                SliderControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0).transform);
                SliderControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                SliderControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 20);
                SliderControlType.maxValue = 4;
                SliderControlType.wholeNumbers = true;
                break;

            case 3:
                DropdownControlType = Instantiate(SpeedDropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.name = "Speed";
                DropdownControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0).transform);
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                break;
        }
    }

    public void CreateControls(int ControlIndex)
    {
        Panel = GameObject.Find("ControlsPanel");

        if (Panel == null)
        {
            PanelName = "ControlsPanel";
            CreatePanel();
        }

        switch (ControlIndex)
        {
            case 0:
                ButtonControlType = Instantiate(ControlButtonPrefab, transform.position, transform.rotation);
                ButtonControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0).transform);
                ButtonControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                ButtonControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                break;

            case 1:
                DropdownControlType = Instantiate(ControlDropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0).transform);
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                break;
        }
    }

    public void CreateGraphics(int GraphicsIndex)
    {
        Panel = GameObject.Find("GraphicsPanel");

        Canvas WindowSize;
        WindowSize = FindObjectOfType<Canvas>();
        Resolution[] Resolutions;

        if (Panel == null)
        {
            PanelName = "GraphicsPanel";
            CreatePanel();
        }

        switch (GraphicsIndex)
        {
            case 0:
                DropdownControlType = Instantiate(ResolutionDropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                DropdownControlType.name = "ResolutionDropdown";
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);

                Resolutions = Screen.resolutions;

                if (Resolutions != null)
                {
                    foreach (Resolution resolution in Resolutions)
                    {
                        DropdownControlType.options.Add(new Dropdown.OptionData(resolution.ToString()));
                    }
                }
                else
                {
                    WindowSize.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                    WindowSize.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                    WindowSize.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.5f;
                    WindowSize.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
                }
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/ResolutionDropdown").GetComponent<RectTransform>().anchorMin = new Vector2(0.008f, 0.96f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/ResolutionDropdown").GetComponent<RectTransform>().anchorMax = new Vector2(0.3f, 0.99f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/ResolutionDropdown").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                break;

            case 1:
                ToggleControlType = Instantiate(FullscreenTogglePrefab, transform.position, transform.rotation);
                ToggleControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                ToggleControlType.name = "FullScreenToggle";
                ToggleControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                ToggleControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/FullScreenToggle").GetComponent<RectTransform>().anchorMin = new Vector2(0.72f, 0.96f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/FullScreenToggle").GetComponent<RectTransform>().anchorMax = new Vector2(0.975f, 0.99f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/FullScreenToggle").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/FullScreenToggle").GetChild(1).GetComponent<Text>().text = "Full Screen";
                break;

            case 2:
                DropdownControlType = Instantiate(VSyncDropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                DropdownControlType.name = "VSync";
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/VSync").GetComponent<RectTransform>().anchorMin = new Vector2(0.72f, 0.96f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/VSync").GetComponent<RectTransform>().anchorMax = new Vector2(0.975f, 0.99f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/VSync").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                break;

            case 3:
                DropdownControlType = Instantiate(AntiAliasingDropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                DropdownControlType.name = "AntiAliasing";
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/AntiAliasing").GetComponent<RectTransform>().anchorMin = new Vector2(0.008f, 0.96f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/AntiAliasing").GetComponent<RectTransform>().anchorMax = new Vector2(0.3f, 0.99f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/AntiAliasing").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                break;

            case 4:
                DropdownControlType = Instantiate(TextureQualityDropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                DropdownControlType.name = "TextureQuality";
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/TextureQuality").GetComponent<RectTransform>().anchorMin = new Vector2(0.72f, 0.96f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/TextureQuality").GetComponent<RectTransform>().anchorMax = new Vector2(0.975f, 0.99f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/TextureQuality").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                break;
        }
    }

    public void CreateAudio(int AudioIndex)
    {
        Panel = GameObject.Find("AudioPanel");

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