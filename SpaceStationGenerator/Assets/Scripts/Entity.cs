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
                _moduleType = ModuleType.SolarPanel;
            }
            else if(Neightbours.Count == 2)
            {
                _moduleType = ModuleType.Small;
            }
            else if (Neightbours.Count == 3)
            {
                _moduleType = ModuleType.LifeSupport;
            }
            else if (Neightbours.Count == 4)
            {
                _moduleType = ModuleType.Standart;
            }
            else if (Neightbours.Count == 5)
            {
                _moduleType = ModuleType.Large;
            }
            else
            {
                _moduleType = ModuleType.Thick;
            }

            VisualFactory(_moduleType);
            ConnectionFactory();
        }
    }

    void VisualFactory(ModuleType visualType)
    {
        if (_moduleType == ModuleType.LifeSupport)
        {
            GameObject visuals = Object.Instantiate(_gameSettings.SpaceStationLife);
            visuals.transform.SetParent(this.transform);
            visuals.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if (_moduleType == ModuleType.Standart)
        {
            GameObject visuals = Object.Instantiate(_gameSettings.SpaceStationModule);
            visuals.transform.SetParent(this.transform);
            visuals.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if (_moduleType == ModuleType.Small)
        {
            GameObject visuals = Object.Instantiate(_gameSettings.SpaceStationSmall);
            visuals.transform.SetParent(this.transform);
            visuals.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if (_moduleType == ModuleType.Large)
        {
            GameObject visuals = Object.Instantiate(_gameSettings.SpaceStationLarge);
            visuals.transform.SetParent(this.transform);
            visuals.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if (_moduleType == ModuleType.Thick)
        {
            GameObject visuals = Object.Instantiate(_gameSettings.SpaceStationThick);
            visuals.transform.SetParent(this.transform);
            visuals.transform.localPosition = new Vector3(0, 0, 0);
        }
        else if (_moduleType == ModuleType.SolarPanel)
        {
            GameObject visuals = Object.Instantiate(_gameSettings.SpaceStationSolarPanel);
            visuals.transform.SetParent(this.transform);
            visuals.transform.localPosition = new Vector3(0, 0, 0);
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
