using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Structure : MonoBehaviour
{
    [Inject] private ModuleFactory ModuleFactory;
    [Inject] private GameSettings _gameSettings;

    List<Module> modules = new List<Module>();
    List<Connection> freeConnections = new List<Connection>();

    void Start()
    {
        //base module creation
        Module baseModule = ModuleFactory.CreateStartingModule(_gameSettings.initialPosition, this.transform);
        modules.Add(baseModule);
        //assign the parent module to the connection
        for (int i = 0; i < baseModule.Connections.Length; i++)
        {
            baseModule.Connections[i].MyModule = baseModule;
        }

        for (int i = 0; i < _gameSettings.ModuleAmount; i++)
        {
            //get all the free connections
            GetFreeConnections();
            //get all the free space where i can create a new module, choose randomly between free connection, and place the module
            Connection currentConnection = freeConnections[Random.Range(0, freeConnections.Count)];
        
            //generate the module
            Module node = ModuleFactory.CreateModule(currentConnection, this.transform);
            //assign the parent module to the connection
            for (int y = 0; y < node.Connections.Length; y++)
            {
                node.Connections[y].MyModule = node;
            }
            currentConnection.ConnectedModule = node;
            modules.Add(node);
            
        }

        for (int i = 0; i < modules.Count; i++)
        {
            PlaceConnectionsVisuals(modules[i]);
            PlaceStorageBoxes(modules[i]);
        }
       
    }
    
    public void GetFreeConnections()
    {
        for (int i = 0; i < modules.Count; i++)
        {
            for (int y = 0; y < modules[i].Connections.Length; y++)
            {
                if(modules[i].Connections[y].ConnectedModule == null)
                {
                    freeConnections.Add(modules[i].Connections[y]);
                }
            }
        }
    }

    public void PlaceConnectionsVisuals(Module module)
    {
        for (int i = 0; i < module.Connections.Length; i++)
        {
            if (module.Connections[i].ConnectedModule != null)
            {
                GameObject connection = Object.Instantiate(_gameSettings.Connection);
                connection.transform.SetParent(module.Connections[i].transform);
                connection.transform.rotation = module.Connections[i].transform.rotation;
                connection.transform.localPosition = new Vector3(0, 0, 0 + _gameSettings.ConnectionOffset);
            }
        }
    }

    public void PlaceStorageBoxes(Module module)
    {
        for (int i = 0; i < module.StorageBoxes.Length; i++)
        {
            if (_gameSettings.BoxChance > Random.Range(0, 100))
            {
                GameObject storage = Object.Instantiate(_gameSettings.StorageBox);
                storage.transform.SetParent(module.StorageBoxes[i].transform);
                storage.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }
}
