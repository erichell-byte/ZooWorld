using System.Collections.Generic;
using Zenject;
using ZooWorld.Core.Configs;

namespace ZooWorld.Animals.Factories
{
    public class AnimalPool : IAnimalPool
    {
        private readonly DiContainer _container;
        private readonly Dictionary<string, Stack<Animal>> _pool = new();

        public AnimalPool(DiContainer container)
        {
            _container = container;
        }

        public Animal Get(AnimalDefinition definition)
        {
            if (_pool.TryGetValue(definition.Id, out var stack) && stack.Count > 0)
            {
                var pooled = stack.Pop();
                pooled.gameObject.SetActive(true);
                return pooled;
            }

            var instance = _container.InstantiatePrefab(definition.Prefab);
            var animalComponent = instance.GetComponent<Animal>() ?? instance.GetComponentInChildren<Animal>();
            if (animalComponent == null)
            {
                throw new System.Exception($"Prefab '{definition.Prefab.name}' does not contain an Animal component.");
            }
            return animalComponent;
        }

        public void Despawn(Animal animal)
        {
            if (animal == null)
                return;

            if (!_pool.TryGetValue(animal.Id, out var stack))
            {
                stack = new Stack<Animal>();
                _pool[animal.Id] = stack;
            }

            animal.gameObject.SetActive(false);
            stack.Push(animal);
        }
    }
}
