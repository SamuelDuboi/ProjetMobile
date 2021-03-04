using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractifElement))]
public class InteractifElementEditor : Editor
{
    InteractifElement interactifElements;


    private bool foldoutListLeft, foldoutListRight,foldoutListOpen, foldoutListInteract;
    private void OnEnable()
    {
        interactifElements = target as InteractifElement;
    }
    public override void OnInspectorGUI()
    {
        interactifElements.interactionAnimator = (Animator)EditorGUILayout.ObjectField("Interaction Animator", interactifElements.interactionAnimator, typeof(Animator), true);

        interactifElements.isLInkedToWall = EditorGUILayout.Toggle("Link to a wall", interactifElements.isLInkedToWall);
        if (interactifElements.isLInkedToWall)
            interactifElements.wallLinked = (GameObject)EditorGUILayout.ObjectField("Wall linked", interactifElements.wallLinked, typeof(GameObject), true);
        EditorGUILayout.Space(20);
        foldoutListOpen = EditorGUILayout.BeginFoldoutHeaderGroup(foldoutListOpen, "ObjectToActivate");
        if (foldoutListOpen)
        {
            ManageObjectList(interactifElements.objectToActive);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space(20);
        foldoutListInteract = EditorGUILayout.BeginFoldoutHeaderGroup(foldoutListInteract, "Object to interact after anim");
        if (foldoutListInteract)
        {
            ManageObjectList(interactifElements.objectToInteract);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.Space(20);
        interactifElements.zoom = EditorGUILayout.Toggle("Zoom", interactifElements.zoom);

        if (interactifElements.zoom)
            Zoom();
        else
            PickUp();

        EditorUtility.SetDirty(interactifElements);
        serializedObject.ApplyModifiedProperties();
        Repaint();

    }

    public void Zoom()
    {
        interactifElements.inventory = false;
            EditorGUILayout.Space(20);

            interactifElements.zoomFromUp = EditorGUILayout.Toggle("Zoom from up", interactifElements.zoomFromUp);
            if (!interactifElements.zoomFromUp)
            {
                EditorGUILayout.Space(20);

                foldoutListLeft = EditorGUILayout.BeginFoldoutHeaderGroup(foldoutListLeft,"Zoom On Left");
                    if(foldoutListLeft)
                         ManageCamLis(interactifElements.leftCam);
                    EditorGUILayout.Space(20);
                EditorGUILayout.EndFoldoutHeaderGroup();

                    foldoutListRight = EditorGUILayout.BeginFoldoutHeaderGroup(foldoutListRight,"Zoom On Right");
                    if (foldoutListRight)
                        ManageCamLis(interactifElements.rightCam);
                EditorGUILayout.EndFoldoutHeaderGroup();

            }
            EditorGUILayout.Space(20);

            Links();

        
    }


    private void ManageCamLis(List<CamDirection> list)
    {
        if (list != null && list.Count != 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = (CamDirection)EditorGUILayout.EnumPopup(list[i]);
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+", EditorStyles.miniButton))
                interactifElements.AddList(list);
            if (GUILayout.Button("-", EditorStyles.miniButton))
                interactifElements.RemoveFromList(list);
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            if (GUILayout.Button("+", EditorStyles.miniButton))
                interactifElements.AddList(list);
        }
    }

    private void Links()
    {

        interactifElements.spawnNewTrial = EditorGUILayout.Toggle("Spawn a Trial", interactifElements.spawnNewTrial);
        if (interactifElements.spawnNewTrial)
        {
            interactifElements.TrialGameObjects = (GameObject)EditorGUILayout.ObjectField("Triak to Spawn", interactifElements.TrialGameObjects, typeof(GameObject), true);
            EditorGUILayout.Space(20);
        }


        interactifElements.hasLinkGameObject = EditorGUILayout.Toggle("Need object to open/", interactifElements.hasLinkGameObject);
        if (interactifElements.hasLinkGameObject)
        {
            ManageObjectList(interactifElements.ObjectoOpen);
        }

        EditorGUILayout.Space(20);
    }

    private void PickUp()
    {
        interactifElements.inventory = EditorGUILayout.Toggle("Will be Stock in inventory", interactifElements.inventory);
        if (interactifElements.inventory)
        {
            
            interactifElements.needToBeAssemble = EditorGUILayout.Toggle("Need To be Assemble", interactifElements.needToBeAssemble);
            if (interactifElements.needToBeAssemble)
            {
                interactifElements.otherAssemblingObject = (GameObject)EditorGUILayout.ObjectField("other object needed", interactifElements.otherAssemblingObject, typeof(GameObject), true);
                interactifElements.assembledGameObjetc = (GameObject)EditorGUILayout.ObjectField("New object created", interactifElements.assembledGameObjetc, typeof(GameObject), true);
            }
        }
    }

    private void ManageObjectList(List<GameObject> list)
    {
        if (list != null && list.Count != 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = (GameObject)EditorGUILayout.ObjectField(list[i], typeof(GameObject), true);
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+", EditorStyles.miniButton))
                interactifElements.AddList(list);
            if (GUILayout.Button("-", EditorStyles.miniButton))
                interactifElements.RemoveFromList(list);
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            if (GUILayout.Button("+", EditorStyles.miniButton))
                interactifElements.AddList(list);
        }

    }
        
    
}
