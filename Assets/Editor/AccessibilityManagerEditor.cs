using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AccessibilityManager))]
public class AccessibilityManagerEditor : Editor
{
    private string PanelName;
    private int PanelNumber;
    private int GameplayIndex = 0;
    private int ControlIndex = 0;
    private int GraphicsIndex = 0;
    private int AudioIndex = 0;

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

        PanelNumber = GUILayout.Toolbar(PanelNumber, new string[] { "GamePlay", "Controls", "Graphics", "Audio" }); //this line creates a toolbar and places the enum in the variable

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

        /*This is the beginning of the gameplay settings*/
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

/*This is the beginning of the Controls settings*/
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

/*This is the beginning of the graphics settings*/
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

        EditorGUILayout.LabelField("Graphics", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();

        string[] GraphicsOptions = new[] { "Resolution", "Full Screen", "Dropdown" };

        GraphicsIndex = EditorGUILayout.Popup(GraphicsIndex, GraphicsOptions);

        if (GUILayout.Button("Create Graphics " + GraphicsOptions[GraphicsIndex]))
        {
            Manager.CreateGraphics(GraphicsIndex);
        }

        GUILayout.EndHorizontal();
/*This is the end of the Grapics settings*/

/*This is the beginning of the Audio settings*/
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

        base.OnInspectorGUI();
    }
}
