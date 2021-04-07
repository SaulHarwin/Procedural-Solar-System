using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Information))]
public class Button : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        Information information = (Information) target;

        if (GUILayout.Button("Enable all trails")) {
            information.AllTrailsChanged();
        }
        if (GUILayout.Button("Clear all trails")) {
            information.ClearTrails();
        }
    }
}