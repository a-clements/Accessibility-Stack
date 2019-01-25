using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections.Generic;

[CustomEditor(typeof(UIManager))] //declares this script as a custom editor of type AccessibilityManager, which is a script.
public class UIManagerEditor : Editor //derives from the Editor class.
{
    private string PanelName;
    private int PanelNumber;
    private int GameplayIndex = 0;
    private int ControlIndex = 0;
    private int GraphicsIndex = 0;
    private int AudioIndex = 0;

    public override void OnInspectorGUI() //this line overrides the base implementation of OnInspectorGui()
    {
        UIManager Manager = (UIManager)target; //sets the target of this script be the AccessibilityManager script

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

        string[] GraphicsOptions = new[] { "Resolution", "Full Screen", "Texture Quality", "VSync", "Anti Aliasing", "Gamma Correction", "Main Colour", "Secondary Colour" };

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
}