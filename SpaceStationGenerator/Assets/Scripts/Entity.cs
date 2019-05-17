using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private GameSettings _gameSettings;

    public Vector3 modulePosition;
    public string Name;
    public List<Entity> Neightbours = new List<Entity>();
    private SpacepointType _type;
    public ModuleType _moduleType;
    private Structure _structure;
    public EntityData EntityData;

    public void SetupReferences(GameSettings gameSettings, Vector3 position, string name, SpacepointType type, Structure structure)
    {
        _gameSettings = gameSettings;
        modulePosition = position;
        Name = name;
        _structure = structure;
        _type = type;
    }

    public void SetupVisuals()
    {
        if (_type == SpacepointType.Module)
        {
            if (Neightbours.Count == 1)
            {
                EntityData = _gameSettings.SolarPanelModuleData;
            }
            else if(Neightbours.Count == 2)
            {
                EntityData = _gameSettings.SmallModuleData;
            }
            else if (Neightbours.Count == 3)
            {
                EntityData = _gameSettings.LifeModuleData;
            }
            else if (Neightbours.Count == 4)
            {
                EntityData = _gameSettings.StandartModuleData;
            }
            else if (Neightbours.Count == 5)
            {
                EntityData = _gameSettings.LargeModuleData;
            }
            else
            {
                EntityData = _gameSettings.ThickModuleData;
            }

            VisualFactory(_moduleType);
            ConnectionFactory();
        }
    }

    void VisualFactory(ModuleType visualType)
    {
        GameObject visuals = Object.Instantiate(EntityData.Prefab);
        visuals.transform.SetParent(this.transform);
        visuals.transform.localPosition = new Vector3(0, 0, 0);

        if (EntityData.ModuleType == ModuleType.SolarPanel)
        {
            visuals.transform.rotation = _structure.GetDirectionForNewModule(Neightbours[0], this);
        }
    }

    void ConnectionFactory()
    {
        for (int i = 0; i < Neightbours.Count; i++)
        {
            GameObject connection = Object.Instantiate(_gameSettings.SpaceStationConnection);
            connection.transform.SetParent(this.transform);
            connection.transform.localPosition = new Vector3(0, 0, 0);
            connection.transform.rotation = _structure.GetDirectionForNewModule(this, Neightbours[i]);
            connection.name = "Connection " + Neightbours[i];
        }
    }
}
