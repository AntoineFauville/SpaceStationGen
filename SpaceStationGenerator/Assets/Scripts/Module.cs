using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    private GameSettings _gameSettings;

    public GameObject[] Connections;
    public GameObject[] StorageBoxes;

    public void SetupReferences(GameSettings gameSettings)
    {
        _gameSettings = gameSettings;

        PlaceConnections();
        PlaceStorageBoxes();
    }

    public void PlaceConnections()
    {
        for (int i = 0; i < Connections.Length; i++)
        {
            GameObject connection = Object.Instantiate(_gameSettings.Connection);
            connection.transform.SetParent(Connections[i].transform);
            connection.transform.rotation = Connections[i].transform.rotation;
            connection.transform.localPosition = new Vector3(0, 0, 0 + _gameSettings.ConnectionOffset);
        }
    }

    public void PlaceStorageBoxes()
    {
        for (int i = 0; i < StorageBoxes.Length; i++)
        {
            if (_gameSettings.BoxChance > Random.Range(0, 100))
            {
                GameObject storage = Object.Instantiate(_gameSettings.StorageBox);
                storage.transform.SetParent(StorageBoxes[i].transform);
                storage.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }
}
