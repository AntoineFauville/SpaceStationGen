using System;
using UnityEngine;

[Serializable]
public class GameSettings
{
    [Header("Player")]
    public PlayerControllerFPS PlayerController;

    [Header("Structure")]
    public Structure Structure;
    public Module Module;

    public GameObject PointInSpace;
    public int GridSize = 5;
    public int ModuleSpacing = 7;
    public int ModuleAmount = 20;
}
