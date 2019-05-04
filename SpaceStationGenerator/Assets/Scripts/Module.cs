using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    private GameSettings _gameSettings;

    public Connection[] Connections;
    public GameObject[] StorageBoxes;

    public void SetupReferences(GameSettings gameSettings)
    {
        _gameSettings = gameSettings;
    }
}
