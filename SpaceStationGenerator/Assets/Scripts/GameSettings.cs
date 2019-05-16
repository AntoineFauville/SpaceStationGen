using System;
using UnityEngine;

[Serializable]
public class GameSettings
{
    [Header("Player")]
    public PlayerControllerFPS PlayerController;

    [Header("Structure")]
    public Structure Structure;
    public Entity Module;
    public GameObject SpaceStationModule;
    public GameObject SpaceStationSmall;
    public GameObject SpaceStationLarge;
    public GameObject SpaceStationThick;
    public GameObject SpaceStationLife;
    public GameObject SpaceStationSolarPanel;
    public GameObject SpaceStationConnection;
    public GameObject SpaceStationDoor;

    //public Spacepoint PointInSpace;
    public Vector3 GridSize = new Vector3(5,5,5);
    public int ModuleSpacing = 7;
    public int ModuleAmount = 20;
}
