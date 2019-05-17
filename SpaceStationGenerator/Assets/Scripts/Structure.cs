using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;
using UnityEngine.UI;

public class Structure : MonoBehaviour
{
    [Inject] private EntityFactory ModuleFactory;

    public GameObject freePointParent;
    public GameObject ModuleParent;

    public List<Entity> Entities = new List<Entity>();

    public List<Vector3> freePoints = new List<Vector3>();
    public List<Vector3> modulesPoints = new List<Vector3>();

    private Vector3 _gridSize;
    private int _moduleSpacing;
    private int _moduleAmount;
    
    private string informationToDisplay;

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
                        Entities.Add(baseModule);
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
                Entities.Add(Module);
                modulesPoints.Add(Module.modulePosition);
                freePoints.Remove(newModulePosition);
                DestroyImmediate(GameObject.Find(newModulePosition.ToString()));
            }
        }

        smoothProgress++;
        EditorUtility.DisplayProgressBar("Creating Space Station", "Finishing Up Progress", smoothProgress / totalProgress);

        AssignNeighbours();

        for (int i = 0; i < Entities.Count; i++)
        {
            Entities[i].SetupVisuals();
        }

        EditorUtility.ClearProgressBar();
        DisplayInformation();
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
            for (int i = 0; i < Entities.Count; i++)
            {
                if (Entities[i].modulePosition == possibleSpot)
                {
                    neightbours.Add(Entities[i]);
                }
            }
        }
    }
    
    public List<Vector3> GetFreeNeighbours()
    {
        List<Vector3> freeSpots = new List<Vector3>();

        for (int i = 0; i < Entities.Count; i++)
        {
            //+x
            PositionCheckFreeNeighbours(freeSpots, new Vector3(1 * _moduleSpacing, 0, 0), Entities[i]);
            //-x
            PositionCheckFreeNeighbours(freeSpots, new Vector3(-1 * _moduleSpacing, 0, 0), Entities[i]);
            //+y
            PositionCheckFreeNeighbours(freeSpots, new Vector3(0, 1 * _moduleSpacing, 0), Entities[i]);
            //-y
            PositionCheckFreeNeighbours(freeSpots, new Vector3(0, -1 * _moduleSpacing, 0), Entities[i]);
            //+z
            PositionCheckFreeNeighbours(freeSpots, new Vector3(0, 0, -1 * _moduleSpacing), Entities[i]);
            //-z
            PositionCheckFreeNeighbours(freeSpots, new Vector3(0, 0, 1 * _moduleSpacing), Entities[i]);
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
        for (int i = 0; i < Entities.Count; i++)
        {
            Entities[i].Neightbours = GetNeighboursForModule(Entities[i]);
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

    void DisplayInformation()
    {
        informationToDisplay = "";

        Text textInformation = GameObject.Find("InformationsText").GetComponent<Text>();
        
        informationToDisplay += "Life Amount : " + LifeCount().ToString();
        informationToDisplay += "\n" + "Storage Capacity : " + StorageCount().ToString();
        informationToDisplay += "\n" + "Energy Production : " + EnergyCount().ToString();
        informationToDisplay += "\n" + "Bed Amount : " + LifeCount().ToString();
        informationToDisplay += "\n" + "Crew Healing Bed : " + LifeCount().ToString();

        textInformation.text = informationToDisplay;
    }

    int LifeCount()
    {
        int structureLifeAmount = 0;
        for (int i = 0; i < Entities.Count; i++)
        {
            structureLifeAmount += Entities[i].EntityData.Health;
        }
        return structureLifeAmount;
    }

    int StorageCount()
    {
        int structureStorageAmount = 0;
        for (int i = 0; i < Entities.Count; i++)
        {
            structureStorageAmount += Entities[i].EntityData.StorageCapacity;
        }
        return structureStorageAmount;
    }

    int EnergyCount()
    {
        int structureEnergyAmount = 0;
        for (int i = 0; i < Entities.Count; i++)
        {
            structureEnergyAmount += Entities[i].EntityData.EnergyProduction;
        }
        return structureEnergyAmount;
    }

    int BedCount()
    {
        int structureBedAmount = 0;
        for (int i = 0; i < Entities.Count; i++)
        {
            structureBedAmount += Entities[i].EntityData.BedAmount;
        }
        return structureBedAmount;
    }

    int CrewHealingCount()
    {
        int structureCrewHealingAmount = 0;
        for (int i = 0; i < Entities.Count; i++)
        {
            structureCrewHealingAmount += Entities[i].EntityData.LifeHealingCapacity;
        }
        return structureCrewHealingAmount;
    }
}
