#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResetTransforms : EditorWindow
{
    public List<GameObject> originalObj = new List<GameObject>();
    public List<GameObject> newObj = new List<GameObject>();

    [MenuItem("Tools/Level Design/Align Transforms")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<ResetTransforms>("Reset Transforms");
    }

    private void OnGUI()
    {
        EditorGUILayout.Space(50);

        GUILayout.Label("Original Objects", EditorStyles.boldLabel);

        SerializedObject serializedObject= new SerializedObject(this);
        SerializedProperty originalProperty = serializedObject.FindProperty("originalObj");
        EditorGUILayout.PropertyField(originalProperty, true);

        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space(20);

        GUILayout.Label("New Objects", EditorStyles.boldLabel);

        SerializedProperty newProperty = serializedObject.FindProperty("newObj");
        EditorGUILayout.PropertyField(newProperty, true);
        serializedObject.ApplyModifiedProperties();

        GUILayout.Space(20);

        if (GUILayout.Button("Align Transforms"))
            UpdateAllTransforms();

        GUILayout.Space(20);

        if (GUILayout.Button("Unparent Originals"))
            UnparentAllOriginals();

        GUILayout.Space(20);

        if (GUILayout.Button("Randomize Originals Rotation"))
            RandomizeOriginalRotation();
    }
    
    public void UpdateAllTransforms()
    {
        for (int i = 0; i < originalObj.Count; i++)
        {
            newObj[i].transform.position = originalObj[i].transform.position;
            newObj[i].transform.rotation = originalObj[i].transform.rotation;
        }
    }

    public void UnparentAllOriginals()
    {
        for (int i = 0; i < originalObj.Count; i++)
        {
            originalObj[i].transform.parent = null;
        }
    }

    public void RandomizeOriginalRotation()
    {
        for (int i = 0; i < originalObj.Count; i++)
        {
            Quaternion rotation = new Quaternion(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));
            originalObj[i].transform.rotation = rotation;
        }
    }
}
#endif