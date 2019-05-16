using Zenject;
using UnityEngine;
using System.Collections.Generic;

public class DoorCheckerFactory
{
    [Inject] private GameSettings _gameSettings;

    public DoorPlacementRayCast CreateDoorCheck()
    {
        DoorPlacementRayCast doorChecker =  new DoorPlacementRayCast();

        return doorChecker;
    }
}
