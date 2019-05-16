using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

public class Structure : MonoBehaviour
{
    [Inject] private EntityFactory ModuleFactory;

    public GameObject freePointParent;
    public GameObject ModuleParent;

    public List<Entity> modules = new List<Entity>();

    public List<Vector3> freePoints = new List<Vector3>();
    public List<Vector3> modulesPoints = new List<Vector3>();

    private Vector3 _gridSize;
    private int _moduleSpacing;
    private int _moduleAmount;

    public void RefreshStats(Vector3 gridSize, int moduleSpacing, int moduleAmount)
    {
        _gridSize = gridSize;
        _moduleSpacing = moduleSpacing;
        _moduleAmount = moduleAmount;
    }


    public void CreateSpaceStation()
    {
        float smoothProgress = 0;
        EditorUtility.DisplayProgressBar("Creating Space Station", "Space Point Progress", smoothProgress);

        Vector3 position = new Vector3();
        Vector3 initialPosition = new Vector3((_gridSize.x / 2) * _moduleSpacing,
                                        (_gridSize.y / 2) * _moduleSpacing,
                                        (_gridSize.z / 2) * _moduleSpacing);

        int totalProgress = 2;

        for (int h = 0; h < _gridSize.z; h++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                for (int x = 0; x < _gridSize.x; x++)
                {
                    position = new Vector3(x * _moduleSpacing, h * _moduleSpacing, y * _moduleSpacing);

                    if (position == initialPosition)
                    {
                        Entity baseModule = ModuleFactory.CreateEntity(position, ModuleParent.transform, SpacepointType.Module, this);
                        modules.Add(baseModule);
                        modulesPoints.Add(baseModule.modulePosition);
                    }
                    else
                    {
                        Entity spacePoint = ModuleFactory.CreateEntity(position, freePointParent.transform, SpacepointType.SpacePoint, this);
                        freePoints.Add(position);
                    }

                    ;
                }
            }
        }

        smoothProgress++;
        EditorUtility.DisplayProgressBar("Creating Space Station", "Module Creation Progress", smoothProgress / totalProgress);

        for (int i = 0; i < _moduleAmount - 1; i++)
        {
            Vector3 newModulePosition;

            newModulePosition = GetFreeNeighbours()[Random.Range(0,GetFreeNeighbours().Count)];

            if (freePoints.Contains(newModulePosition))
            {
                Entity Module = ModuleFactory.CreateEntity(newModulePosition, ModuleParent.transform, SpacepointType.Module, this);
                modules.Add(Module);
                modulesPoints.Add(Module.modulePosition);
                freePoints.Remove(newModulePosition);
                DestroyImmediate(GameObject.Find(newModulePosition.ToString()));
            }
        }

        smoothProgress++;
        EditorUtility.DisplayProgressBar("Creating Space Station", "Finishing Up Progress", smoothProgress / totalProgress);

        AssignNeighbours();

        for (int i = 0; i < modules.Count; i++)
        {
            modules[i].SetupVisuals();
        }

        EditorUtility.ClearProgressBar();
    }
    
    public List<Entity> GetNeighboursForModule(Entity module)
    {
        List<Entity> neightbours = new List<Entity>();
        //+x
        DirectionModuleCheckForNeightbours(neightbours, new Vector3(1 * _moduleSpacing, 0, 0), module);
        //-x
        DirectionModuleCheckForNeightbours(neightbours, new Vector3(-1 * _moduleSpacing, 0, 0), module);
        //+y
        DirectionModuleCheckForNeightbours(neightbours, new Vector3(0, 1 * _moduleSpacing, 0), module);
        //-y
        DirectionModuleCheckForNeightbours(neightbours, new Vector3(0, -1 * _moduleSpacing, 0), module);
        //+z
        DirectionModuleCheckForNeightbours(neightbours, new Vector3(0, 0, -1 * _moduleSpacing), module);
        //-z
        DirectionModuleCheckForNeightbours(neightbours, new Vector3(0, 0, 1 * _moduleSpacing), module);
       
        return neightbours;
    }

    public void DirectionModuleCheckForNeightbours(List<Entity> neightbours, Vector3 newVectorPossiblePos, Entity module)
    {
        //check all neighbours
        Vector3 possibleSpot = new Vector3();

        possibleSpot = module.modulePosition + newVectorPossiblePos;
        if (possibleSpot != null && modulesPoints.Contains(possibleSpot))
        {
            for (int i = 0; i < modules.Count; i++)
            {
                if (modules[i].modulePosition == possibleSpot)
                {
                    neightbours.Add(modules[i]);
                }
            }
        }
    }
    
    public List<Vector3> GetFreeNeighbours()
    {
        List<Vector3> freeSpots = new List<Vector3>();

        for (int i = 0; i < modules.Count; i++)
        {
            //+x
            PositionCheckFreeNeighbours(freeSpots, new Vector3(1 * _moduleSpacing, 0, 0), modules[i]);
            //-x
            PositionCheckFreeNeighbours(freeSpots, new Vector3(-1 * _moduleSpacing, 0, 0), modules[i]);
            //+y
            PositionCheckFreeNeighbours(freeSpots, new Vector3(0, 1 * _moduleSpacing, 0), modules[i]);
            //-y
            PositionCheckFreeNeighbours(freeSpots, new Vector3(0, -1 * _moduleSpacing, 0), modules[i]);
            //+z
            PositionCheckFreeNeighbours(freeSpots, new Vector3(0, 0, -1 * _moduleSpacing), modules[i]);
            //-z
            PositionCheckFreeNeighbours(freeSpots, new Vector3(0, 0, 1 * _moduleSpacing), modules[i]);
        }
            
        return freeSpots;
    }

    void PositionCheckFreeNeighbours(List<Vector3> freeSpots, Vector3 addedPosition, Entity module)
    {
        //check all neighbours
        Vector3 possibleSpot = new Vector3();

        possibleSpot = module.transform.position + addedPosition;
        if (freePoints.Contains(possibleSpot))
        {
            freeSpots.Add(possibleSpot);
        }
    }

    void AssignNeighbours()
    {
        for (int i = 0; i < modules.Count; i++)
        {
            modules[i].Neightbours = GetNeighboursForModule(modules[i]);
        }
    }

    public Quaternion GetDirectionForNewModule(Entity moduleBase, Entity moduleNew)
    {
        Quaternion orientation = new Quaternion();

        Vector3 moduleBasePosition = moduleBase.modulePosition;

        if (moduleNew.modulePosition.x > moduleBasePosition.x)
        {
            orientation.eulerAngles = new Vector3(0,90,0);
        }

        if (moduleNew.modulePosition.x < moduleBasePosition.x)
        {
            orientation.eulerAngles = new Vector3(0, -90, 0);
        }

        if (moduleNew.modulePosition.y > moduleBasePosition.y)
        {
            orientation.eulerAngles = new Vector3(-90, 0, 0);
        }

        if (moduleNew.modulePosition.y < moduleBasePosition.y)
        {
            orientation.eulerAngles = new Vector3(90, 0, 0);
        }

        if (moduleNew.modulePosition.z > moduleBasePosition.z)
        {
            orientation.eulerAngles = new Vector3(0, 0, 0);
        }

        if (moduleNew.modulePosition.z < moduleBasePosition.z)
        {
            orientation.eulerAngles = new Vector3(0, 180, 0);
        }

        return orientation;
    }
}
