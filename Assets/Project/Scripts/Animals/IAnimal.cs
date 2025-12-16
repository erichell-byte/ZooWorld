using UnityEngine;

namespace ZooWorld.Animals
{
    public interface IAnimal
    {
        string Id { get; }
        AnimalDiet Diet { get; }
        GameObject GameObject { get; }
        Transform Transform { get; }
        bool IsAlive { get; }
        void Initialize();
        void Die();
    }
}
