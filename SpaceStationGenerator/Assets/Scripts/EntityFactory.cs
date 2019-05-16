using Zenject;
using UnityEngine;
using System.Collections.Generic;

public class EntityFactory
{
    [Inject] private GameSettings _gameSettings;
    
    public Entity CreateEntity(Vector3 position, Transform parent, SpacepointType type, Structure structure)
    {
        Entity module = Object.Instantiate(_gameSettings.Module);

        module.name = "(" + position.x.ToString() + ",0, " + position.y.ToString() + ",0, " + position.z.ToString() + ",0)";
        module.SetupReferences(_gameSettings, position, module.name, type, structure);
        module.transform.SetParent(parent);
        module.transform.position = position;

        return module;
    }
}
