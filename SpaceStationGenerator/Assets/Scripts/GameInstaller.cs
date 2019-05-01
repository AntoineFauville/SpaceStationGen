﻿using Zenject;

public class GameInstaller : MonoInstaller {
    
    [Inject] private GameSettings _gameSettings;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerController>()
            .FromComponentInNewPrefab(_gameSettings.PlayerController)
            .AsSingle().NonLazy();
        //Container.Bind<Vehicule>()
        //    .FromResolveGetter<PlayerController>(playerController => playerController.GetVehicule())
        //    .AsSingle().NonLazy();
        //Container.Bind<GemFactory>()
        //    .AsSingle().NonLazy();
    }
}
