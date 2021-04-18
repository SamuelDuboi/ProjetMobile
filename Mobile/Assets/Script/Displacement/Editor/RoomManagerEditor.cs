using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditor : Editor
{
    RoomManager roomManager;
    private void OnEnable()
    {
        roomManager = target as RoomManager;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("TurnLeft"))
        {
            if (!roomManager.upsideDownEditor)
                roomManager.TurnEditor(90);
            else
                roomManager.TurnEditor(-90);
        }
        if (GUILayout.Button("TurnRight"))
        {
            if (!roomManager.upsideDownEditor)
                roomManager.TurnEditor(-90);
            else
                roomManager.TurnEditor(90);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("Position          " + roomManager.currentDirection.ToString(), EditorStyles.boldLabel);

        EditorGUILayout.Space(20);
        if (GUILayout.Button("UpsideDown"))
        {
            if (!roomManager.upsideDownEditor)
            {
                roomManager.main.transform.rotation = Quaternion.Euler(new Vector3(-270,45,-90));
                roomManager.main.transform.localPosition = Vector3.up*12;
            }
            else
            {
                roomManager.main.transform.rotation = Quaternion.Euler(-90,45,0);
                roomManager.main.transform.localPosition = Vector3.zero;

            }
            roomManager.upsideDownEditor = !roomManager.upsideDownEditor;
        }

        EditorUtility.SetDirty(roomManager);
        serializedObject.ApplyModifiedProperties();
    }
}
