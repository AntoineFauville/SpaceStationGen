﻿using Zenject;

public class GameInstaller : MonoInstaller {
    
    [Inject] private GameSettings _gameSettings;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Structure>()
            .FromComponentInNewPrefab(_gameSettings.Structure)
            .AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerControllerFPS>()
           .FromComponentInNewPrefab(_gameSettings.PlayerController)
           .AsSingle().NonLazy();
        Container.Bind<DoorCheckerFactory>()
           .AsSingle().NonLazy();
        Container.Bind<EntityFactory>()
            .AsSingle().NonLazy();
    }
}
