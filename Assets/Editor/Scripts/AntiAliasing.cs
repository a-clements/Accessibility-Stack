using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntiAliasing : MonoBehaviour
{
    public Dropdown AADropdown;

    public void OnAAChange()
    {
        QualitySettings.antiAliasing = (int)Mathf.Pow(2, AADropdown.value);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
