﻿using Zenject;
using UnityEngine;

public class ModuleFactory
{
    [Inject] private GameSettings _gameSettings;
    
    public Module CreateModule(Vector3 position, Transform parent)
    {
        Module module = Object.Instantiate(_gameSettings.Module);

        module.name = "X " + position.x.ToString() + " Y " + position.y.ToString() + " Z " + position.z.ToString();
        module.SetupReferences(_gameSettings);
        module.transform.SetParent(parent);
        module.transform.position = position;

        return module;
    }
}
