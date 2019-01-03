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
    private KeyCode Keycode;
    private int Index;

    private bool IsButtonPressed = false;

    Event KeyEvent;

    private void Awake()
    {
        Button = this.gameObject.GetComponent<Button>();
        Index = this.transform.GetSiblingIndex();
    }

    private void OnEnable()
    {
        Button.onClick.AddListener(delegate { OnButtonClick(); });
    }

    private void Start()
    {
        Keycode = AccessibilityManager.ManagerInstance.Keys[Index];
        Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
    }

    public void OnButtonClick()
    {
        Button.transform.GetChild(0).GetComponent<Text>().text = "Please enter a new key";

        AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);

        if(Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            StartCoroutine(GetNewJoystickButton());
        }

        else if (IsButtonPressed == false)
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

            AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;

            string keytext = Keycode.ToString();
            int stringlength = keytext.Length;

            if (stringlength > 5 && stringlength < 7)
            {
                if (keytext == "SysReq")
                {
                    Button.transform.GetChild(0).GetComponent<Text>().text = "System Requirements";
                    AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
                }
                else
                {
                    keytext = keytext.Substring(0, 5) + " " + keytext.Substring(5, stringlength - 5);

                    Button.transform.GetChild(0).GetComponent<Text>().text = keytext;
                    AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
                }
            }
            else
            {
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            IsButtonPressed = false;
        }

        if (KeyEvent.isMouse && IsButtonPressed == true)
        {
            if (KeyEvent.button == 0)
            {
                Keycode = KeyCode.Mouse0;
            }
            else if (KeyEvent.button == 1)
            {
                Keycode = KeyCode.Mouse1;
            }
            else if (KeyEvent.button == 2)
            {
                Keycode = KeyCode.Mouse2;
            }

            AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;

            string keytext = Keycode.ToString();
            int stringlength = keytext.Length;
            keytext = keytext.Substring(0, 5) + " " + keytext.Substring(5, stringlength - 5);

            Button.transform.GetChild(0).GetComponent<Text>().text = keytext;
            AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);

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

    IEnumerator WaitForJoystick()
    {
        while (IsButtonPressed == true)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                IsButtonPressed = false;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                AccessibilityManager.ManagerInstance.Speak("Cancel");
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton2;
                AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Action 1";
                AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton3;
                AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Action 2";
                AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton4))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton4;
                AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Left Bumper";
                AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton5;
                AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Right Bumper";
                AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton6))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton6;
                AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Status";
                AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton7;
                AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Pause";
                AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton8))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton8;
                AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Left Analogue Button";
                AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton9))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton9;
                AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Right Analogue Button";
                AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton10))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton10;
                AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton11))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton11;
                AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton12))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton12;
                AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton13))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton13;
                AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton14))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton14;
                AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton15))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton15;
                AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton16))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton16;
                AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton17))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton17;
                AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton18))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton18;
                AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton19))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton19;
                AccessibilityManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                AccessibilityManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            yield return null;
        }
    }

    public IEnumerator GetNewKey()
    {
        IsButtonPressed = true;
        yield return WaitForKey();

        StopCoroutine(GetNewKey());
    }

    private IEnumerator GetNewJoystickButton()
    {
        IsButtonPressed = true;
        yield return WaitForJoystick();

        StopCoroutine(GetNewJoystickButton());
    }
}
