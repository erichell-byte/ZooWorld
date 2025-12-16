using ZooWorld.Core.Configs;

namespace ZooWorld.Animals.Behaviours.Factory
{
    public interface IMovementBehaviorFactory
    {
        IMovementBehavior Create(AnimalDefinition definition);
    }
}
