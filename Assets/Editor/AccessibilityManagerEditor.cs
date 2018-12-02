using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AccessibilityManager))]
public class AccessibilityManagerEditor : Editor
{
	// Use this for initialization

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
        if (GUILayout.Button("Create GamePlay Panel"))
        {
            Manager.CreatePanel();
        }

        base.OnInspectorGUI();
    }
}
