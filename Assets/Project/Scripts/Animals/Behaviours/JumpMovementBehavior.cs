using System.Collections;
using UnityEngine;
using ZooWorld.Core.Configs;
using ZooWorld.Gameplay.WorldBounds;

namespace ZooWorld.Animals.Behaviours
{
    public class JumpMovementBehavior : IMovementBehavior
    {
        private readonly IWorldBoundsService _worldBounds;
        private readonly JumpSettings _settings;

        private Animal _animal;
        private Rigidbody _rigidbody;
        private Transform _transform;
        private bool _isJumping;
        private float _jumpTimer;

        public JumpMovementBehavior(
            IWorldBoundsService worldBounds,
            JumpSettings settings)
        {
            _worldBounds = worldBounds;
            _settings = settings;
        }

        public void Initialize(Animal animal)
        {
            _animal = animal;
            _rigidbody = animal.GetComponent<Rigidbody>();
            _transform = animal.transform;
            _isJumping = false;
            _jumpTimer = 0f;
        }

        public void Tick()
        {
            if (_animal == null || !_animal.IsAlive)
                return;

            _jumpTimer += Time.deltaTime;
            if (_jumpTimer >= _settings.JumpInterval && !_isJumping)
            {
                _jumpTimer = 0f;
                StartJump();
            }
        }

        public void FixedTick()
        {
            if (_animal == null || !_animal.IsAlive)
                return;

            Vector3 currentPosition = _transform.position;
            Vector3 clampedPosition = _worldBounds.ClampPosition(currentPosition);
            if (currentPosition != clampedPosition)
            {
                _transform.position = clampedPosition;
                Vector3 directionToCenter = (_worldBounds.Center - currentPosition).normalized;
                _rigidbody.velocity = directionToCenter * Mathf.Abs(_rigidbody.velocity.magnitude);
            }
        }

        private void StartJump()
        {
            _isJumping = true;

            Vector3 randomDirection = new Vector3(
                Random.Range(-1f, 1f),
                0f,
                Random.Range(-1f, 1f)
            ).normalized;

            Vector3 targetPosition = _transform.position + randomDirection * _settings.JumpForce;
            targetPosition = _worldBounds.ClampPosition(targetPosition);

            Vector3 jumpVector = targetPosition - _transform.position;
            Vector3 jumpForceVector = new Vector3(
                jumpVector.x,
                _settings.JumpHeight,
                jumpVector.z
            );

            _rigidbody.AddForce(jumpForceVector, ForceMode.Impulse);
            _animal.StartCoroutine(ResetJumpAfterDelay(_settings.JumpCooldown));
        }

        private IEnumerator ResetJumpAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _isJumping = false;
        }
    }
}
