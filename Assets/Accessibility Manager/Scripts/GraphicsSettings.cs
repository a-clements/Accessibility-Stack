using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    public Canvas WindowSize;
    public Dropdown ResolutionDropdown;
    public Toggle FullscreenToggle;
    public Dropdown TextureQuality;
    public Dropdown VSync;
    public Dropdown AA;
    public Slider Gamma;
    public Dropdown MainColour;
    public Dropdown SecondColour;

    private Resolution[] Resolution;
    public Button[] Buttons;
    public Dropdown[] Dropdowns;
    public Toggle[] Toggles;
    public Slider[] Sliders;
    public Text[] Texts;

    private void Awake()
    {
        WindowSize = FindObjectOfType<Canvas>();
        Buttons = FindObjectsOfType<Button>();
        Dropdowns = FindObjectsOfType<Dropdown>();
        Toggles = FindObjectsOfType<Toggle>();
        Sliders = FindObjectsOfType<Slider>();
        Texts = FindObjectsOfType<Text>();
        Resolution = Screen.resolutions;

        ResolutionDropdown = GameObject.Find("Scroll View/Viewport/Content/ResolutionMenu").GetComponent<Dropdown>();
        FullscreenToggle = GameObject.Find("Scroll View/Viewport/Content/FullScreenToggle").GetComponent<Toggle>();
        TextureQuality = GameObject.Find("Scroll View/Viewport/Content/TextureQuality").GetComponent<Dropdown>();
        VSync = GameObject.Find("Scroll View/Viewport/Content/VSync").GetComponent<Dropdown>();
        AA = GameObject.Find("Scroll View/Viewport/Content/AntiAliasing").GetComponent<Dropdown>();
        Gamma = GameObject.Find("Scroll View/Viewport/Content/GammaCorrection").GetComponent<Slider>();
        MainColour = GameObject.Find("GraphicsPanel/Scroll View/Viewport/Content/MainColour").GetComponent<Dropdown>();
        SecondColour = GameObject.Find("GraphicsPanel/Scroll View/Viewport/Content/SecondColour").GetComponent<Dropdown>();


        Gamma.value = 0.5f;
        MainColour.value = 0;
        SecondColour.value = 7;
    }

    private void OnEnable()
    {
        if (ResolutionDropdown != null)
        {
            ResolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        }

        if (FullscreenToggle != null)
        {
            FullscreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
        }

        if (TextureQuality != null)
        {
            TextureQuality.onValueChanged.AddListener(delegate { OnTextureChange(); });
        }

        if (VSync != null)
        {
            VSync.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        }

        if (AA != null)
        {
            AA.onValueChanged.AddListener(delegate { OnAAChange(); });
        }

        if (Gamma != null)
        {
            Gamma.onValueChanged.AddListener(delegate { OnGammaChange(); });
        }

        if (MainColour != null)
        {
            MainColour.onValueChanged.AddListener(delegate { OnMainColourChange(); });
        }

        if (SecondColour != null)
        {
            SecondColour.onValueChanged.AddListener(delegate { OnSecondColourChange(); });
        }
    }

    private void OnMainColourChange()
    {
        //float Hue = 0.0f;
        //float Saturation = 0.0f;
        //float Brightness = 0.0f;

        switch (MainColour.value)
        {
            case 0:
                OnMainColourLoad(Color.black);
                //Color.RGBToHSV(Color.black, out Hue, out Saturation, out Brightness);
                break;
            case 1:
                OnMainColourLoad(Color.blue);
                //Color.RGBToHSV(Color.blue, out Hue, out Saturation, out Brightness);
                break;
            case 2:
                OnMainColourLoad(Color.cyan);
                //Color.RGBToHSV(Color.cyan, out Hue, out Saturation, out Brightness);
                break;
            case 3:
                OnMainColourLoad(Color.green);
                //Color.RGBToHSV(Color.green, out Hue, out Saturation, out Brightness);
                break;
            case 4:
                OnMainColourLoad(Color.grey);
                //Color.RGBToHSV(Color.grey, out Hue, out Saturation, out Brightness);
                break;
            case 5:
                OnMainColourLoad(Color.magenta);
                //Color.RGBToHSV(Color.magenta, out Hue, out Saturation, out Brightness);
                break;
            case 6:
                OnMainColourLoad(Color.red);
                //Color.RGBToHSV(Color.red, out Hue, out Saturation, out Brightness);
                break;
            case 7:
                OnMainColourLoad(Color.white);
                //Color.RGBToHSV(Color.white, out Hue, out Saturation, out Brightness);
                break;
            case 8:
                OnMainColourLoad(Color.yellow);
                //Color.RGBToHSV(Color.yellow, out Hue, out Saturation, out Brightness);
                break;
        }

        //Gamemanager.UIMainColour = UIMainColour;
        //MainHue.value = Hue;
        //MainSaturation.value = Saturation;
        //MainBrightness.value = Brightness;
    }

    private void OnSecondColourChange()
    {
        //float Hue = 0.0f;
        //float Saturation = 0.0f;
        //float Brightness = 0.0f;

        switch (SecondColour.value)
        {
            case 0:
                OnSecondColourLoad(Color.black);
                //Color.RGBToHSV(Color.black, out Hue, out Saturation, out Brightness);
                break;
            case 1:
                OnSecondColourLoad(Color.blue);
                //Color.RGBToHSV(Color.blue, out Hue, out Saturation, out Brightness);
                break;
            case 2:
                OnSecondColourLoad(Color.cyan);
                //Color.RGBToHSV(Color.cyan, out Hue, out Saturation, out Brightness);
                break;
            case 3:
                OnSecondColourLoad(Color.green);
                //Color.RGBToHSV(Color.green, out Hue, out Saturation, out Brightness);
                break;
            case 4:
                OnSecondColourLoad(Color.grey);
                //Color.RGBToHSV(Color.grey, out Hue, out Saturation, out Brightness);
                break;
            case 5:
                OnSecondColourLoad(Color.magenta);
                //Color.RGBToHSV(Color.magenta, out Hue, out Saturation, out Brightness);
                break;
            case 6:
                OnSecondColourLoad(Color.red);
                //Color.RGBToHSV(Color.red, out Hue, out Saturation, out Brightness);
                break;
            case 7:
                OnSecondColourLoad(Color.white);
                //Color.RGBToHSV(Color.white, out Hue, out Saturation, out Brightness);
                break;
            case 8:
                OnSecondColourLoad(Color.yellow);
                //Color.RGBToHSV(Color.yellow, out Hue, out Saturation, out Brightness);
                break;
        }

        //Gamemanager.UIMainColour = UIMainColour;
        //MainHue.value = Hue;
        //MainSaturation.value = Saturation;
        //MainBrightness.value = Brightness;
    }

    private void OnMainColourLoad(Color Colour)
    {

        //this function makes all text components the same colour ans puts the current colour settings into a variable so that it can be saved and retrieved on reload
        foreach (Button buttons in Buttons)
        {
            var MainColour = buttons.GetComponent<Button>().colors;
            MainColour.normalColor = Colour;
            buttons.GetComponent<Button>().colors = MainColour;
        }

        foreach (Dropdown dropdown in Dropdowns)
        {
            var MainColour = dropdown.GetComponent<Dropdown>().colors;
            MainColour.normalColor = Colour;
            dropdown.GetComponent<Dropdown>().colors = MainColour;
        }

        foreach (Toggle toggle in Toggles)
        {
            var MainColour = toggle.GetComponent<Toggle>().colors;
            MainColour.normalColor = Colour;
            toggle.GetComponent<Toggle>().colors = MainColour;
        }

        foreach (Slider slider in Sliders)
        {
            var MainColour = slider.GetComponent<Slider>().colors;
            MainColour.normalColor = Colour;
            slider.GetComponent<Slider>().colors = MainColour;
        }
    }

    private void OnSecondColourLoad(Color Colour)
    {
        //this function makes all text components the same colour ans puts the current colour settings into a variable so that it can be saved and retrieved on reload
        foreach (Text texts in Texts)
        {
            var TextColour = texts.GetComponent<Text>().color;
            TextColour = Colour;
            texts.GetComponent<Text>().color = TextColour;
        }
    }

    private void OnResolutionChange()
    {
        //this function sets the game resolution and changes window size if the game is not in full screen mode
        Screen.SetResolution(Resolution[ResolutionDropdown.value].width, Resolution[ResolutionDropdown.value].height, FullscreenToggle.isOn);

        ResolutionDropdown.RefreshShownValue();

        if (!FullscreenToggle.isOn)
        {
            WindowSize.GetComponent<CanvasScaler>().referenceResolution = new Vector2(Resolution[ResolutionDropdown.value].width, Resolution[ResolutionDropdown.value].height);
        }
    }

    private void OnFullScreenToggle()
    {
        //this toggles fullscreen mode and calls the resolutionchanged function so that the screen size is adjusted correctly
        Screen.fullScreen = FullscreenToggle.isOn;
        OnResolutionChange();
    }

    private void OnTextureChange()
    {
        QualitySettings.masterTextureLimit = TextureQuality.value;
    }

    private void OnVSyncChange()
    {
        QualitySettings.vSyncCount = VSync.value;
    }

    private void OnAAChange()
    {
        QualitySettings.antiAliasing = (int)Mathf.Pow(2, AA.value);
    }

    private void OnGammaChange()
    {
        RenderSettings.ambientLight = new Color(Gamma.value, Gamma.value, Gamma.value, 1.0f);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}