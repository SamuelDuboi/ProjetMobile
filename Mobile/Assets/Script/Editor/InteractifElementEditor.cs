using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InteractifElement))]
public class InteractifElementEditor : Editor
{
    InteractifElement interactifElements;


    private bool foldoutListCam,foldoutListOpen, foldoutListInteract, foldoutLists;
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
        ObjectInteract();

        EditorGUILayout.Space(20);
        Popup();

        EditorGUILayout.Space(20);

        interactifElements.zoom = EditorGUILayout.Toggle("Zoom", interactifElements.zoom);

        if (interactifElements.zoom)
            Zoom();
        else
            PickUp();

              Links();
        EditorUtility.SetDirty(interactifElements);
        serializedObject.ApplyModifiedProperties();
        Repaint();

    }

    public void Zoom()
    {
        interactifElements.inventory = false;
            EditorGUILayout.Space(20);
        interactifElements.orthoGraphicSize = EditorGUILayout.FloatField("Orthographic size ", interactifElements.orthoGraphicSize);


        interactifElements.onlyZoom = EditorGUILayout.Toggle("Only zoom", interactifElements.onlyZoom);
        interactifElements.UpsideDown = EditorGUILayout.Toggle("Upside Down", interactifElements.UpsideDown);

        EditorGUILayout.Space(20);

        foldoutListCam = EditorGUILayout.BeginFoldoutHeaderGroup(foldoutListCam, "CamsToZoom");

            if (foldoutListCam)
                ManageCams(interactifElements.cams);
            EditorGUILayout.Space(20);
        EditorGUILayout.EndFoldoutHeaderGroup();


        EditorGUILayout.Space(20);

  


    }
    private void ManageCams(List<Cams> list)
    {
        if (list != null && list.Count != 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                list[i].camDirection = (CamDirection)EditorGUILayout.EnumPopup(list[i].camDirection);
                list[i].cam = (GameObject)EditorGUILayout.ObjectField(list[i].cam, typeof(GameObject), true);
                EditorGUILayout.EndHorizontal();
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
            if (interactifElements.ObjectoOpen != null && interactifElements.ObjectoOpen.Count != 0)
            {
                for (int i = 0; i < interactifElements.ObjectoOpen.Count; i++)
                {
                    interactifElements.ObjectoOpen[i] = EditorGUILayout.TextField("Name of item in inventorty",interactifElements.ObjectoOpen[i]);
                }

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("+", EditorStyles.miniButton))
                    interactifElements.AddList(interactifElements.ObjectoOpen);
                if (GUILayout.Button("-", EditorStyles.miniButton))
                    interactifElements.RemoveFromList(interactifElements.ObjectoOpen);
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                if (GUILayout.Button("+", EditorStyles.miniButton))
                    interactifElements.AddList(interactifElements.ObjectoOpen);
            }
            
        }

        EditorGUILayout.Space(20);
    }

    private void PickUp()
    {
        interactifElements.inventory = EditorGUILayout.Toggle("Will be Stock in inventory", interactifElements.inventory);
        if (interactifElements.inventory)
        {
            interactifElements.nameInventory = EditorGUILayout.TextField("Name in Inventory", interactifElements.nameInventory);
            interactifElements.inventoryTexture =(Sprite) EditorGUILayout.ObjectField( interactifElements.inventoryTexture, typeof(Sprite),true);
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
        
    private void Popup()
    {
        interactifElements.popup = EditorGUILayout.Toggle("PopUp", interactifElements.popup);
        if (interactifElements.popup)
        {
            interactifElements.popInteract = EditorGUILayout.Toggle("Pop Up On Interaction", interactifElements.popInteract);
            if (interactifElements.popInteract)
                interactifElements.PopupAfterAnim = false;
            interactifElements.PopupAfterAnim = EditorGUILayout.Toggle("Pop Up After Animation", interactifElements.PopupAfterAnim);
            if (interactifElements.PopupAfterAnim)
                interactifElements.popInteract = false;

            if(!interactifElements.popInteract && !interactifElements.PopupAfterAnim)
            {
                EditorGUILayout.LabelField("You need to select when the pop up will appear",EditorStyles.helpBox);
            }
            else
            {
                EditorGUILayout.LabelField("Text");
                interactifElements.text = EditorGUILayout.TextArea(interactifElements.text, EditorStyles.textArea);
                interactifElements.timePopup = EditorGUILayout.FloatField("Time", interactifElements.timePopup);
            }
        }
        else
        {
            interactifElements.PopupAfterAnim = false;
            interactifElements.popInteract = false;
            interactifElements.text = string.Empty;
        }
    }
    private void ObjectInteract()
    {
        foldoutLists = EditorGUILayout.Foldout(foldoutLists, "Objects linked");
        if (foldoutLists)
        {
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
        }
       
    }
}
