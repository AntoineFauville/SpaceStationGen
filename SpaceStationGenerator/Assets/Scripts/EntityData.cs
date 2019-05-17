using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Station/EntityData", order = 1)]
public class EntityData : ScriptableObject
{
    public ModuleType ModuleType;
    public int Health;
    public int StorageCapacity;
    public int EnergyProduction;
    public int BedAmount;
    public int LifeHealingCapacity;
    public GameObject Prefab;
}
