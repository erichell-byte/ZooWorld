using ZooWorld.Core.Configs;

namespace ZooWorld.Animals.Factories
{
    public interface IAnimalPool
    {
        Animal Get(AnimalDefinition definition);
        void Despawn(Animal animal);
    }
}
