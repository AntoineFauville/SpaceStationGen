using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Structure : MonoBehaviour
{
    [Inject] private ModuleFactory ModuleFactory;
    [Inject] private GameSettings _gameSettings;
    
    public Vector3 currentPosition;

    List<Vector3> savedPosition = new List<Vector3>();

    private bool destroyEntity;

    void Start()
    {
        //base module
        ModuleFactory.CreateModule(_gameSettings.initialPosition, this.transform);
        currentPosition = _gameSettings.initialPosition;
        savedPosition.Add(currentPosition);

        //get a random rotation and add one
        for (int i = 0; i < _gameSettings.ModuleAmount; i++)
        {
            Vector3 pos = GenerateNewPosition();
            if (!destroyEntity)
            {
                ModuleFactory.CreateModule(pos, this.transform);
            }
            else
            {

                destroyEntity = false;
            }
        }
    }

    public Vector3 GenerateNewPosition()
    {
        int direction = Random.Range(1, 4);
        if (direction == 1)
        {
            return ModulePosition(new Vector3(7,0,0));            
        }
        if (direction == 2)
        {
            return ModulePosition(new Vector3(-7, 0, 0));
        }
        if (direction == 3)
        {
            return ModulePosition(new Vector3(0, 0, 7));
        }
        if (direction == 4)
        {
            return ModulePosition(new Vector3(0, 0, -7));
        }
        return new Vector3(0,0,0);
    }

    private Vector3 ModulePosition(Vector3 position)
    {
        Vector3 modulePosition = currentPosition + new Vector3(position.x, position.y, position.z);
        if (savedPosition.Contains(modulePosition))
        {
            destroyEntity = true;
            return new Vector3(0, 0, 0);
        }
        else
        {
            currentPosition = modulePosition;
            savedPosition.Add(currentPosition);
            return modulePosition;
        }
    }
}
