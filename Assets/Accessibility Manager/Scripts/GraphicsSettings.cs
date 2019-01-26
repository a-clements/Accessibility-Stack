﻿using System.Collections;
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
    public GameObject MainHSVPanel;
    public GameObject SecondHSVPanel;

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
        MainHSVPanel = GameObject.Find("GraphicsPanel/Scroll View/Viewport/Content/MainHSVPanel");
        SecondHSVPanel = GameObject.Find("GraphicsPanel/Scroll View/Viewport/Content/SecondHSVPanel");


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

        if (MainHSVPanel != null)
        {
            MainHSVPanel.transform.GetChild(1).GetComponent<Slider>().onValueChanged.AddListener(delegate { OnMainHSVChanged(); });
            MainHSVPanel.transform.GetChild(2).GetComponent<Slider>().onValueChanged.AddListener(delegate { OnMainHSVChanged(); });
            MainHSVPanel.transform.GetChild(3).GetComponent<Slider>().onValueChanged.AddListener(delegate { OnMainHSVChanged(); });
        }

        if (SecondHSVPanel != null)
        {
            SecondHSVPanel.transform.GetChild(1).GetComponent<Slider>().onValueChanged.AddListener(delegate { OnSecondHSVChanged(); });
            SecondHSVPanel.transform.GetChild(2).GetComponent<Slider>().onValueChanged.AddListener(delegate { OnSecondHSVChanged(); });
            SecondHSVPanel.transform.GetChild(3).GetComponent<Slider>().onValueChanged.AddListener(delegate { OnSecondHSVChanged(); });
        }
    }

    private void OnMainHSVChanged()
    {
        OnMainColourLoad(Color.HSVToRGB(MainHSVPanel.transform.GetChild(1).GetComponent<Slider>().value,
            MainHSVPanel.transform.GetChild(2).GetComponent<Slider>().value,
            MainHSVPanel.transform.GetChild(2).GetComponent<Slider>().value));
    }

    private void OnSecondHSVChanged()
    {
        OnSecondColourLoad(Color.HSVToRGB(SecondHSVPanel.transform.GetChild(1).GetComponent<Slider>().value,
            SecondHSVPanel.transform.GetChild(2).GetComponent<Slider>().value,
            SecondHSVPanel.transform.GetChild(2).GetComponent<Slider>().value));
    }

    private void OnMainColourChange()
    {
        switch (MainColour.value)
        {
            case 0:
                OnMainColourLoad(Color.black);
                break;
            case 1:
                OnMainColourLoad(Color.blue);
                break;
            case 2:
                OnMainColourLoad(Color.cyan);
                break;
            case 3:
                OnMainColourLoad(Color.green);
                break;
            case 4:
                OnMainColourLoad(Color.grey);
                break;
            case 5:
                OnMainColourLoad(Color.magenta);
                break;
            case 6:
                OnMainColourLoad(Color.red);
                break;
            case 7:
                OnMainColourLoad(Color.white);
                break;
            case 8:
                OnMainColourLoad(Color.yellow);
                break;
        }
    }

    private void OnSecondColourChange()
    {
        switch (SecondColour.value)
        {
            case 0:
                OnSecondColourLoad(Color.black);
                break;
            case 1:
                OnSecondColourLoad(Color.blue);
                break;
            case 2:
                OnSecondColourLoad(Color.cyan);
                break;
            case 3:
                OnSecondColourLoad(Color.green);
                break;
            case 4:
                OnSecondColourLoad(Color.grey);
                break;
            case 5:
                OnSecondColourLoad(Color.magenta);
                break;
            case 6:
                OnSecondColourLoad(Color.red);
                break;
            case 7:
                OnSecondColourLoad(Color.white);
                break;
            case 8:
                OnSecondColourLoad(Color.yellow);
                //Color.RGBToHSV(Color.yellow, out Hue, out Saturation, out Brightness);
                break;
        }
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