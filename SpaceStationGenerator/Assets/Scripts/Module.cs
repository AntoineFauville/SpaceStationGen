using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    private GameSettings _gameSettings;
    
    public void SetupReferences(GameSettings gameSettings)
    {
        _gameSettings = gameSettings;
    }
}
