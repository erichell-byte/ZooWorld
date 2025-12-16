namespace ZooWorld.Animals.Behaviours
{
    public interface IMovementBehavior
    {
        void Initialize(Animal animal);
        void Tick();
        void FixedTick();
    }
}
