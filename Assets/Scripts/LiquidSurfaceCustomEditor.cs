using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LiquidSurface))]
public class LiquidSurfaceCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LiquidSurface __target = (LiquidSurface)target;
        DrawDefaultInspector();
        if (GUILayout.Button("Test")) 
        {
            __target.Test();
        }
    }
}
