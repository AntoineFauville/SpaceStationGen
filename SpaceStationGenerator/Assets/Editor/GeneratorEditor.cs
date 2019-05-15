using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Generator))]
[CanEditMultipleObjects]

public class GeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Generator generator = (Generator)target;

        serializedObject.Update();

        if (GUILayout.Button("Generate"))
        {
            generator.Generate();
        }
    }
}
