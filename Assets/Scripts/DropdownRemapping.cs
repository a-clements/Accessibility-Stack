using UnityEngine;
using UnityEngine.UI;
using System;

public class DropdownRemapping : MonoBehaviour
{
    private string[] codes;
    public Dropdown Dropdown;
    public KeyCode Keycode;

    private void Awake()
    {
        Dropdown = this.gameObject.GetComponent<Dropdown>();
    }

    private void OnEnable()
    {
        Dropdown.ClearOptions();

        codes = Enum.GetNames(typeof(KeyCode));

        foreach (string code in codes)
        {
            Dropdown.options.Add(new Dropdown.OptionData(code));
        }
    }

    public void OnDropdownChange()
    {
        Keycode = (KeyCode)Enum.Parse(typeof(KeyCode), Dropdown.options[Dropdown.value].text, true);
    }
}
