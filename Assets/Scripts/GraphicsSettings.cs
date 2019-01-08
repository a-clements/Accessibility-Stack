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

    private Resolution[] Resolution;

    private void Awake()
    {
        WindowSize = FindObjectOfType<Canvas>();
        Resolution = Screen.resolutions;

        ResolutionDropdown = GameObject.Find("Scroll View/Viewport/Content/ResolutionMenu").GetComponent<Dropdown>();
        FullscreenToggle = GameObject.Find("Scroll View/Viewport/Content/FullScreenToggle").GetComponent<Toggle>();
        TextureQuality = GameObject.Find("Scroll View/Viewport/Content/TextureQuality").GetComponent<Dropdown>();
        VSync = GameObject.Find("Scroll View/Viewport/Content/VSync").GetComponent<Dropdown>();
        AA = GameObject.Find("Scroll View/Viewport/Content/AntiAliasing").GetComponent<Dropdown>();
        Gamma = GameObject.Find("Scroll View/Viewport/Content/GammaCorrection").GetComponent<Slider>();

        Gamma.value = 0.5f;
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
