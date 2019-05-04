using Zenject;
using UnityEngine;

public class ModuleFactory
{
    [Inject] private GameSettings _gameSettings;

    public Module CreateModule(Connection connection, Transform parent)
    {
        Module module = Object.Instantiate(_gameSettings.Module);
      
        module.name = "X " + connection.transform.position.x.ToString() + " Y " + connection.transform.position.y.ToString() + " Z " + connection.transform.position.z.ToString();
        module.SetupReferences(_gameSettings);
        module.transform.SetParent(parent);

        module.transform.localRotation = connection.transform.localRotation;
        //find the positioon of the next module + offset
        Vector3 pos = connection.MyModule.transform.localPosition;
        Vector3 offset = new Vector3();
        if (connection.transform.position.x > pos.x)
        {
            offset = pos + new Vector3(7, 0, 0);
        }
        else if (connection.transform.position.x < pos.x)
        {
            offset = pos + new Vector3(-7, 0, 0);
        }
        else if (connection.transform.position.y > pos.y)
        {
            offset = pos + new Vector3(0, 7, 0);
        }
        else if (connection.transform.position.y < pos.y)
        {
            offset = pos + new Vector3(0, -7, 0);
        }
        else if (connection.transform.position.z > pos.z)
        {
            offset = pos + new Vector3(0, 0, 7);
        }
        else if (connection.transform.position.z < pos.z)
        {
            offset = pos + new Vector3(0, 0, -7);
        }
        module.transform.localPosition = offset;
        return module;
    }

    public Module CreateStartingModule(Vector3 position, Transform parent)
    {
        Module module = Object.Instantiate(_gameSettings.Module);

        module.name = "X " + position.x.ToString() + " Y " + position.y.ToString() + " Z " + position.z.ToString();
        module.SetupReferences(_gameSettings);
        module.transform.SetParent(parent);
        module.transform.position = position;

        return module;
    }
}
