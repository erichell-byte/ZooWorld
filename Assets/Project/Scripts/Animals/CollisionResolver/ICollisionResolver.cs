using ZooWorld.Animals;

namespace ZooWorld.Animals.CollisionResolution
{
    public interface ICollisionResolver
    {
        void ResolveCollision(IAnimal attacker, IAnimal target);
    }
}
