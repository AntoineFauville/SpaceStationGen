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

    public void SetupReferences(GameSettings gameSettings, Vector3 position, string name, SpacepointType type)
    {
        _gameSettings = gameSettings;
        modulePosition = position;
        Name = name;
        
        _type = type;

        if (_type == SpacepointType.Module)
        {
            GameObject visuals = Object.Instantiate(_gameSettings.SpaceStationModule);
            visuals.transform.SetParent(this.transform);
            visuals.transform.position = new Vector3(0,0,0);
        }
    }

    //ask neighbours to create toward me + create toward him aswell.
    //flag somehow as marked that there is a connection in this direction.
        //creer une liste vide de potentiel connection autour
        //eliminer les elements de la liste du modele du voisin de la direction voulue + la mienne
}
