using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshGenerator))]
public class MeshGeneratorCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MeshGenerator __target = (MeshGenerator)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Setup")) 
        {
            __target.Setup();
        }
        if (GUILayout.Button("Create Mesh by Hand")) 
        {
            __target.CreateMesh(true);
        }
        if (GUILayout.Button("Create Mesh by Object")) 
        {
            __target.CreateMesh(false);
        }
        if (GUILayout.Button("Clear")) 
        {
            __target.ClearMesh();
        }
    }
}
