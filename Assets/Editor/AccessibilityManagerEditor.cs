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
        if (GUILayout.Button("Create GamePlay Panel"))
        {
            Manager.CreateGamePlayPanel();
        }

        if (GUILayout.Button("Create Controls Panel"))
        {
            Manager.CreateControlsPanel();
        }

        if (GUILayout.Button("Create Graphics Panel"))
        {
            Manager.CreateGraphicsPanel();
        }

        if (GUILayout.Button("Create Audio Panel"))
        {
            Manager.CreateAudioPanel();
        }
    }
}
