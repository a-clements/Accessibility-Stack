using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using System.Speech.Synthesis; //native functionality does not work in unity so it has to be done with the speechlib.
using SpeechLib;
using UnityEngine.UI;
using UnityEditor;
using System.Text.RegularExpressions;

public class AccessibilityManager : MonoBehaviour
{
    public static AccessibilityManager ManagerInstance = null;
    //SpeechSynthesizer Voice = new SpeechSynthesizer();
    static SpVoice Voice = new SpVoice();
    private GameObject Panel;
    private Button ButtonControlType;
    private Dropdown DropdownControlType;
    private Toggle ToggleControlType;
    private Slider SliderControlType;
    private Text TextControlType;
    private Image ImageControlType;
    public Canvas WindowSize;
    public Button ButtonPrefab;
    public Dropdown DropdownPrefab;
    public Toggle TogglePrefab;
    public Slider SliderPrefab;
    public Text TextPrefab;
    public Image ImagePrefab;
    public GameObject PanelPrefab;

    public KeyCode[] Keys;
    public string Trigger;
    public ButtonRemapping[] Buttons;
    public DropdownRemapping[] Dropdowns;
    public TTS[] TTS;
    public Resolution[] Resolutions;

    public string PanelName;

    public static string AxisName;
    public static string DescriptiveAxisName;
    public static string DescriptiveNegativeAxisName;
    public static string NegativeButton;
    public static string PositiveButton;
    public static string AltNegativeButton;
    public static string AltPositiveButton;
    public static float AxisGravity;
    public static float AxisDeadZone = 0.001f;
    public static float AxisSensitivity = 1.0f;
    public static bool AxisSnap;
    public static bool AxisInvert;
    public static int ControlType;
    public static int AxisType;
    public static int JoyNum;

    static readonly List<AxisPreset> axisPresets = new List<AxisPreset>();

    private void Awake()
    {
        if (ManagerInstance == null)
        {
            ManagerInstance = this;
        }

        if (ManagerInstance != this)
        {
            Destroy(gameObject);
        }
    }

    public void OnEnable()
    {
        Buttons = FindObjectsOfType<ButtonRemapping>();
        Dropdowns = FindObjectsOfType<DropdownRemapping>();
        TTS = FindObjectsOfType<TTS>();
        WindowSize = FindObjectOfType<Canvas>();
    }

