using Zenject;
using ZooWorld.Animals.Factories;
using ZooWorld.Core.Signals;

namespace ZooWorld.Animals
{
    public class AnimalLifecycle
    {
        public bool IsAlive { get; private set; } = true;

        private readonly Animal _owner;
        private readonly SignalBus _signalBus;
        private readonly IAnimalPool _animalPool;

        public AnimalLifecycle(Animal owner, SignalBus signalBus, IAnimalPool animalPool)
        {
            _owner = owner;
            _signalBus = signalBus;
            _animalPool = animalPool;
        }

        public void OnSpawned()
        {
            IsAlive = true;
            _owner.gameObject.SetActive(true);
        }

        public void MarkDead()
        {
            if (!IsAlive)
                return;

            IsAlive = false;
            _signalBus.Fire(new AnimalDiedSignal(_owner));
            _owner.StopAllCoroutines();
            _animalPool?.Despawn(_owner);
        }
    }
}
