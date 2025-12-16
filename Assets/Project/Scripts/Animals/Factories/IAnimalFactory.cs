using UnityEngine;

namespace ZooWorld.Animals.Factories
{
    public interface IAnimalFactory
    {
        IAnimal Create(string animalId, Vector3 position);
    }
}
