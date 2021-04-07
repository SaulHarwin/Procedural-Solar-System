using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Information))]
public class Button : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        EditorGUILayout.LabelField("Trail Actions", EditorStyles.boldLabel);

        Information information = (Information) target;

        if (GUILayout.Button(new GUIContent("Enable all Trails", "Hint: Press Space"))) {
            information.EnableAllTrails();
        }
        if (GUILayout.Button(new GUIContent("Clear all Trails", "Hint: Press C"))) {
            information.ClearTrails();
        }
    }
}