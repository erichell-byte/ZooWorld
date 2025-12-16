using UnityEngine;
using Zenject;
using ZooWorld.Core.Configs;
using ZooWorld.Gameplay.WorldBounds;

namespace ZooWorld.Animals.Factories
{
    public class AnimalSpawner :
        IInitializable,
        ITickable
    {
        private readonly IAnimalFactory _animalFactory;
        private readonly IWorldBoundsService _worldBounds;
        private readonly AnimalCatalog _animalCatalog;
        private readonly GameConfig _gameConfig;
    
        private float _spawnTimer;
        private bool _isSpawning;
    
        public AnimalSpawner(
            IAnimalFactory animalFactory,
            IWorldBoundsService worldBounds, 
            AnimalCatalog animalCatalog,
            GameConfig gameConfig)
        {
            _animalFactory = animalFactory;
            _worldBounds = worldBounds;
            _animalCatalog = animalCatalog;
            _gameConfig = gameConfig;
        }
    
        public void Initialize()
        {
            StartSpawning();
        }
    
        public void StartSpawning()
        {
            _isSpawning = true;
            _spawnTimer = Random.Range(_gameConfig.MinSpawnInterval, _gameConfig.MaxSpawnInterval);
        }
    
        public void StopSpawning()
        {
            _isSpawning = false;
        }
    
        public void Tick()
        {
            if (!_isSpawning) return;
        
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer <= 0f)
            {
                SpawnRandomAnimal();
                _spawnTimer = Random.Range(_gameConfig.MinSpawnInterval, _gameConfig.MaxSpawnInterval);
            }
        }
    
        private void SpawnRandomAnimal()
        {
            string randomId = _animalCatalog.GetRandomAnimalIdByWeight();
            Vector3 spawnPosition = _worldBounds.GetRandomPositionWithinBounds();
        
            _animalFactory.Create(randomId, spawnPosition);
        }
    }
}
