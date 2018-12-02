using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AccessibilityManager))]
public class AccessibilityManagerEditor : Editor
{
    private string PanelName;

	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public override void OnInspectorGUI()
    {
        AccessibilityManager Manager = (AccessibilityManager)target;
        Manager.PanelNumber = GUILayout.Toolbar(Manager.PanelNumber, new string[] { "GamePlay", "Controls", "Graphics", "Audio" });

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

        if (GUILayout.Button("Create " + PanelName + " Panel"))
        {
            Manager.CreatePanel();
        }

        base.OnInspectorGUI();
    }
}
