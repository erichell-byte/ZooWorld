using UnityEngine;
using Zenject;
using ZooWorld.Animals.Behaviours.Factory;
using ZooWorld.Animals.CollisionResolution;
using ZooWorld.Animals.Factories;
using ZooWorld.Core.Configs;
using ZooWorld.Core.Signals;
using ZooWorld.Gameplay;
using ZooWorld.Gameplay.WorldBounds;
using ZooWorld.UI.Models;
using ZooWorld.UI.ViewModels;
using ZooWorld.UI.Views;

namespace ZooWorld.Core.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private AnimalCatalog _animalCatalog;
        [SerializeField] private TastyTextPool _tastyTextPool;
        [SerializeField] private GameHudView _gameHudView;
    
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<AnimalDiedSignal>();
            Container.DeclareSignal<AnimalEatenSignal>();
            
            Container.BindInstance(_gameConfig);
            Container.BindInstance(_animalCatalog);
            Container.BindInstance(_tastyTextPool);
            Container.BindInstance(_gameHudView);
            
            Container.BindInterfacesAndSelfTo<AnimalDeathStatsModel>().AsSingle();
            Container.Bind<IGameHudViewModel>().To<GameHudViewModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<WorldBoundsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimalSpawner>().AsSingle();
            Container.Bind<IMovementBehaviorFactory>().To<MovementBehaviorFactory>().AsSingle();
            Container.Bind<IAnimalPool>().To<AnimalPool>().AsSingle();
            Container.Bind<IAnimalFactory>().To<AnimalFactory>().AsSingle();
            Container.Bind<ICollisionResolver>().To<AnimalCollisionResolver>().AsSingle();
        }
    }
}
