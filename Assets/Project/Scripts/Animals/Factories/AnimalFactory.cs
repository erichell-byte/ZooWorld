using UnityEngine;
using ZooWorld.Core.Configs;

namespace ZooWorld.Animals.Factories
{
    public class AnimalFactory : IAnimalFactory
    {
        private readonly AnimalCatalog _animalCatalog;
        private readonly IAnimalPool _animalPool;

        public AnimalFactory(AnimalCatalog animalCatalog, IAnimalPool animalPool)
        {
            _animalCatalog = animalCatalog;
            _animalPool = animalPool;
        }

        public IAnimal Create(string animalId, Vector3 position)
        {
            var definition = _animalCatalog.GetDefinition(animalId);
            var animal = _animalPool.Get(definition);
            animal.Configure(definition);
            animal.GameObject.transform.position = position;
            animal.Initialize();
            return animal;
        }
    }
}
