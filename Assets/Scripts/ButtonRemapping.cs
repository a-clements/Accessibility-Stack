using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ButtonRemapping : MonoBehaviour
{
    /*This script will assign new keycodes on on either a button click or controller button 0 being pressed. There are a no known bugs.                                    */
    /*Pressing return will invoke OnButtonClick(). Pressing return a second time will continue the loop until either a new button is pressed. Or a mouse button clicked.   */
    /*Clicking the button with a mouse will invoke the OnButtonClick() but follow it's own subsystem.                                                                      */
    /*Pressing button 0 on a gamepad will invoke the OnButtonClick() but follows its own subsystem. Pressing button 1 will cancel the button remapping.                    */
    /*The gamepad subsystem is entirely segregated from the keyboard and mouse subsystems. The keyboard and mouse subsystem have an overlap.                               */

    public Button Button;
    private KeyCode Keycode;
    private KeyCode OldKeycode;
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
        Keycode = UIManager.ManagerInstance.Keys[Index];
        Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
    }

    private void Update()
    {
        if(Input.GetAxis(UIManager.ManagerInstance.Trigger) > 0.0f)
        {
            Debug.Log(UIManager.ManagerInstance.Trigger);
        }
    }

    public void OnButtonClick()
    {
        OldKeycode = UIManager.ManagerInstance.Keys[Index];
        Button.transform.GetChild(0).GetComponent<Text>().text = "Please enter a new key";

        UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);

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
            if (KeyEvent.keyCode != KeyCode.Return && KeyEvent.keyCode != KeyCode.KeypadEnter)
            {
                if (KeyEvent.keyCode != KeyCode.Escape)
                {
                    Keycode = KeyEvent.keyCode;

                    UIManager.ManagerInstance.Keys[Index] = Keycode;

                    if (UIManager.ManagerInstance.Keys[Index] == KeyCode.None)
                    {
                        UIManager.ManagerInstance.Keys[Index] = OldKeycode;
                    }

                    string keytext = Keycode.ToString();
                    int stringlength = keytext.Length;

                    if (stringlength > 5 && stringlength < 7)
                    {
                        if (keytext == "SysReq")
                        {
                            Button.transform.GetChild(0).GetComponent<Text>().text = "System Requirements";
                            UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
                        }
                        else
                        {
                            keytext = keytext.Substring(0, 5) + " " + keytext.Substring(5, stringlength - 5);

                            Button.transform.GetChild(0).GetComponent<Text>().text = keytext;
                            UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
                        }
                    }
                    else
                    {
                        Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                        UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
                    }

                    IsButtonPressed = false;
                }
                else
                {
                    UIManager.ManagerInstance.Speak("Cancel");
                    Keycode = UIManager.ManagerInstance.Keys[Index];
                    Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                    IsButtonPressed = false;
                }
            }
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

            UIManager.ManagerInstance.Keys[Index] = Keycode;

            string keytext = Keycode.ToString();
            int stringlength = keytext.Length;
            keytext = keytext.Substring(0, 5) + " " + keytext.Substring(5, stringlength - 5);

            Button.transform.GetChild(0).GetComponent<Text>().text = keytext;
            UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);

            IsButtonPressed = false;
        }
    }

    public IEnumerator GetNewKey()
    {
        IsButtonPressed = true;
        yield return WaitForKey();

        StopCoroutine(GetNewKey());
    }

    IEnumerator WaitForKey()
    {
        while (!KeyEvent.isKey)
        {
            yield return null;
        }
    }

    private IEnumerator GetNewJoystickButton()
    {
        IsButtonPressed = true;
        yield return WaitForJoystick();

        StopCoroutine(GetNewJoystickButton());
    }

    IEnumerator WaitForJoystick()
    {
        while (IsButtonPressed == true)
        {
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                IsButtonPressed = false;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                UIManager.ManagerInstance.Speak("Cancel");
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton2;
                UIManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Action 1";
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton3;
                UIManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Action 2";
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton4))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton4;
                UIManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Left Bumper";
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton5;
                UIManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Right Bumper";
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton6))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton6;
                UIManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Status";
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton7;
                UIManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Pause";
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton8))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton8;
                UIManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Left Analogue Button";
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton9))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton9;
                UIManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = "Right Analogue Button";
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton10))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton10;
                UIManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton11))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton11;
                UIManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton12))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton12;
                UIManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton13))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton13;
                UIManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton14))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton14;
                UIManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton15))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton15;
                UIManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton16))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton16;
                UIManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton17))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton17;
                UIManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton18))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton18;
                UIManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetKeyDown(KeyCode.JoystickButton19))
            {
                IsButtonPressed = false;
                Keycode = KeyCode.JoystickButton19;
                UIManager.ManagerInstance.Keys[Index] = Keycode;
                Button.transform.GetChild(0).GetComponent<Text>().text = Keycode.ToString();
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetAxis("Left Trigger") != 0.0f)
            {
                IsButtonPressed = false;
                UIManager.ManagerInstance.Trigger = "Left Trigger";
                Button.transform.GetChild(0).GetComponent<Text>().text = UIManager.ManagerInstance.Trigger;
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetAxis("Right Trigger") != 0.0f)
            {
                IsButtonPressed = false;
                UIManager.ManagerInstance.Trigger = "Right Trigger";
                Button.transform.GetChild(0).GetComponent<Text>().text = UIManager.ManagerInstance.Trigger;
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetAxis("DPad Vertical") != 0.0f)
            {
                IsButtonPressed = false;
                UIManager.ManagerInstance.Trigger = "DPad Vertical";
                Button.transform.GetChild(0).GetComponent<Text>().text = UIManager.ManagerInstance.Trigger;
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            else if (Input.GetAxis("DPad Horizontal") != 0.0f)
            {
                IsButtonPressed = false;
                UIManager.ManagerInstance.Trigger = "DPad Horizontal";
                Button.transform.GetChild(0).GetComponent<Text>().text = UIManager.ManagerInstance.Trigger;
                UIManager.ManagerInstance.Speak(this.transform.GetComponentInChildren<Text>().text);
            }

            yield return null;
        }
    }
}
