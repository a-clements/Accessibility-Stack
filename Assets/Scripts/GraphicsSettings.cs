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

    private Resolution[] Resolution;

    private void Awake()
    {
        WindowSize = FindObjectOfType<Canvas>();
        Resolution = Screen.resolutions;

        switch (this.name)
        {
            case "ResolutionMenu":
                ResolutionDropdown = this.GetComponent<Dropdown>();
                break;
            case "FullScreenToggle":
                FullscreenToggle = this.GetComponent<Toggle>();
                break;
        }
    }

    private void OnEnable()
    {
        if(ResolutionDropdown != null)
        {
            ResolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        }

        else if(FullscreenToggle != null)
        {
            FullscreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
