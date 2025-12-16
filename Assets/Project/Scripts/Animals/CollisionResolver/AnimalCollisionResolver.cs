using UnityEngine;
using Zenject;
using ZooWorld.Core.Configs;
using ZooWorld.Core.Signals;
using ZooWorld.Gameplay;

namespace ZooWorld.Animals.CollisionResolution
{
    public class AnimalCollisionResolver : ICollisionResolver
    {
        private readonly TastyTextPool _tastyTextPool;
        private readonly GameConfig _gameConfig;
        private readonly SignalBus _signalBus;

        public AnimalCollisionResolver(TastyTextPool tastyTextPool, SignalBus signalBus, GameConfig gameConfig)
        {
            _tastyTextPool = tastyTextPool;
            _gameConfig = gameConfig;
            _signalBus = signalBus;
        }

        public void ResolveCollision(IAnimal attacker, IAnimal target)
        {
            if (!attacker.IsAlive || !target.IsAlive)
                return;

            if (attacker.Diet == AnimalDiet.Predator && target.Diet == AnimalDiet.Prey)
            {
                HandlePredatorEatsTarget(attacker, target);
                return;
            }

            if (attacker.Diet == AnimalDiet.Prey && target.Diet == AnimalDiet.Predator)
            {
                HandlePredatorEatsTarget(target, attacker);
                return;
            }

            if (attacker.Diet == AnimalDiet.Predator && target.Diet == AnimalDiet.Predator)
            {
                HandlePredatorVsPredator(attacker, target);
            }
        }

        private void HandlePredatorEatsTarget(IAnimal predator, IAnimal victim)
        {
            if (!victim.IsAlive)
                return;

            victim.Die();
            _tastyTextPool.ShowTastyText(predator.Transform.position);
            _signalBus.Fire(new AnimalEatenSignal(predator, victim));
        }

        private void HandlePredatorVsPredator(IAnimal attacker, IAnimal target)
        {
            bool attackerSurvives = Random.value < _gameConfig.PredatorWinProbability;
            if (attackerSurvives)
            {
                HandlePredatorEatsTarget(attacker, target);
            }
            else
            {
                attacker.Die();
            }
        }
    }
}
