using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using Zenject;

[ExecuteInEditMode]

public class Generator : MonoBehaviour
{

    [Inject] private Structure _structure;

    public void Generate()
    {
        if (GameObject.Find("Structure(Clone)"))
        {
            foreach (Transform child in _structure.ModuleParent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            foreach (Transform child in _structure.freePointParent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            _structure.modules.RemoveRange(0,_structure.modules.Count);
            _structure.freePoints.RemoveRange(0, _structure.freePoints.Count);
            _structure.CreateSpaceStation();
        }
    }
}
