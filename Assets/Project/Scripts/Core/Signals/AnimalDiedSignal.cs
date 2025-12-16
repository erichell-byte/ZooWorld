using ZooWorld.Animals;

namespace ZooWorld.Core.Signals
{
    public class AnimalDiedSignal
    {
        public IAnimal Animal { get; }
    
        public AnimalDiedSignal(IAnimal animal)
        {
            Animal = animal;
        }
    }
}
