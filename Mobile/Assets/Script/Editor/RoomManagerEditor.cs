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
        if (GUILayout.Button("TrunRight"))
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
        if (GUILayout.Button("UpsideDOwn"))
        {
            if (!roomManager.upsideDownEditor)
            {
                roomManager.animator.gameObject.transform.position = Vector3.up * 22;
                roomManager.animator.gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * 180);
            }
            else
            {
                roomManager.animator.gameObject.transform.position = Vector3.zero;
                roomManager.animator.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
            }
            roomManager.upsideDownEditor = !roomManager.upsideDownEditor;
        }

        EditorUtility.SetDirty(roomManager);
        serializedObject.ApplyModifiedProperties();
    }
}
