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
    [Inject] private GameSettings _gameSettings;

    public int GridSizeX;
    public int GridSizeY;
    public int GridSizeZ;

    public Vector3 GridSize;
    public int ModuleSpacing;
    public int ModuleAmount;

    void OnEnable()
    {
        Debug.Log("Initialising Generator Data");
        ResetDataToPreset();
    }

    public void ResetDataToPreset()
    {
        if (_gameSettings != null)
        {
            GridSize = _gameSettings.GridSize;
            ModuleSpacing = _gameSettings.ModuleSpacing;
            ModuleAmount = _gameSettings.ModuleAmount;

            GridSizeX = (int)GridSize.x;
            GridSizeY = (int)GridSize.y;
            GridSizeZ = (int)GridSize.z;
        }
    }

    public void SaveValuesAsNewPresetDefault()
    {
        if (_gameSettings != null)
        {
            _gameSettings.GridSize = GridSize;
            _gameSettings.ModuleSpacing = ModuleSpacing;
            _gameSettings.ModuleAmount = ModuleAmount;
        }
    }

    public void ApplyToStructureNewValues()
    {
        GridSize.x = (float)GridSizeX;
        GridSize.y = (float)GridSizeY;
        GridSize.z = (float)GridSizeZ;
        _structure.RefreshStats(GridSize, ModuleSpacing, ModuleAmount);
    }

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
