using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Generator))]
[CanEditMultipleObjects]

public class GeneratorEditor : Editor
{
    SerializedProperty GridSizeX;
    SerializedProperty GridSizeY;
    SerializedProperty GridSizeZ;
    SerializedProperty ModuleSpacing;
    SerializedProperty ModuleAmount;

    bool showOverride = false;

    void OnEnable()
    {
        GridSizeX = serializedObject.FindProperty("GridSizeX");
        GridSizeY = serializedObject.FindProperty("GridSizeY");
        GridSizeZ = serializedObject.FindProperty("GridSizeZ");
        ModuleSpacing = serializedObject.FindProperty("ModuleSpacing");
        ModuleAmount = serializedObject.FindProperty("ModuleAmount");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        Generator generator = (Generator)target;

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        
        if (GUILayout.Button("ResetDataToPreset"))
        {
            generator.ResetDataToPreset();
        }

        showOverride = EditorGUILayout.Foldout(showOverride, "Preset Override");
        if (showOverride)
        {
            GUILayout.Label("Override the values of the preset", EditorStyles.boldLabel);
            if (GUILayout.Button("OverridePresetValues"))
            {
                generator.SaveValuesAsNewPresetDefault();
            }
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        }
        

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        EditorGUILayout.IntSlider(GridSizeX, 4, 20, new GUIContent("GridSize X"));
        EditorGUILayout.IntSlider(GridSizeY, 4, 20, new GUIContent("GridSize Y"));
        EditorGUILayout.IntSlider(GridSizeZ, 4, 20, new GUIContent("GridSize Z"));
        EditorGUILayout.IntSlider(ModuleSpacing, 7, 14, new GUIContent("Module Spacing"));
        EditorGUILayout.IntSlider(ModuleAmount, 2, 100, new GUIContent("Module Amount"));
        if (GUILayout.Button("Apply New Values"))
        {
            generator.ApplyToStructureNewValues();
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        if (GUILayout.Button("Generate"))
        {
            generator.Generate();
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        serializedObject.ApplyModifiedProperties();
    }
}
