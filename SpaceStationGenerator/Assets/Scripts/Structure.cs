using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

public class Structure : MonoBehaviour
{
    [Inject] private ModuleFactory ModuleFactory;
    [Inject] private GameSettings _gameSettings;

    public GameObject freePointParent;
    public GameObject ModuleParent;

    public List<Module> modules = new List<Module>();

    public List<Vector3> freePoints = new List<Vector3>();

    public void CreateSpaceStation()
    {
        Vector3 position = new Vector3();
        Vector3 initialPosition = new Vector3((_gameSettings.GridSize/2)* _gameSettings.ModuleSpacing,
                                        (_gameSettings.GridSize / 2)*_gameSettings.ModuleSpacing,
                                        (_gameSettings.GridSize / 2) * _gameSettings.ModuleSpacing);

        for (int h = 0; h < _gameSettings.GridSize; h++)
        {
            for (int y = 0; y < _gameSettings.GridSize; y++)
            {
                for (int x = 0; x < _gameSettings.GridSize; x++)
                {
                    position = new Vector3(x * _gameSettings.ModuleSpacing, h * _gameSettings.ModuleSpacing, y * _gameSettings.ModuleSpacing);

                    if (position == initialPosition)
                    {
                        Module baseModule = ModuleFactory.CreateModule(position, ModuleParent.transform);
                        modules.Add(baseModule);
                    }
                    else
                    {
                        GameObject point = Object.Instantiate(_gameSettings.PointInSpace);
                        point.transform.SetParent(freePointParent.transform);
                        point.transform.position = position;

                        point.name = "(" + position.x.ToString() + ",0, " + position.y.ToString() + ",0, " + position.z.ToString() + ",0)";

                        freePoints.Add(position);
                    }
                }
            }
        }

        for (int i = 0; i < _gameSettings.ModuleAmount; i++)
        {

            Vector3 a;

            a = GetFreeNeighbours()[Random.Range(0,GetFreeNeighbours().Count)];

            if (freePoints.Contains(a))
            {
                Module Module = ModuleFactory.CreateModule(a, ModuleParent.transform);
                modules.Add(Module);
                freePoints.Remove(a);
                DestroyImmediate(GameObject.Find(a.ToString()));
            }
        }
    }
    
    public void GetFreeConnections()
    {
       
    }

    public void PlaceConnectionsVisuals()
    {
        
    }

    public void PlaceStorageBoxes()
    {
        
    }

    public List<Vector3> GetFreeNeighbours()
    {
        List<Vector3> freeSpots = new List<Vector3>();

        for (int i = 0; i < modules.Count; i++)
        {
            //check all neighbours
            Vector3 possibleSpot = new Vector3();
            //+x
            possibleSpot = modules[i].transform.position + new Vector3(1 * _gameSettings.ModuleSpacing, 0, 0);
            if (freePoints.Contains(possibleSpot))
            {
                freeSpots.Add(possibleSpot);
            }
            //-x
            possibleSpot = modules[i].transform.position + new Vector3(-1 * _gameSettings.ModuleSpacing, 0, 0);
            if (freePoints.Contains(possibleSpot))
            {
                freeSpots.Add(possibleSpot);
            }
            //+y
            possibleSpot = modules[i].transform.position + new Vector3(0, 1 * _gameSettings.ModuleSpacing, 0);
            if (freePoints.Contains(possibleSpot))
            {
                freeSpots.Add(possibleSpot);
            }
            //-y
            possibleSpot = modules[i].transform.position + new Vector3(0, -1 * _gameSettings.ModuleSpacing, 0);
            if (freePoints.Contains(possibleSpot))
            {
                freeSpots.Add(possibleSpot);
            }
            //+z
            possibleSpot = modules[i].transform.position + new Vector3(0, 0, -1 * _gameSettings.ModuleSpacing);
            if (freePoints.Contains(possibleSpot))
            {
                freeSpots.Add(possibleSpot);
            }
            //-z
            possibleSpot = modules[i].transform.position + new Vector3(0, 0, 1 * _gameSettings.ModuleSpacing);
            if (freePoints.Contains(possibleSpot))
            {
                freeSpots.Add(possibleSpot);
            }
        }
            
        return freeSpots;
    }
}
