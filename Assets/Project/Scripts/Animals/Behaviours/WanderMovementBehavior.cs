using UnityEngine;
using ZooWorld.Core.Configs;
using ZooWorld.Gameplay.WorldBounds;

namespace ZooWorld.Animals.Behaviours
{
    public class WanderMovementBehavior : IMovementBehavior
    {
        private readonly IWorldBoundsService _worldBounds;
        private readonly WanderSettings _settings;

        private Animal _animal;
        private Rigidbody _rigidbody;
        private Transform _transform;
        private Vector3 _currentDirection;
        private float _directionTimer;

        public WanderMovementBehavior(
            IWorldBoundsService worldBounds,
            WanderSettings settings)
        {
            _worldBounds = worldBounds;
            _settings = settings;
        }

        public void Initialize(Animal animal)
        {
            _animal = animal;
            _rigidbody = animal.GetComponent<Rigidbody>();
            _transform = animal.transform;
            _directionTimer = 0f;
            CorrectOrientation();
            ChangeDirection();
        }

        public void Tick()
        {
            if (_animal == null || !_animal.IsAlive)
                return;

            _directionTimer += Time.deltaTime;
            if (_directionTimer >= _settings.DirectionChangeInterval)
            {
                ChangeDirection();
                _directionTimer = 0f;
            }

            Move();
            UpdateRotation();
        }

        public void FixedTick()
        {
            if (_animal == null || !_animal.IsAlive)
                return;

            CheckWorldBounds();
        }

        private void Move()
        {
            _rigidbody.velocity = _currentDirection * _settings.MoveSpeed;
        }

        private void CorrectOrientation()
        {
            _transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }

        private void UpdateRotation()
        {
            if (_rigidbody.velocity.magnitude < _settings.MinMoveSpeedThreshold)
                return;

            Vector3 movementDirection = _rigidbody.velocity.normalized;
            if (movementDirection == Vector3.zero)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(movementDirection) * Quaternion.Euler(90f, 0f, 0f);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation,
                _settings.RotationSpeed * Time.deltaTime);
        }

        private void ChangeDirection()
        {
            _currentDirection = new Vector3(
                Random.Range(-1f, 1f),
                0f,
                Random.Range(-1f, 1f)
            ).normalized;
        }

        private void CheckWorldBounds()
        {
            Vector3 currentPosition = _transform.position;
            if (!_worldBounds.IsWithinBounds(currentPosition))
            {
                _currentDirection = (_worldBounds.Center - currentPosition).normalized;
            }
        }
    }
}
