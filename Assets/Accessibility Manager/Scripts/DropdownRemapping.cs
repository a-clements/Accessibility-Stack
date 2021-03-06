﻿using UnityEngine;
using UnityEngine.UI;
using System;

public class DropdownRemapping : MonoBehaviour
{
    private string[] codes;
    public Dropdown Dropdown;
    private KeyCode Keycode;
    private int Index;
    private bool initial = true;

    private void Awake()
    {
        Dropdown = this.gameObject.GetComponent<Dropdown>();
        Index = this.transform.GetSiblingIndex();
    }

    private void Start()
    {
        Keycode = UIManager.ManagerInstance.Keys[Index - 1];

        int i = 0;

        while(Dropdown.options[i].text != Keycode.ToString())
        {
            i++;
        }

        Dropdown.value = i;
    }

    private void OnEnable()
    {
        Dropdown.ClearOptions();

        codes = Enum.GetNames(typeof(KeyCode));

        foreach (string code in codes)
        {
            Dropdown.options.Add(new Dropdown.OptionData(code));
        }

        Dropdown.onValueChanged.AddListener(delegate { OnDropdownChange(); });
    }

    public void OnDropdownChange()
    {
        Keycode = (KeyCode)Enum.Parse(typeof(KeyCode), Dropdown.options[Dropdown.value].text, true);

        UIManager.ManagerInstance.Keys[Index - 1] = Keycode;

        if (initial == false)
        {
            UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
        }

        initial = false;
    }
}
