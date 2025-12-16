using System;
using Zenject;
using ZooWorld.Animals;
using ZooWorld.Core.Signals;

namespace ZooWorld.UI.Models
{
    public class AnimalDeathStatsModel : IInitializable, IDisposable
    {
        private int _deadPreyCount;
        private int _deadPredatorCount;

        public event Action<int> DeadPreyCountChanged;
        public event Action<int> DeadPredatorCountChanged;

        private readonly SignalBus _signalBus;

        public AnimalDeathStatsModel(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<AnimalDiedSignal>(OnAnimalDied);
        }
        
        private void OnAnimalDied(AnimalDiedSignal signal)
        {
            if (signal.Animal.Diet == AnimalDiet.Prey)
            {
                IncrementPrey();
            }
            else if (signal.Animal.Diet == AnimalDiet.Predator)
            {
                IncrementPredator();
            }
        }

        private void IncrementPrey()
        {
            _deadPreyCount++;
            DeadPreyCountChanged?.Invoke(_deadPreyCount);
        }

        private void IncrementPredator()
        {
            _deadPredatorCount++;
            DeadPredatorCountChanged?.Invoke(_deadPredatorCount);
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<AnimalDiedSignal>(OnAnimalDied);
        }
    }
}
