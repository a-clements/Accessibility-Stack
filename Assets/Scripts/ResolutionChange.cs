using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ResolutionChange : MonoBehaviour
{
    public Dropdown dropdown;

    private Resolution[] Resolution;

    private void Awake()
    {
        dropdown = this.GetComponent<Dropdown>();

        Resolution = Screen.resolutions;
    }

    private void OnEnable()
    {

        dropdown.onValueChanged.AddListener(delegate { OnDropdownChange(); });
    }

    public void OnDropdownChange()
    {
        Screen.SetResolution(Resolution[dropdown.value].width, Resolution[dropdown.value].height, true);
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
