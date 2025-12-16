using System;
using ZooWorld.Core.Configs;
using ZooWorld.Gameplay.WorldBounds;

namespace ZooWorld.Animals.Behaviours.Factory
{
    public class MovementBehaviorFactory : IMovementBehaviorFactory
    {
        private readonly IWorldBoundsService _worldBounds;

        public MovementBehaviorFactory(IWorldBoundsService worldBounds)
        {
            _worldBounds = worldBounds;
        }

        public IMovementBehavior Create(AnimalDefinition definition) 
        {
            return definition.MovementType switch
            {
                MovementType.Jump => new JumpMovementBehavior(_worldBounds, definition.Jump),
                MovementType.Wander => new WanderMovementBehavior(_worldBounds, definition.Wander),
                _ => throw new ArgumentOutOfRangeException(nameof(definition.MovementType), definition.MovementType, null)
            };
        }
    }
}