    public void CreatePanel()
    {
        WindowSize = FindObjectOfType<Canvas>();

        if (GameObject.Find(PanelName) == null)
        {
            Panel = Instantiate(PanelPrefab, transform.position, transform.rotation);
            Panel.name = PanelName;
            Panel.transform.SetParent(WindowSize.transform);
            Panel.transform.localPosition = new Vector3(0, 0, 0);
            Panel.GetComponent<RectTransform>().anchorMax = WindowSize.transform.GetChild(0).GetComponent<RectTransform>().anchorMax;
            Panel.GetComponent<RectTransform>().anchorMin = WindowSize.transform.GetChild(0).GetComponent<RectTransform>().anchorMin;
            Panel.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            Panel.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);

            switch(PanelName)
            {
                case "GamePlay":
                    break;

                case "ControlsPanel":
                    break;

                case "GraphicsPanel":
                    Panel.AddComponent<GraphicsSettings>();
                    break;

                case "AudioPanel":
                    break;
            }
        }
        else
        {
            Debug.Log("Panel " + PanelName + " already exists.");
        }
    }

    public void CreateGameplay(int GameplayIndex)
    {
        Panel = GameObject.Find("GamePlayPanel");

        if (Panel == null)
        {
            PanelName = "GamePlayPanel";
            CreatePanel();
        }
    }

    public void RemapAxis()
    {
        axisPresets.Clear();
        //CreateRequiredAxisPresets();
        ImportExistingAxisPresets();
        CreateCompatibilityAxisPresets();

        var inputManagerAsset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
        var serializedObject = new SerializedObject(inputManagerAsset);
        var axisArray = serializedObject.FindProperty("m_Axes");

        axisArray.arraySize = axisPresets.Count;
        serializedObject.ApplyModifiedProperties();

        for (int i = 0; i < axisPresets.Count; i++)
        {
            var axisEntry = axisArray.GetArrayElementAtIndex(i);
            axisPresets[i].ApplyTo(ref axisEntry);
        }

        serializedObject.ApplyModifiedProperties();

        AssetDatabase.Refresh();
    }

    public void CreateControls(int ControlIndex)
    {
        Panel = GameObject.Find("ControlsPanel");

        if (Panel == null)
        {
            PanelName = "ControlsPanel";
            CreatePanel();
        }

        RemapAxis();

        switch (ControlIndex)
        {
            case 0:
                ButtonControlType = Instantiate(ButtonPrefab, transform.position, transform.rotation);
                ButtonControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                ButtonControlType.gameObject.AddComponent<ButtonRemapping>();
                ButtonControlType.gameObject.AddComponent<TTS>();
                ButtonControlType.GetComponent<RectTransform>().anchoredPosition = new Vector2(8.5f, 0);
                ButtonControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                break;

            case 1:
                DropdownControlType = Instantiate(DropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                DropdownControlType.gameObject.AddComponent<DropdownRemapping>();
                DropdownControlType.gameObject.AddComponent<TTS>();
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = new Vector2(8.5f, 0);
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                break;
        }
    }

    public void CreateGraphics(int GraphicsIndex)
    {
        Panel = GameObject.Find("GraphicsPanel");

        Canvas WindowSize;
        WindowSize = FindObjectOfType<Canvas>();
        int i = 0;

        if (Panel == null)
        {
            PanelName = "GraphicsPanel";
            CreatePanel();
        }

        switch (GraphicsIndex)
        {
            case 0:
                DropdownControlType = Instantiate(DropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                DropdownControlType.name = "ResolutionMenu";
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                DropdownControlType.transform.GetChild(3).gameObject.AddComponent<TTS>();
                DropdownControlType.transform.GetChild(3).GetComponent<Text>().text = "Resolution";

                Resolutions = Screen.resolutions;

                if (Resolutions != null)
                {
                    foreach (Resolution resolution in Resolutions)
                    {
                        DropdownControlType.options.Add(new Dropdown.OptionData(resolution.ToString()));
                    }
                }

                while (Screen.currentResolution.width != Resolutions[i].width)
                {
                    i++;
                }

                DropdownControlType.value = i;

                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/ResolutionMenu").GetComponent<RectTransform>().anchorMin = new Vector2(0.008f, 0.942f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/ResolutionMenu").GetComponent<RectTransform>().anchorMax = new Vector2(0.3f, 0.972f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/ResolutionMenu").GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/ResolutionMenu").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;

            case 1:
                ToggleControlType = Instantiate(TogglePrefab, transform.position, transform.rotation);
                ToggleControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                ToggleControlType.name = "FullScreenToggle";
                ToggleControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                ToggleControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                ToggleControlType.gameObject.AddComponent<TTS>();

                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/FullScreenToggle").GetComponent<RectTransform>().anchorMin = new Vector2(0.682f, 0.942f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/FullScreenToggle").GetComponent<RectTransform>().anchorMax = new Vector2(0.975f, 0.972f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/FullScreenToggle").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/FullScreenToggle").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/FullScreenToggle").GetChild(1).GetComponent<Text>().text = "Full Screen";
                break;

            case 2:
                DropdownControlType = Instantiate(DropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                DropdownControlType.name = "TextureQuality";
                DropdownControlType.options.Add(new Dropdown.OptionData("Low"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Medium"));
                DropdownControlType.options.Add(new Dropdown.OptionData("High"));
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                DropdownControlType.transform.GetChild(3).gameObject.AddComponent<TTS>();
                DropdownControlType.transform.GetChild(3).GetComponent<Text>().text = "TextureQuality";

                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/TextureQuality").GetComponent<RectTransform>().anchorMin = new Vector2(0.008f, 0.892f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/TextureQuality").GetComponent<RectTransform>().anchorMax = new Vector2(0.3f, 0.923f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/TextureQuality").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/TextureQuality").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;

            case 3:
                DropdownControlType = Instantiate(DropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                DropdownControlType.name = "VSync";
                DropdownControlType.options.Add(new Dropdown.OptionData("Don't Sync"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Every V Blank"));
                DropdownControlType.options.Add(new Dropdown.OptionData("Every Second V Blank"));
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                DropdownControlType.transform.GetChild(3).gameObject.AddComponent<TTS>();
                DropdownControlType.transform.GetChild(3).GetComponent<Text>().text = "V Sync";

                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/VSync").GetComponent<RectTransform>().anchorMin = new Vector2(0.682f, 0.892f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/VSync").GetComponent<RectTransform>().anchorMax = new Vector2(0.975f, 0.923f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/VSync").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/VSync").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;

            case 4:
                DropdownControlType = Instantiate(DropdownPrefab, transform.position, transform.rotation);
                DropdownControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                DropdownControlType.name = "AntiAliasing";
                DropdownControlType.options.Add(new Dropdown.OptionData("0x"));
                DropdownControlType.options.Add(new Dropdown.OptionData("2x"));
                DropdownControlType.options.Add(new Dropdown.OptionData("4x"));
                DropdownControlType.options.Add(new Dropdown.OptionData("8x"));
                DropdownControlType.options.Add(new Dropdown.OptionData("16x"));
                DropdownControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                DropdownControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                DropdownControlType.transform.GetChild(3).gameObject.AddComponent<TTS>();
                DropdownControlType.transform.GetChild(3).GetComponent<Text>().text = "AntiAliasing";

                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/AntiAliasing").GetComponent<RectTransform>().anchorMin = new Vector2(0.008f, 0.845f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/AntiAliasing").GetComponent<RectTransform>().anchorMax = new Vector2(0.3f, 0.875f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/AntiAliasing").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/AntiAliasing").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;

            case 5:
                SliderControlType = Instantiate(SliderPrefab, transform.position, transform.rotation);
                SliderControlType.transform.SetParent(Panel.transform.GetChild(0).GetChild(0).GetChild(0));
                SliderControlType.name = "GammaCorrection";
                SliderControlType.transform.GetChild(3).GetComponent<Text>().text = "Gamma Correction";
                SliderControlType.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                SliderControlType.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 30);
                SliderControlType.gameObject.AddComponent<TTS>();

                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/GammaCorrection").GetComponent<RectTransform>().anchorMin = new Vector2(0.682f, 0.85f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/GammaCorrection").GetComponent<RectTransform>().anchorMax = new Vector2(0.975f, 0.87f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/GammaCorrection").GetComponent<RectTransform>().sizeDelta = new Vector2(0.0f, 0.0f);
                WindowSize.gameObject.transform.Find("GraphicsPanel/Scroll View/Viewport/Content/GammaCorrection").GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
        }
    }

    public void CreateAudio(int AudioIndex)
    {
        Panel = GameObject.Find("AudioPanel");

        if (Panel == null)
        {
            PanelName = "AudioPanel";
            CreatePanel();
        }
    }

    internal class AxisPreset
    {
        public string name;
        public string descriptiveName;
        public string descriptiveNegativeName;
        public string negativeButton;
        public string positiveButton;
        public string altNegativeButton;
        public string altPositiveButton;
        public float gravity;
        public float deadZone = 0.001f;
        public float sensitivity = 1.0f;
        public bool snap;
        public bool invert;
        public int type;
        public int axis;
        public int joyNum;

        public AxisPreset()
        {
        }


        public AxisPreset(SerializedProperty axisPreset)
        {
            this.name = GetChildProperty(axisPreset, "m_Name").stringValue;
            this.descriptiveName = GetChildProperty(axisPreset, "descriptiveName").stringValue;
            this.descriptiveNegativeName = GetChildProperty(axisPreset, "descriptiveNegativeName").stringValue;
            this.negativeButton = GetChildProperty(axisPreset, "negativeButton").stringValue;
            this.positiveButton = GetChildProperty(axisPreset, "positiveButton").stringValue;
            this.altNegativeButton = GetChildProperty(axisPreset, "altNegativeButton").stringValue;
            this.altPositiveButton = GetChildProperty(axisPreset, "altPositiveButton").stringValue;
            this.gravity = GetChildProperty(axisPreset, "gravity").floatValue;
            this.deadZone = GetChildProperty(axisPreset, "dead").floatValue;
            this.sensitivity = GetChildProperty(axisPreset, "sensitivity").floatValue;
            this.snap = GetChildProperty(axisPreset, "snap").boolValue;
            this.invert = GetChildProperty(axisPreset, "invert").boolValue;
            this.type = GetChildProperty(axisPreset, "type").intValue;
            this.axis = GetChildProperty(axisPreset, "axis").intValue;
            this.joyNum = GetChildProperty(axisPreset, "joyNum").intValue;
        }

        public AxisPreset(string name, int type, int axis, float sensitivity)
        {
            this.name = name;
            this.descriptiveName = "";
            this.descriptiveNegativeName = "";
            this.negativeButton = "";
            this.positiveButton = "";
            this.altNegativeButton = "";
            this.altPositiveButton = "";
            this.gravity = 0.0f;
            this.deadZone = 0.001f;
            this.sensitivity = sensitivity;
            this.snap = false;
            this.invert = false;
            this.type = type;
            this.axis = axis;
            this.joyNum = 0;
        }

        public AxisPreset(int device, int analog)
        {
            this.name = string.Format("joystick {0} analog {1}", device, analog);
            this.descriptiveName = "";
            this.descriptiveNegativeName = "";
            this.negativeButton = "";
            this.positiveButton = "";
            this.altNegativeButton = "";
            this.altPositiveButton = "";
            this.gravity = 0.0f;
            this.deadZone = 0.001f;
            this.sensitivity = 1.0f;
            this.snap = false;
            this.invert = false;
            this.type = 2;
            this.axis = analog;
            this.joyNum = device;
        }


        public bool ReservedName
        {
            get
            {
                if (Regex.Match(name, @"^joystick \d+ analog \d+$").Success ||
                    Regex.Match(name, @"^mouse (x|y|z)$").Success)
                {
                    return true;
                }

                return false;
            }
        }

        public void ApplyTo(ref SerializedProperty axisPreset)
        {
            GetChildProperty(axisPreset, "m_Name").stringValue = name;
            GetChildProperty(axisPreset, "descriptiveName").stringValue = descriptiveName;
            GetChildProperty(axisPreset, "descriptiveNegativeName").stringValue = descriptiveNegativeName;
            GetChildProperty(axisPreset, "negativeButton").stringValue = negativeButton;
            GetChildProperty(axisPreset, "positiveButton").stringValue = positiveButton;
            GetChildProperty(axisPreset, "altNegativeButton").stringValue = altNegativeButton;
            GetChildProperty(axisPreset, "altPositiveButton").stringValue = altPositiveButton;
            GetChildProperty(axisPreset, "gravity").floatValue = gravity;
            GetChildProperty(axisPreset, "dead").floatValue = deadZone;
            GetChildProperty(axisPreset, "sensitivity").floatValue = sensitivity;
            GetChildProperty(axisPreset, "snap").boolValue = snap;
            GetChildProperty(axisPreset, "invert").boolValue = invert;
            GetChildProperty(axisPreset, "type").intValue = type;
            GetChildProperty(axisPreset, "axis").intValue = axis;
            GetChildProperty(axisPreset, "joyNum").intValue = joyNum;
        }
    }

    static SerializedProperty GetChildProperty(SerializedProperty parent, string name)
    {
        var child = parent.Copy();
        child.Next(true);

        do
        {
            if (child.name == name)
            {
                return child;
            }
        } while (child.Next(false));

        return null;
    }

    static void CreateCompatibilityAxisPresets()
    {
        if (!HasAxisPreset("Mouse ScrollWheel"))
        {
            axisPresets.Add(new AxisPreset("Mouse ScrollWheel", 1, 2, 0.1f));
        }

        if (!HasAxisPreset("Horizontal"))
        {
            axisPresets.Add(new AxisPreset()
            {
                name = "Horizontal",
                negativeButton = "left",
                positiveButton = "right",
                altNegativeButton = "a",
                altPositiveButton = "d",
                gravity = 3.0f,
                deadZone = 0.001f,
                sensitivity = 3.0f,
                snap = true,
                type = 0,
                axis = 0,
                joyNum = 0
            });

            axisPresets.Add(new AxisPreset()
            {
                name = "Horizontal",
                gravity = 0.0f,
                deadZone = 0.19f,
                sensitivity = 1.0f,
                type = 2,
                axis = 0,
                joyNum = 0
            });
        }

        if (!HasAxisPreset("Vertical"))
        {
            axisPresets.Add(new AxisPreset()
            {
                name = "Vertical",
                negativeButton = "down",
                positiveButton = "up",
                altNegativeButton = "s",
                altPositiveButton = "w",
                gravity = 3.0f,
                deadZone = 0.001f,
                sensitivity = 3.0f,
                snap = true,
                type = 0,
                axis = 0,
                joyNum = 0
            });

            axisPresets.Add(new AxisPreset()
            {
                name = "Vertical",
                gravity = 0.0f,
                deadZone = 0.19f,
                sensitivity = 1.0f,
                type = 2,
                axis = 1,
                invert = true,
                joyNum = 0
            });
        }

        if (!HasAxisPreset("Fire1"))
        {
            axisPresets.Add(new AxisPreset()
            {
                name = "Fire1",
                negativeButton = "",
                positiveButton = "left ctrl",
                altNegativeButton = "",
                altPositiveButton = "mouse 0",
                gravity = 1000.0f,
                deadZone = 0.001f,
                sensitivity = 1000.0f,
                snap = false,
                type = 0,
                axis = 0,
                joyNum = 0
            });

            axisPresets.Add(new AxisPreset()
            {
                name = "Fire1",
                negativeButton = "",
                positiveButton = "joystick button 0",
                altNegativeButton = "",
                altPositiveButton = "",
                gravity = 1000.0f,
                deadZone = 0.001f,
                sensitivity = 1000.0f,
                snap = false,
                type = 0,
                axis = 0,
                joyNum = 0
            });
        }

        if (!HasAxisPreset("Fire2"))
        {
            axisPresets.Add(new AxisPreset()
            {
                name = "Fire2",
                negativeButton = "",
                positiveButton = "left alt",
                altNegativeButton = "",
                altPositiveButton = "mouse 1",
                gravity = 1000.0f,
                deadZone = 0.001f,
                sensitivity = 1000.0f,
                snap = false,
                type = 0,
                axis = 0,
                joyNum = 0
            });

            axisPresets.Add(new AxisPreset()
            {
                name = "Fire2",
                negativeButton = "",
                positiveButton = "joystick button 1",
                altNegativeButton = "",
                altPositiveButton = "",
                gravity = 1000.0f,
                deadZone = 0.001f,
                sensitivity = 1000.0f,
                snap = false,
                type = 0,
                axis = 0,
                joyNum = 0
            });
        }

        if (!HasAxisPreset("Fire3"))
        {
            axisPresets.Add(new AxisPreset()
            {
                name = "Fire3",
                negativeButton = "",
                positiveButton = "left shift",
                altNegativeButton = "",
                altPositiveButton = "mouse 2",
                gravity = 1000.0f,
                deadZone = 0.001f,
                sensitivity = 1000.0f,
                snap = false,
                type = 0,
                axis = 0,
                joyNum = 0
            });

            axisPresets.Add(new AxisPreset()
            {
                name = "Fire3",
                negativeButton = "",
                positiveButton = "joystick button 2",
                altNegativeButton = "",
                altPositiveButton = "",
                gravity = 1000.0f,
                deadZone = 0.001f,
                sensitivity = 1000.0f,
                snap = false,
                type = 0,
                axis = 0,
                joyNum = 0
            });
        }

        if (!HasAxisPreset("Jump"))
        {
            axisPresets.Add(new AxisPreset()
            {
                name = "Jump",
                negativeButton = "",
                positiveButton = "space",
                altNegativeButton = "",
                altPositiveButton = "",
                gravity = 1000.0f,
                deadZone = 0.001f,
                sensitivity = 1000.0f,
                snap = false,
                type = 0,
                axis = 0,
                joyNum = 0
            });

            axisPresets.Add(new AxisPreset()
            {
                name = "Jump",
                negativeButton = "",
                positiveButton = "joystick button 3",
                altNegativeButton = "",
                altPositiveButton = "",
                gravity = 1000.0f,
                deadZone = 0.001f,
                sensitivity = 1000.0f,
                snap = false,
                type = 0,
                axis = 0,
                joyNum = 0
            });
        }

        if (!HasAxisPreset("Mouse X"))
        {
            axisPresets.Add(new AxisPreset()
            {
                name = "Mouse X",
                negativeButton = "",
                positiveButton = "",
                altNegativeButton = "",
                altPositiveButton = "",
                gravity = 0.0f,
                deadZone = 0.0f,
                sensitivity = 0.1f,
                snap = false,
                type = 0,
                axis = 0,
                joyNum = 0
            });
        }

        if (!HasAxisPreset("Mouse Y"))
        {
            axisPresets.Add(new AxisPreset()
            {
                name = "Mouse Y",
                negativeButton = "",
                positiveButton = "",
                altNegativeButton = "",
                altPositiveButton = "",
                gravity = 0.0f,
                deadZone = 0.0f,
                sensitivity = 0.1f,
                snap = false,
                type = 0,
                axis = 1,
                joyNum = 0
            });
        }

        if (!HasAxisPreset("Submit"))
        {
            axisPresets.Add(new AxisPreset()
            {
                name = "Submit",
                positiveButton = "return",
                altPositiveButton = "joystick button 0",
                gravity = 1000.0f,
                deadZone = 0.001f,
                sensitivity = 1000.0f,
                type = 0,
                axis = 0,
                joyNum = 0
            });

            axisPresets.Add(new AxisPreset()
            {
                name = "Submit",
                positiveButton = "enter",
                altPositiveButton = "space",
                gravity = 1000.0f,
                deadZone = 0.001f,
                sensitivity = 1000.0f,
                type = 0,
                axis = 0,
                joyNum = 0
            });
        }

        if (!HasAxisPreset("Cancel"))
        {
            axisPresets.Add(new AxisPreset()
            {
                name = "Cancel",
                positiveButton = "escape",
                altPositiveButton = "joystick button 1",
                gravity = 1000.0f,
                deadZone = 0.001f,
                sensitivity = 1000.0f,
                type = 0,
                axis = 0,
                joyNum = 0
            });
        }

        if (!HasAxisPreset("Left Trigger"))
        {
            axisPresets.Add(new AxisPreset()
            {
                name = "Left Trigger",
                positiveButton = "",
                altPositiveButton = "",
                gravity = 1000.0f,
                deadZone = 0.001f,
                sensitivity = 1000.0f,
                type = 2,
                axis = 8,
                joyNum = 0
            });
        }

        if (!HasAxisPreset("Right Trigger"))
        {
            axisPresets.Add(new AxisPreset()
            {
                name = "Right Trigger",
                positiveButton = "",
                altPositiveButton = "",
                gravity = 1000.0f,
                deadZone = 0.001f,
                sensitivity = 1000.0f,
                type = 2,
                axis = 9,
                joyNum = 0
            });
        }

        if (!HasAxisPreset("DPad Horizontal"))
        {
            axisPresets.Add(new AxisPreset()
            {
                name = "DPad Horizontal",
                positiveButton = "",
                altPositiveButton = "",
                gravity = 1000.0f,
                deadZone = 0.001f,
                sensitivity = 1000.0f,
                type = 2,
                axis = 5,
                joyNum = 0
            });
        }

        if (!HasAxisPreset("DPad Vertical"))
        {
            axisPresets.Add(new AxisPreset()
            {
                name = "DPad Vertical",
                positiveButton = "",
                altPositiveButton = "",
                gravity = 1000.0f,
                deadZone = 0.001f,
                sensitivity = 1000.0f,
                type = 2,
                axis = 6,
                joyNum = 0
            });
        }

        if (AxisName != null)
        {
            if (!HasAxisPreset(AxisName))
            {
                axisPresets.Add(new AxisPreset()
                {
                    name = AxisName,
                    negativeButton = NegativeButton,
                    positiveButton = PositiveButton,
                    altNegativeButton = AltNegativeButton,
                    altPositiveButton = AltPositiveButton,
                    gravity = AxisGravity,
                    deadZone = AxisDeadZone,
                    sensitivity = AxisSensitivity,
                    snap = AxisSnap,
                    type = ControlType,
                    axis = AxisType,
                    joyNum = JoyNum
                });
            }
        }
    }

    static SerializedProperty GetInputManagerAxisArray()
    {
        var inputManagerAsset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
        var serializedObject = new SerializedObject(inputManagerAsset);
        return serializedObject.FindProperty("m_Axes");
    }

    static bool HasAxisPreset(string name)
    {
        for (int i = 0; i < axisPresets.Count; i++)
        {
            if (axisPresets[i].name == name)
            {
                return true;
            }
        }

        return false;
    }

    static void CreateRequiredAxisPresets()
    {
        for (int device = 1; device <= 10; device++)
        {
            for (int analog = 0; analog < 20; analog++)
            {
                axisPresets.Add(new AxisPreset(device, analog));
            }
        }
    }

    static void ImportExistingAxisPresets()
    {
        var axisArray = GetInputManagerAxisArray();
        for (int i = 0; i < axisArray.arraySize; i++)
        {
            var axisEntry = axisArray.GetArrayElementAtIndex(i);
            var axisPreset = new AxisPreset(axisEntry);
            if (!axisPreset.ReservedName)
            {
                axisPresets.Add(axisPreset);
            }
        }
    }

    public void Speak(string text)
    {
        Voice.Speak(text);
    }
}