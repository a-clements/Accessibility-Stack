﻿using UnityEngine;
//using System.Speech.Synthesis; //native functionality does not work in unity so it has to be done with the speechlib.
using SpeechLib;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class UIManager : MonoBehaviour
{
    public static UIManager ManagerInstance = null;
    //SpeechSynthesizer Voice = new SpeechSynthesizer();
    private SpVoice Voice = new SpVoice();
    private GameObject Canvas;
    private GameObject MainMenu;
    private GameObject Panel;
    private GameObject PanelControlType;
    private Button ButtonControlType;
    private Dropdown DropdownControlType;
    private Toggle ToggleControlType;
    private Slider SliderControlType;
    private Text TextControlType;
    private Image ImageControlType;

    public TTS[] TTS;
    public Canvas WindowSize;
    public Button ButtonPrefab;
    public Dropdown DropdownPrefab;
    public Toggle TogglePrefab;
    public Slider SliderPrefab;
    public Text TextPrefab;
    public Image ImagePrefab;
    public GameObject HSVPanelPrefab;
    public GameObject PanelPrefab;
    public GameObject MainMenuPanel;

    public KeyCode[] Keys;
    public string[] Movement;
    public Resolution[] Resolutions;

    public GameObject GamePlay;
    public GameObject Controls;
    public GameObject Graphics;
    public GameObject Audio;

    [HideInInspector]public string PanelName;
    public float SpeechVolume = 0.0f;

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

    private void OnEnable()
    {
        WindowSize = FindObjectOfType<Canvas>();

        TTS = FindObjectsOfType<TTS>();
    }

    private void Start()
    {
        if(GameObject.Find("Canvas/GamePlayPanel") != null)
        {
            GamePlay = GameObject.Find("Canvas/GamePlayPanel");
            GamePlay.SetActive(false);
        }

        if (GameObject.Find("Canvas/ControlsPanel") != null)
        {
            Controls = GameObject.Find("Canvas/ControlsPanel");
            Controls.SetActive(false);
        }

        if (GameObject.Find("Canvas/GraphicsPanel") != null)
        {
            Graphics = GameObject.Find("Canvas/GraphicsPanel");
            Graphics.SetActive(false);
        }

        if (GameObject.Find("Canvas/AudioPanel") != null)
        {
            Audio = GameObject.Find("Canvas/AudioPanel");
            Audio.SetActive(false);
        }
    }

    public void CreateMainMenu()
    {
        Canvas = new GameObject("Canvas");
        Canvas.AddComponent<Canvas>();
        Canvas.AddComponent<CanvasScaler>();
        Canvas.AddComponent<GraphicRaycaster>();
        Canvas.GetComponent<RectTransform>().position = new Vector2(574.5f,271f);
        Canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(1149f,542f);
        Canvas.GetComponent<RectTransform>().localScale = new Vector3(1.0f,1.0f,1.0f);
        Canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        WindowSize = Canvas.GetComponent<Canvas>();

        MainMenu = Instantiate(MainMenuPanel,transform.position,transform.rotation);
        MainMenu.name = "MainMenuPanel";
        MainMenu.transform.SetParent(WindowSize.transform);
        MainMenu.transform.localPosition = new Vector3(0, 0, 0);
        MainMenu.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        MainMenu.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public void CreatePanel()
    {
        WindowSize = FindObjectOfType<Canvas>();

        if(WindowSize == null)
        {
            CreateMainMenu();
        }

        if (GameObject.Find(PanelName) == null)
        {
            Panel = Instantiate(PanelPrefab, transform.position, transform.rotation);
            Panel.name = PanelName;
            Panel.transform.SetParent(WindowSize.transform);
            Panel.transform.localPosition = new Vector3(0, 0, 0);
            Panel.GetComponent<RectTransform>().anchorMax = WindowSize.transform.GetChild(0).GetComponent<RectTransform>().anchorMax;
            Panel.GetComponent<RectTransform>().anchorMin = WindowSize.transform.GetChild(0).GetComponent<RectTransform>().anchorMin;
            Panel.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            Panel.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);

            switch(PanelName)
            {
                case "GamePlay":
                    break;

                case "ControlsPanel":
                    break;

                case "GraphicsPanel":
                    Panel.AddComponent<GraphicsSettings>();
                    break;

                case "AudioPanel":
                    Panel.AddComponent<AudioSettings>();
                    break;
            }
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

        switch(GameplayIndex)
        {
            case 0:
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
                ButtonControlType = Instantiate(ButtonPrefab, transform.position, transform.rotation);
                ButtonControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                ButtonControlType.gameObject.AddComponent<ButtonRemapping>();
                ButtonControlType.gameObject.AddComponent<TTS>();
                ButtonControlType.GetComponent<RectTransform>().anchoredPosition = new Vector2(8.5f, 0);
                ButtonControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                break;

            case 1:
                DropdownControlType = Instantiate(DropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                DropdownControlType.gameObject.AddComponent<DropdownRemapping>();
                DropdownControlType.gameObject.AddComponent<TTS>();
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = new Vector2(8.5f, 0);
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                break;
        }
    }

    public void CreateGraphics(int GraphicsIndex)
    {
        Panel = GameObject.Find("GraphicsPanel");

        int i = 0;

        if (Panel == null)
        {
            PanelName = "GraphicsPanel";
            CreatePanel();
        }

        Canvas WindowSize;
        WindowSize = FindObjectOfType<Canvas>();

        switch (GraphicsIndex)
        {
            case 0:
                DropdownControlType = Instantiate(DropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                DropdownControlType.name = "ResolutionMenu";
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                DropdownControlType.transform.GetChild(3).gameObject.AddComponent<TTS>();
                DropdownControlType.transform.GetChild(3).GetComponent<Text>().text = "Resolution";

                Resolutions = Screen.resolutions;

                if (Resolutions != null)
                {
                    foreach (Resolution resolution in Resolutions)
                    {
                        DropdownControlType.options.Add(new Dropdown.OptionData(resolution.ToString()));
                    }
                }

                while (Screen.currentResolution.width != Resolutions[i].width)
                {
                    i++;
                }

                DropdownControlType.value = i;

                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/ResolutionMenu").GetComponent<RectTransform>().anchorMin = new Vector2(0.008f, 0.942f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/ResolutionMenu").GetComponent<RectTransform>().anchorMax = new Vector2(0.3f, 0.972f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/ResolutionMenu").GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/ResolutionMenu").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;

            case 1:
                ToggleControlType = Instantiate(TogglePrefab, transform.position, transform.rotation);
                ToggleControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                ToggleControlType.name = "FullScreenToggle";
                ToggleControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                ToggleControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                ToggleControlType.gameObject.AddComponent<TTS>();

                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/FullScreenToggle").GetComponent<RectTransform>().anchorMin = new Vector2(0.682f, 0.942f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/FullScreenToggle").GetComponent<RectTransform>().anchorMax = new Vector2(0.975f, 0.972f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/FullScreenToggle").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/FullScreenToggle").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/FullScreenToggle").GetChild(1).GetComponent<Text>().text = "Full Screen";
                break;

            case 2:
                DropdownControlType = Instantiate(DropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                DropdownControlType.name = "TextureQuality";
                DropdownControlType.options.Add(new Dropdown.OptionData("Low"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Medium"));
                DropdownControlType.options.Add(new Dropdown.OptionData("High"));
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                DropdownControlType.transform.GetChild(3).gameObject.AddComponent<TTS>();
                DropdownControlType.transform.GetChild(3).GetComponent<Text>().text = "TextureQuality";

                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/TextureQuality").GetComponent<RectTransform>().anchorMin = new Vector2(0.008f, 0.892f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/TextureQuality").GetComponent<RectTransform>().anchorMax = new Vector2(0.3f, 0.923f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/TextureQuality").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/TextureQuality").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;

            case 3:
                DropdownControlType = Instantiate(DropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                DropdownControlType.name = "VSync";
                DropdownControlType.options.Add(new Dropdown.OptionData("Don't Sync"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Every V Blank"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Every Second V Blank"));
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                DropdownControlType.transform.GetChild(3).gameObject.AddComponent<TTS>();
                DropdownControlType.transform.GetChild(3).GetComponent<Text>().text = "V Sync";

                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/VSync").GetComponent<RectTransform>().anchorMin = new Vector2(0.682f, 0.892f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/VSync").GetComponent<RectTransform>().anchorMax = new Vector2(0.975f, 0.923f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/VSync").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/VSync").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;

            case 4:
                DropdownControlType = Instantiate(DropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                DropdownControlType.name = "AntiAliasing";
                DropdownControlType.options.Add(new Dropdown.OptionData("0x"));
                DropdownControlType.options.Add(new Dropdown.OptionData("2x"));
                DropdownControlType.options.Add(new Dropdown.OptionData("4x"));
                DropdownControlType.options.Add(new Dropdown.OptionData("6x"));
                DropdownControlType.options.Add(new Dropdown.OptionData("8x"));
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                DropdownControlType.transform.GetChild(3).gameObject.AddComponent<TTS>();
                DropdownControlType.transform.GetChild(3).GetComponent<Text>().text = "AntiAliasing";

                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/AntiAliasing").GetComponent<RectTransform>().anchorMin = new Vector2(0.008f, 0.845f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/AntiAliasing").GetComponent<RectTransform>().anchorMax = new Vector2(0.3f, 0.875f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/AntiAliasing").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/AntiAliasing").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;

            case 5:
                SliderControlType = Instantiate(SliderPrefab, transform.position, transform.rotation);
                SliderControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                SliderControlType.name = "GammaCorrection";
                SliderControlType.transform.GetChild(3).GetComponent<Text>().text = "Gamma Correction";
                SliderControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                SliderControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                SliderControlType.gameObject.AddComponent<TTS>();

                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/GammaCorrection").GetComponent<RectTransform>().anchorMin = new Vector2(0.682f, 0.85f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/GammaCorrection").GetComponent<RectTransform>().anchorMax = new Vector2(0.975f, 0.87f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/GammaCorrection").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/GammaCorrection").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;

            case 6:
                DropdownControlType = Instantiate(DropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                DropdownControlType.name = "MainColour";
                DropdownControlType.options.Add(new Dropdown.OptionData("Black"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Blue"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Cyan"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Green"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Grey"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Magenta"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Red"));
                DropdownControlType.options.Add(new Dropdown.OptionData("White"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Yellow"));
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                DropdownControlType.transform.GetChild(3).gameObject.AddComponent<TTS>();
                DropdownControlType.transform.GetChild(3).GetComponent<Text>().text = "Main Colour";

                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/MainColour").GetComponent<RectTransform>().anchorMin = new Vector2(0.008f, 0.796f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/MainColour").GetComponent<RectTransform>().anchorMax = new Vector2(0.3f, 0.83f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/MainColour").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/MainColour").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);

                PanelControlType = Instantiate(HSVPanelPrefab, transform.position, transform.rotation);
                PanelControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                PanelControlType.name = "MainHSVPanel";
                PanelControlType.transform.localPosition = new Vector3(0, 0, 0);
                PanelControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                PanelControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                PanelControlType.transform.GetChild(0).GetChild(3).GetComponent<Text>().text = "Hue";
                PanelControlType.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = "Saturation";
                PanelControlType.transform.GetChild(2).GetChild(3).GetComponent<Text>().text = "Brightness";

                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/MainHSVPanel").GetComponent<RectTransform>().anchorMin = new Vector2(0.008f, 0.69f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/MainHSVPanel").GetComponent<RectTransform>().anchorMax = new Vector2(0.3f, 0.796f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/MainHSVPanel").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/MainHSVPanel").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                PanelControlType.transform.SetParent(DropdownControlType.transform);
                break;

            case 7:
                DropdownControlType = Instantiate(DropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                DropdownControlType.name = "SecondColour";
                DropdownControlType.options.Add(new Dropdown.OptionData("Black"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Blue"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Cyan"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Green"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Grey"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Magenta"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Red"));
                DropdownControlType.options.Add(new Dropdown.OptionData("White"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Yellow"));
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                DropdownControlType.transform.GetChild(3).gameObject.AddComponent<TTS>();
                DropdownControlType.transform.GetChild(3).GetComponent<Text>().text = "Second Colour";

                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/SecondColour").GetComponent<RectTransform>().anchorMin = new Vector2(0.682f, 0.796f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/SecondColour").GetComponent<RectTransform>().anchorMax = new Vector2(0.975f, 0.83f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/SecondColour").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/SecondColour").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
 
                PanelControlType = Instantiate(HSVPanelPrefab, transform.position, transform.rotation);
                PanelControlType.name = "SecondHSVPanel";
                PanelControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                PanelControlType.transform.localPosition = new Vector3(0, 0, 0);
                PanelControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                PanelControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                PanelControlType.transform.GetChild(0).GetChild(3).GetComponent<Text>().text = "Hue";
                PanelControlType.transform.GetChild(1).GetChild(3).GetComponent<Text>().text = "Saturation";
                PanelControlType.transform.GetChild(2).GetChild(3).GetComponent<Text>().text = "Brightness";

                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/SecondHSVPanel").GetComponent<RectTransform>().anchorMin = new Vector2(0.682f, 0.69f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/SecondHSVPanel").GetComponent<RectTransform>().anchorMax = new Vector2(0.975f, 0.796f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/SecondHSVPanel").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/SecondHSVPanel").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                PanelControlType.transform.SetParent(DropdownControlType.transform);
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

        switch(AudioIndex)
        {
            case 0:
                ToggleControlType = Instantiate(TogglePrefab, transform.position, transform.rotation);
                ToggleControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                ToggleControlType.name = "TextToSpeech";
                ToggleControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                ToggleControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                ToggleControlType.transform.GetChild(1).gameObject.AddComponent<TTS>();
                ToggleControlType.transform.GetChild(1).GetComponent<Text>().text = "Text To Speech";


                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/TextToSpeech").GetComponent<RectTransform>().anchorMin = new Vector2(0.008f, 0.942f);
                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/TextToSpeech").GetComponent<RectTransform>().anchorMax = new Vector2(0.3f, 0.972f);
                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/TextToSpeech").GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/TextToSpeech").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;

            case 1:
                SliderControlType = Instantiate(SliderPrefab, transform.position, transform.rotation);
                SliderControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                SliderControlType.name = "SpeechVolume";
                SliderControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                SliderControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                SliderControlType.GetComponent<Slider>().minValue = 0;
                SliderControlType.GetComponent<Slider>().maxValue = 10;
                SliderControlType.GetComponent<Slider>().wholeNumbers = true;
                SliderControlType.GetComponent<Slider>().value = 5;
                SliderControlType.transform.GetChild(3).gameObject.AddComponent<TTS>();
                SliderControlType.transform.GetChild(3).GetComponent<Text>().text = "Speech Volume";

                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/SpeechVolume").GetComponent<RectTransform>().anchorMin = new Vector2(0.682f, 0.947f);
                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/SpeechVolume").GetComponent<RectTransform>().anchorMax = new Vector2(0.975f, 0.965f);
                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/SpeechVolume").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/SpeechVolume").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;

            case 2:
                SliderControlType = Instantiate(SliderPrefab, transform.position, transform.rotation);
                SliderControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                SliderControlType.name = "MasterVolume";
                SliderControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                SliderControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                SliderControlType.transform.GetChild(3).gameObject.AddComponent<TTS>();
                SliderControlType.transform.GetChild(3).GetComponent<Text>().text = "Master Volume";

                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/MasterVolume").GetComponent<RectTransform>().anchorMin = new Vector2(0.198f, 0.906f);
                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/MasterVolume").GetComponent<RectTransform>().anchorMax = new Vector2(0.795f, 0.925f);
                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/MasterVolume").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/MasterVolume").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;

            case 3:
                SliderControlType = Instantiate(SliderPrefab, transform.position, transform.rotation);
                SliderControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                SliderControlType.name = "MusicVolume";
                SliderControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                SliderControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                SliderControlType.transform.GetChild(3).gameObject.AddComponent<TTS>();
                SliderControlType.transform.GetChild(3).GetComponent<Text>().text = "Music Volume";

                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/MusicVolume").GetComponent<RectTransform>().anchorMin = new Vector2(0.198f, 0.874f);
                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/MusicVolume").GetComponent<RectTransform>().anchorMax = new Vector2(0.795f, 0.894f);
                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/MusicVolume").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/MusicVolume").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;

            case 4:
                SliderControlType = Instantiate(SliderPrefab, transform.position, transform.rotation);
                SliderControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                SliderControlType.name = "SfxVolume";
                SliderControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                SliderControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                SliderControlType.transform.GetChild(3).gameObject.AddComponent<TTS>();
                SliderControlType.transform.GetChild(3).GetComponent<Text>().text = "Special Effects Volume";

                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/SfxVolume").GetComponent<RectTransform>().anchorMin = new Vector2(0.198f, 0.844f);
                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/SfxVolume").GetComponent<RectTransform>().anchorMax = new Vector2(0.795f, 0.862f);
                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/SfxVolume").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/SfxVolume").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;

            case 5:
                SliderControlType = Instantiate(SliderPrefab, transform.position, transform.rotation);
                SliderControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                SliderControlType.name = "AmbientVolume";
                SliderControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                SliderControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                SliderControlType.transform.GetChild(3).gameObject.AddComponent<TTS>();
                SliderControlType.transform.GetChild(3).GetComponent<Text>().text = "Ambient Volume";

                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/AmbientVolume").GetComponent<RectTransform>().anchorMin = new Vector2(0.198f, 0.812f);
                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/AmbientVolume").GetComponent<RectTransform>().anchorMax = new Vector2(0.795f, 0.832f);
                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/AmbientVolume").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("AudioPanel/Scroll View/Viewport/Content/AmbientVolume").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
        }
    }

    public void Speak(string text)
    {
        //this switch statement checks what the value of the string variable OSType is and runs the correct switch statement
        Voice.Volume = (int)SpeechVolume;
        Voice.Speak(text);
    }
}