using System;
using UnityEngine;

[Serializable]
public class GameSettings
{
    [Header("Player")]
    public PlayerController PlayerController;

    [Header("Structure")]
    public Structure Structure;
    public Module Module;
    public int ModuleAmount = 5;
    public Vector3 initialPosition = new Vector3(0, 0, 0);
    public float ModuleSpacing = 3;
    public GameObject Connection;
    public float ConnectionOffset = 0.5f;

    public int BoxChance = 50;
    public GameObject StorageBox;
}
