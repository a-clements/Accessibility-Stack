using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ButtonRemapping : MonoBehaviour
{
    /*This script will assign new keycodes on on either a button click or a change of a dropdown. There are a few bugs.                                              */
    /*1. Pressing return will click the button but doesn't allow for a new button to be assigned because the enter key is also the submit within the event manager.  */
    /*Not everyone can use a mouse to click a button so being able to press enter and start the keyremapping is an ideal if it is at all possible.                   */
    /*2. It's not generic. There are different methods for different control methods such as dropdowns for setting up a controller.                                  */
    /*3. It doesn't work with controllers.                                                                                                                           */
    /*Being able to use the same script for controllers would greatly simplify the process and remove external dependancies such as incontrol.                       */
    /*This would make it a truly one size fits most script.                                                                                                          */
    /*As the script exists now it would need to be attached to each individual control. While this is ok, a better option would be to have the script attached to a  */
    /*single game object that can access all controls.                                                                                                               */

    public Button Button;
    private string text;
    public KeyCode Keycode;

    private bool IsButtonPressed = false;

    Event KeyEvent;

    private void Awake()
    {
        Button = this.gameObject.GetComponent<Button>();
    }

    public void ButtonClick()
    {
        Button.transform.GetChild(0).GetComponent<Text>().text = "Please enter a new key";
        text = "ButtonPressed";

        if (IsButtonPressed == false)
        {
            StartCoroutine(GetNewKey());
        }
    }

    private void OnGUI()
    {
        KeyEvent = Event.current;

        if (KeyEvent.isKey && IsButtonPressed == true)
        {
            Keycode = KeyEvent.keyCode;
            IsButtonPressed = false;
        }
    }

    IEnumerator WaitForKey()
    {
        while (!KeyEvent.isKey)
        {
            yield return null;
        }
    }

    public IEnumerator GetNewKey()
    {
        IsButtonPressed = true;
        yield return WaitForKey();

        switch (text)
        {
            case "ButtonPressed":
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                StopCoroutine(GetNewKey());
                break;
        }
    }
}
