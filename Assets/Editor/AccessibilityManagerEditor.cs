using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AccessibilityManager))] //declares this script as a custom editor of type AccessibilityManager, which is a script.
public class AccessibilityManagerEditor : Editor //derives from the Editor class.
{
    private string PanelName;
    private int PanelNumber;
    private int GameplayIndex = 0;
    private int ControlIndex = 0;
    private int GraphicsIndex = 0;
    private int AudioIndex = 0;
    private int ControlTypeIndex = 0;
    private int AxisTypeIndex = 0;
    private int JoyNumIndex = 0;

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

        GameplayIndex = EditorGUILayout.Popup(GameplayIndex, GameplayOptions); //this creates a dropdown menu in the inspector with the enum values

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

        AccessibilityManager.AxisName = EditorGUILayout.TextField("Name ", AccessibilityManager.AxisName);
        AccessibilityManager.DescriptiveAxisName = EditorGUILayout.TextField("Descriptive Name ", AccessibilityManager.DescriptiveAxisName);
        AccessibilityManager.DescriptiveNegativeAxisName = EditorGUILayout.TextField("Descriptive Negative Name ", AccessibilityManager.DescriptiveNegativeAxisName);
        AccessibilityManager.NegativeButton = EditorGUILayout.TextField("Negative Button ", AccessibilityManager.NegativeButton);
        AccessibilityManager.PositiveButton = EditorGUILayout.TextField("Positive Button ", AccessibilityManager.PositiveButton);
        AccessibilityManager.AltNegativeButton = EditorGUILayout.TextField("Alt Negative Button ", AccessibilityManager.AltNegativeButton);
        AccessibilityManager.AltPositiveButton = EditorGUILayout.TextField("Alt Negative Butto: ", AccessibilityManager.AltPositiveButton);
        AccessibilityManager.AxisGravity = EditorGUILayout.FloatField("Gravit: ", AccessibilityManager.AxisGravity);
        AccessibilityManager.AxisDeadZone = EditorGUILayout.FloatField("DeadZone ", AccessibilityManager.AxisDeadZone);
        AccessibilityManager.AxisSensitivity = EditorGUILayout.FloatField("Sensetivity ", AccessibilityManager.AxisSensitivity);
        AccessibilityManager.AxisSnap = EditorGUILayout.Toggle("Snap: ", false);
        AccessibilityManager.AxisInvert = EditorGUILayout.Toggle("Invert: ", false);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Type", GUILayout.MaxWidth(135));
        string[] ControlType = new[] { "Key or Mouse Button", "Mouse Movement", "Joystick Axis" }; //this creates an enum list
        AccessibilityManager.ControlType = EditorGUILayout.Popup(ControlTypeIndex, ControlType); //this creates a dropdown menu in the inspector with the enum values

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Axis", GUILayout.MaxWidth(135));
        string[] AxisType = new[] { "X axis", "Y axis", "3rd axis (Joysticks and Scrollwheel", "4th axis (Joysticks)",  "5th axis (Joysticks)",
        "6th axis (Joysticks)", "7th axis (Joysticks)", "8th axis (Joysticks)", "9th axis (Joysticks)", "10th axis (Joysticks)", "11th axis (Joysticks)",
        "12th axis (Joysticks)", "13th axis (Joysticks)", "14th axis (Joysticks)", "15th axis (Joysticks)", "16th axis (Joysticks)", "17th axis (Joysticks)",
        "18th axis (Joysticks)", "19th axis (Joysticks)", "20th axis (Joysticks)", "21th axis (Joysticks)", "22th axis (Joysticks)", "23th axis (Joysticks)",
        "24th axis (Joysticks)", "25th axis (Joysticks)", "26th axis (Joysticks)", "27th axis (Joysticks)", "28th axis (Joysticks)" }; //this creates an enum list
        AccessibilityManager.AxisType = EditorGUILayout.Popup(AxisTypeIndex, AxisType); //this creates a dropdown menu in the inspector with the enum values

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Joy Num", GUILayout.MaxWidth(135));
        string[] JoyNum = new[] { "Get Motion from all Joysticks", "Joystick 1", "Joystick 2", "Joystick 3", "Joystick 4", "Joystick 5", "Joystick 6", "Joystick 7",
        "Joystick 8", "Joystick 9", "Joystick 10", "Joystick 11", "Joystick 12", "Joystick 13", "Joystick 14", "Joystick 15", "Joystick 16" }; //this creates an enum list
        AccessibilityManager.JoyNum = EditorGUILayout.Popup(JoyNumIndex, JoyNum); //this creates an enum list

        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Remap InputManager Axis")) //creates a button whose caption changes depending on the enum of the toolbar
        {
            Manager.RemapAxis(); //makes a call to the AccessibilityManager script
        }

        GUILayout.EndVertical();
        /*This is the end of the remap axis */

        /*This is the beginning of the controls code which modifies and calls the CreateControls function*/
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

        EditorGUILayout.LabelField("Controls", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();

        string[] ControlOptions = new[] { "Button", "Dropdown" };

        ControlIndex = EditorGUILayout.Popup(ControlIndex, ControlOptions);

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

        GraphicsIndex = EditorGUILayout.Popup(GraphicsIndex, GraphicsOptions);

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

        AudioIndex = EditorGUILayout.Popup(AudioIndex, AudioOptions);

        if (GUILayout.Button("Create Audio " + AudioOptions[AudioIndex]))
        {
            Manager.CreateAudio(AudioIndex);
        }

        GUILayout.EndHorizontal();
        /*This is the end of the audio settings*/

        base.OnInspectorGUI(); //this line of code gives this script access to the base functionality of OnInspectorGui().
    }
}