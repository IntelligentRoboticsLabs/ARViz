using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TabExample))]
public class TabExampleEditor : Editor {

    private TabExample myTarget;
    private SerializedObject soTarget;

    private SerializedProperty stringVar1;
    private SerializedProperty stringVar2;
    private SerializedProperty stringVar3;
    private SerializedProperty stringVar4;
    private SerializedProperty stringVar5;

    private SerializedProperty scalar_vertex;

    private SerializedProperty intVar1;
    private SerializedProperty intVar2;
    private SerializedProperty intVar3;
    private SerializedProperty intVar4;
    private SerializedProperty intVar5;
   

    private void OnEnable() {
        // var script = (MyScript)target;
        myTarget = (TabExample)target;
        soTarget = new SerializedObject(target);

        stringVar1 = soTarget.FindProperty("stringVar1");
        stringVar2 = soTarget.FindProperty("stringVar2");
        stringVar3 = soTarget.FindProperty("stringVar3");
        stringVar4 = soTarget.FindProperty("stringVar4");
        stringVar5 = soTarget.FindProperty("stringVar5");

        scalar_vertex = soTarget.FindProperty("scalar_vertex");

        intVar1 = soTarget.FindProperty("intVar1");
        intVar2 = soTarget.FindProperty("intVar2");
        intVar3 = soTarget.FindProperty("intVar3");
        intVar4 = soTarget.FindProperty("intVar4");
        intVar5 = soTarget.FindProperty("intVar5");
    }

    //  Change the inspector view
    public override void OnInspectorGUI()
    {
        soTarget.Update();
        EditorGUI.BeginChangeCheck();
        myTarget.tabsbar1 = GUILayout.Toolbar(myTarget.tabsbar1, new string[] {"Scalar", "Image", "PointCloud", "Laser"});
        switch (myTarget.tabsbar1)
        {
            case 0:
                myTarget.tabsbar2 = 4;
                myTarget.currentTab = "Scalar";
                break;
            case 1:
                myTarget.tabsbar2 = 4;
                myTarget.currentTab = "Image";
                break;
            case 2:
                myTarget.tabsbar2 = 4;
                myTarget.currentTab = "PointCloud";
                break;
            case 3:
                myTarget.tabsbar2 = 4;
                myTarget.currentTab = "Laser";
                break;
        }

        myTarget.tabsbar2 = GUILayout.Toolbar(myTarget.tabsbar2, new string[] {"TF", "Map", "PoseArray", "Path"});
        switch (myTarget.tabsbar2)
        {
            case 0:
                myTarget.tabsbar1 = 4;
                myTarget.currentTab = "TF";
                break;
            case 1:
                myTarget.tabsbar1 = 4;
                myTarget.currentTab = "Map";
                break;
            case 2:
                myTarget.tabsbar1 = 4;
                myTarget.currentTab = "PoseArray";
                break;
            case 3:
                myTarget.tabsbar1 = 4;
                myTarget.currentTab = "Path";
                break;
        }

        if (EditorGUI.EndChangeCheck()) {
            soTarget.ApplyModifiedProperties();
            GUI.FocusControl(null);
        }

        EditorGUI.BeginChangeCheck();

        switch(myTarget.currentTab) {
            case "Scalar":
                /*
                EditorGUILayout.PropertyField(stringVar1);
                EditorGUILayout.PropertyField(stringVar2);
                EditorGUILayout.PropertyField(stringVar3);
                EditorGUILayout.PropertyField(stringVar4);
                EditorGUILayout.PropertyField(stringVar5);
                */
                //Instantiate(scalar_viz, Vector3.zero, Quaternion.identity);
                myTarget.scalar_prefab = (GameObject)EditorGUILayout.ObjectField("Scalar", myTarget.scalar_prefab, typeof(GameObject), false);
                break;
            case "Image":
                //Instantiate(image_viz, Vector3.zero, Quaternion.identity);
                myTarget.image_prefab = (GameObject)EditorGUILayout.ObjectField("Image", myTarget.image_prefab, typeof(GameObject), false);
                break;
            case "PointCloud":
                //Instantiate(pointcloud_viz, Vector3.zero, Quaternion.identity);
                myTarget.pointcloud_prefab = (GameObject)EditorGUILayout.ObjectField("PointCloud", myTarget.pointcloud_prefab, typeof(GameObject), false);
                break;
            case "Laser":
                //Instantiate(laser_viz, Vector3.zero, Quaternion.identity);
                myTarget.laser_prefab = (GameObject)EditorGUILayout.ObjectField("Laser", myTarget.laser_prefab, typeof(GameObject), false);
                break;
            case "TF":
                //Instantiate(myTarget.tf_prefab, Vector3.zero, Quaternion.identity);
                myTarget.tf_prefab = (GameObject)EditorGUILayout.ObjectField("TF", myTarget.tf_prefab, typeof(GameObject), false);
                break;
            case "Map":
                //Instantiate(map_viz, Vector3.zero, Quaternion.identity);
                myTarget.map_prefab = (GameObject)EditorGUILayout.ObjectField("Map", myTarget.map_prefab, typeof(GameObject), false);
                break;
            case "PoseArray":
                //Instantiate(posearray_viz, Vector3.zero, Quaternion.identity);
                myTarget.posearray_prefab = (GameObject)EditorGUILayout.ObjectField("PoseArray", myTarget.posearray_prefab, typeof(GameObject), false);
                break;
            case "Path":
                //Instantiate(path_viz, Vector3.zero, Quaternion.identity);
                myTarget.path_prefab = (GameObject)EditorGUILayout.ObjectField("Path", myTarget.path_prefab, typeof(GameObject), false);
                break;
        }

        if (EditorGUI.EndChangeCheck())
        {
            soTarget.ApplyModifiedProperties();
        }
    }
}
