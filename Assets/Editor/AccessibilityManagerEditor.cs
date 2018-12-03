﻿using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AccessibilityManager))]
public class AccessibilityManagerEditor : Editor
{
    private string PanelName;
    public int GameplayIndex = 0;
    public int ControlIndex = 0;
    public int GraphicsIndex = 0;
    public int AudioIndex = 0;

    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public override void OnInspectorGUI()
    {
        AccessibilityManager Manager = (AccessibilityManager)target; //sets the target of this script be the AccessibilityManager script

        GUILayout.BeginVertical(); //ensures that all controls are under each other vertically

/*The beginning of the Panels code*/
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1)); //creates a line in the inspector

        EditorGUILayout.LabelField("Panels", EditorStyles.boldLabel); //creates a label in the inspector with a bold font

        Manager.PanelNumber = GUILayout.Toolbar(Manager.PanelNumber, new string[] { "GamePlay", "Controls", "Graphics", "Audio" }); //this line creates a toolbar and places the enum in the variable

        switch (Manager.PanelNumber)
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
/*The end of the panels code*/

        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

        EditorGUILayout.LabelField("Game Play", EditorStyles.boldLabel);

        string[] GameplayOptions = new[] { "Button", "Dropdown" }; //this creates an enum list

        GameplayIndex = EditorGUILayout.Popup(GameplayIndex, GameplayOptions); //this creates a dropdown menu in the inspector with the enum values

        if (GUILayout.Button("Create Gameplay " + GameplayOptions[GameplayIndex])) //this line creates a button with the caption changing with the enum value
        {
            Manager.CreateGameplayButton();
        }

        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

        EditorGUILayout.LabelField("Controls", EditorStyles.boldLabel);

        string[] ControlOptions = new[] { "Button", "Dropdown" };

        ControlIndex = EditorGUILayout.Popup(ControlIndex, ControlOptions);

        if (GUILayout.Button("Create Controls " + ControlOptions[ControlIndex]))
        {
            Manager.CreateControlsButton();
        }

        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

        EditorGUILayout.LabelField("Graphics", EditorStyles.boldLabel);

        string[] GraphicsOptions = new[] { "Button", "Dropdown" };

        GraphicsIndex = EditorGUILayout.Popup(GraphicsIndex, GraphicsOptions);

        if (GUILayout.Button("Create Graphics " + GraphicsOptions[GraphicsIndex]))
        {
            Manager.CreateGraphicsButton();
        }

        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

        EditorGUILayout.LabelField("Audio", EditorStyles.boldLabel);

        string[] AudioOptions = new[] { "Button", "Dropdown" };

        AudioIndex = EditorGUILayout.Popup(AudioIndex, AudioOptions);

        if (GUILayout.Button("Create Audio " + AudioOptions[AudioIndex]))
        {
            Manager.CreateAudioButton();
        }

        GUILayout.EndVertical(); //closes off the vertical command

        base.OnInspectorGUI();
    }
}
