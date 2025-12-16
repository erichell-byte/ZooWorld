using System;
using UnityEngine;
using Zenject;
using ZooWorld.Animals.Behaviours;
using ZooWorld.Animals.Behaviours.Factory;
using ZooWorld.Animals.CollisionResolution;
using ZooWorld.Animals.Factories;
using ZooWorld.Core.Configs;

namespace ZooWorld.Animals
{
    public class Animal : MonoBehaviour, IAnimal, IDisposable
    {
        public string Id => _animalId;
        public AnimalDiet Diet => _animalDiet;
        public GameObject GameObject => gameObject;
        public Transform Transform => transform;
        public bool IsAlive => _lifecycle.IsAlive;

        private string _animalId;
        private AnimalDiet _animalDiet;
        private IMovementBehavior _movementBehavior;
        private Rigidbody _rigidbody;
        private ICollisionResolver _collisionResolver;
        private IMovementBehaviorFactory _movementFactory;
        private AnimalLifecycle _lifecycle;
    
        [Inject]
        public void Construct(
            SignalBus signalBus,
            ICollisionResolver collisionResolver,
            IAnimalPool animalPool,
            IMovementBehaviorFactory movementFactory)
        {
            _collisionResolver = collisionResolver;
            _movementFactory = movementFactory;
            _lifecycle = new AnimalLifecycle(this, signalBus, animalPool);
        }
    
        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        protected virtual void Update()
        {
            _movementBehavior?.Tick();
        }

        protected virtual void FixedUpdate()
        {
            _movementBehavior?.FixedTick();
        }

        protected void OnCollisionEnter(Collision collision)
        {
            if (!IsAlive) return;
            var otherAnimal = collision.gameObject.GetComponent<IAnimal>();
            if (otherAnimal != null)
            {
                _collisionResolver?.ResolveCollision(this, otherAnimal);
            }
        }
        
        public virtual void Initialize()
        {
            _lifecycle.OnSpawned();
            ResetPhysics();
            _movementBehavior?.Initialize(this);
        }
        
        public void Configure(AnimalDefinition definition)
        {
            _animalId = definition.Id;
            _animalDiet = definition.Diet;
            _movementBehavior = _movementFactory.Create(definition);
        }
    
        public virtual void Die()
        {
            if (!IsAlive) return;

            _lifecycle.MarkDead();
        }
    
        public virtual void Dispose()
        {
            Destroy(gameObject);
        }

        private void ResetPhysics()
        {
            if (_rigidbody == null) return;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}
