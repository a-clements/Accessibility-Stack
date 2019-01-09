using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections.Generic;

[CustomEditor(typeof(AccessibilityManager))] //declares this script as a custom editor of type AccessibilityManager, which is a script.
public class AccessibilityManagerEditor : Editor //derives from the Editor class.
{
    private string PanelName;
    private int PanelNumber;
    private int GameplayIndex = 0;
    private int ControlIndex = 0;
    private int GraphicsIndex = 0;
    private int AudioIndex = 0;
    private static int ControlTypeIndex = 0;
    private static int AxisTypeIndex = 0;
    private static int JoyNumIndex = 0;

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

    static readonly List<AxisPreset> axisPresets = new List<AxisPreset>();

    public override void OnInspectorGUI() //this line overrides the base implementation of OnInspectorGui()
    {
        AccessibilityManager Manager = (AccessibilityManager)target; //sets the target of this script be the AccessibilityManager script

        GUILayout.BeginVertical(); //ensures that all controls are under each other vertically

        /*The beginning of code that lays out buttons to modify and call the CreatePanel function*/
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1)); //creates a line in the inspector

        EditorGUILayout.LabelField("Panels", EditorStyles.boldLabel); //creates a label in the inspector with a bold font

        PanelNumber = GUILayout.Toolbar(PanelNumber, new string[] { "GamePlay", "Controls", "Graphics", "Audio" }); //this line creates a toolbar and places the enum in the variable

        /*This switch statement changes the caption on the create panel button so that the user knows exactly which panel is being created. It does so by taking the PanelNumber value*/
        /* and changing the PanelName depending on the value passed into the switch both locally and in the AccessibilityManager script. The reason for changing the PanelName in the */
        /*AccessibilityManager script is so that each panel is named correctly. This is important for the layout of each type of control so that it is parented to the correct panel. 0*/
        switch (PanelNumber)
        {
            case 0:
                Manager.PanelName = "GamePlayPanel";
                PanelName = "Game";
                break;
            case 1:
                Manager.PanelName = "ControlsPanel";
                PanelName = "Controls";
                break;
            case 2:
                Manager.PanelName = "GraphicsPanel";
                PanelName = "Graphics";
                break;
            case 3:
                Manager.PanelName = "AudioPanel";
                PanelName = "Audio";
                break;
        }

        if (GUILayout.Button("Create " + PanelName + " Panel")) //creates a button whose caption changes depending on the enum of the toolbar
        {
            Manager.CreatePanel(); //makes a call to the AccessibilityManager script
        }

        GUILayout.EndVertical(); //closes off the vertical command
                                 /*The end of the panels code*/

        /*This is the beginning of the gameplay code which modifies and calls the CreateGamePlay function*/
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

        EditorGUILayout.LabelField("Game Play", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal(); //this line starts an area where the buttons are layed out horizontally

        string[] GameplayOptions = new[] { "Button", "Dropdown" }; //this creates an enum list
        GameplayIndex = EditorGUILayout.Popup(GameplayIndex, GameplayOptions, GUILayout.MaxWidth(135)); //this creates a dropdown menu in the inspector with the enum values
        
        if (GUILayout.Button("Create Gameplay " + GameplayOptions[GameplayIndex])) //this line creates a button with the caption changing with the enum value
        {
            Manager.CreateGameplay(GameplayIndex);
        }

        GUILayout.EndHorizontal(); //this line ends the horizontal layout
                                   /*This is the end of the gameplay settings*/

        /*This is the beginning of the remap axis code which calls the ReMapAxis function*/
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

        EditorGUILayout.LabelField("Remap Axis", EditorStyles.boldLabel);

        GUILayout.BeginVertical();

        AxisName = EditorGUILayout.TextField("Name ", AxisName);
        DescriptiveAxisName = EditorGUILayout.TextField("Descriptive Name ", DescriptiveAxisName);
        DescriptiveNegativeAxisName = EditorGUILayout.TextField("Descriptive Negative Name ", DescriptiveNegativeAxisName);
        NegativeButton = EditorGUILayout.TextField("Negative Button ", NegativeButton);
        PositiveButton = EditorGUILayout.TextField("Positive Button ", PositiveButton);
        AltNegativeButton = EditorGUILayout.TextField("Alt Negative Button ", AltNegativeButton);
        AltPositiveButton = EditorGUILayout.TextField("Alt Negative Button ", AltPositiveButton);
        AxisGravity = EditorGUILayout.FloatField("Gravity ", AxisGravity);
        AxisDeadZone = EditorGUILayout.FloatField("DeadZone ", AxisDeadZone);
        AxisSensitivity = EditorGUILayout.FloatField("Sensetivity ", AxisSensitivity);
        AxisSnap = EditorGUILayout.Toggle("Snap ", AxisSnap);
        AxisInvert = EditorGUILayout.Toggle("Invert ", AxisInvert);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Type", GUILayout.MaxWidth(135));
        string[] ControlType = new[] { "Key or Mouse Button", "Mouse Movement", "Joystick Axis" }; //this creates an enum list
        ControlTypeIndex = EditorGUILayout.Popup(ControlTypeIndex, ControlType); //this creates a dropdown menu in the inspector with the enum values

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Axis", GUILayout.MaxWidth(135));
        string[] AxisType = new[] { "X axis", "Y axis", "3rd axis (Joysticks and Scrollwheel", "4th axis (Joysticks)",  "5th axis (Joysticks)",
        "6th axis (Joysticks)", "7th axis (Joysticks)", "8th axis (Joysticks)", "9th axis (Joysticks)", "10th axis (Joysticks)", "11th axis (Joysticks)",
        "12th axis (Joysticks)", "13th axis (Joysticks)", "14th axis (Joysticks)", "15th axis (Joysticks)", "16th axis (Joysticks)", "17th axis (Joysticks)",
        "18th axis (Joysticks)", "19th axis (Joysticks)", "20th axis (Joysticks)", "21th axis (Joysticks)", "22th axis (Joysticks)", "23th axis (Joysticks)",
        "24th axis (Joysticks)", "25th axis (Joysticks)", "26th axis (Joysticks)", "27th axis (Joysticks)", "28th axis (Joysticks)" }; //this creates an enum list
        AxisTypeIndex = EditorGUILayout.Popup(AxisTypeIndex, AxisType); //this creates a dropdown menu in the inspector with the enum values

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Joy Num", GUILayout.MaxWidth(135));
        string[] JoyNum = new[] { "Get Motion from all Joysticks", "Joystick 1", "Joystick 2", "Joystick 3", "Joystick 4", "Joystick 5", "Joystick 6", "Joystick 7",
        "Joystick 8", "Joystick 9", "Joystick 10", "Joystick 11", "Joystick 12", "Joystick 13", "Joystick 14", "Joystick 15", "Joystick 16" }; //this creates an enum list
        JoyNumIndex = EditorGUILayout.Popup(JoyNumIndex, JoyNum); //this creates an enum list

        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Remap InputManager Axis")) //creates a button whose caption changes depending on the enum of the toolbar
        {
            RemapAxis(); //makes a call to the AccessibilityManager script
            AxisName = "";
            DescriptiveAxisName = "";
            DescriptiveNegativeAxisName = "";
            NegativeButton = "";
            PositiveButton = "";
            AltNegativeButton = "";
            AltPositiveButton = "";
            AxisGravity = 0;
            AxisDeadZone = 0.001f;
            AxisSensitivity = 1;
            AxisSnap = false;
            AxisInvert = false;
            ControlType = new[] { "Key or Mouse Button", "Mouse Movement", "Joystick Axis" }; //this creates an enum list
            ControlTypeIndex = EditorGUILayout.Popup(0, ControlType); //this creates a dropdown menu in the inspector with the enum values
            AxisType = new[] { "X axis", "Y axis", "3rd axis (Joysticks and Scrollwheel", "4th axis (Joysticks)",  "5th axis (Joysticks)",
            "6th axis (Joysticks)", "7th axis (Joysticks)", "8th axis (Joysticks)", "9th axis (Joysticks)", "10th axis (Joysticks)", "11th axis (Joysticks)",
            "12th axis (Joysticks)", "13th axis (Joysticks)", "14th axis (Joysticks)", "15th axis (Joysticks)", "16th axis (Joysticks)", "17th axis (Joysticks)",
            "18th axis (Joysticks)", "19th axis (Joysticks)", "20th axis (Joysticks)", "21th axis (Joysticks)", "22th axis (Joysticks)", "23th axis (Joysticks)",
            "24th axis (Joysticks)", "25th axis (Joysticks)", "26th axis (Joysticks)", "27th axis (Joysticks)", "28th axis (Joysticks)" }; //this creates an enum list
            AxisTypeIndex = EditorGUILayout.Popup(0, AxisType); //this creates a dropdown menu in the inspector with the enum values
            JoyNum = new[] { "Get Motion from all Joysticks", "Joystick 1", "Joystick 2", "Joystick 3", "Joystick 4", "Joystick 5", "Joystick 6", "Joystick 7",
            "Joystick 8", "Joystick 9", "Joystick 10", "Joystick 11", "Joystick 12", "Joystick 13", "Joystick 14", "Joystick 15", "Joystick 16" }; //this creates an enum list
            JoyNumIndex = EditorGUILayout.Popup(0, JoyNum); //this creates an enum list
        }

        GUILayout.EndVertical();
        /*This is the end of the remap axis */

        /*This is the beginning of the controls code which modifies and calls the CreateControls function*/
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

        EditorGUILayout.LabelField("Controls", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();

        string[] ControlOptions = new[] { "Button", "Dropdown" };

        ControlIndex = EditorGUILayout.Popup(ControlIndex, ControlOptions, GUILayout.MaxWidth(135));

        if (GUILayout.Button("Create Controls " + ControlOptions[ControlIndex]))
        {
            Manager.CreateControls(ControlIndex);
        }

        GUILayout.EndHorizontal();
        /*This is the end of the Controls settings*/

        /*This is the beginning of the graphics code which modifies and calls the CreateGraphics function*/
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

        EditorGUILayout.LabelField("Graphics", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();

        string[] GraphicsOptions = new[] { "Resolution", "Full Screen", "Texture Quality", "VSync", "Anti Aliasing", "Gamma Correction" };

        GraphicsIndex = EditorGUILayout.Popup(GraphicsIndex, GraphicsOptions, GUILayout.MaxWidth(135));

        if (GUILayout.Button("Create Graphics " + GraphicsOptions[GraphicsIndex]))
        {
            Manager.CreateGraphics(GraphicsIndex);
        }

        GUILayout.EndHorizontal();
        /*This is the end of the Grapics settings*/

        /*This is the beginning of the audio code which modifies and calls the CreateAudio function*/
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

        EditorGUILayout.LabelField("Audio", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();

        string[] AudioOptions = new[] { "Button", "Dropdown" };

        AudioIndex = EditorGUILayout.Popup(AudioIndex, AudioOptions, GUILayout.MaxWidth(135));

        if (GUILayout.Button("Create Audio " + AudioOptions[AudioIndex]))
        {
            Manager.CreateAudio(AudioIndex);
        }

        GUILayout.EndHorizontal();
        /*This is the end of the audio settings*/

        base.OnInspectorGUI(); //this line of code gives this script access to the base functionality of OnInspectorGui().
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
                    invert = AxisInvert,
                    type = ControlTypeIndex,
                    axis = AxisTypeIndex,
                    joyNum = JoyNumIndex
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
}