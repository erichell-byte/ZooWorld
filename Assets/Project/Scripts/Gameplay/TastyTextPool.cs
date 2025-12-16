using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZooWorld.Core.Configs;

namespace ZooWorld.Gameplay
{
    public class TastyTextPool : MonoBehaviour
    {
        [SerializeField] private GameObject _tastyTextPrefab;
        [SerializeField] private float _displayDuration = 1f;
    
        private DiContainer _diContainer;
        private GameConfig _gameConfig;
        private readonly Queue<GameObject> _tastyTextPool = new();
    
        [Inject]
        public void Construct(DiContainer container, GameConfig gameConfig)
        {
            _diContainer = container;
            _gameConfig = gameConfig;
        }
    
        public void ShowTastyText(Vector3 position)
        {
            var tastyTextInstance = GetOrCreateTastyText();
            tastyTextInstance.transform.position = position + Vector3.up * _gameConfig.TastyTextHeightOffset;

            StartCoroutine(DisableAfterDelay(tastyTextInstance, _displayDuration));
        }

        private IEnumerator DisableAfterDelay(GameObject tastyTextInstance, float delay)
        {
            yield return new WaitForSeconds(delay);
            ReturnToPool(tastyTextInstance);
        }

        private GameObject GetOrCreateTastyText()
        {
            if (_tastyTextPool.Count > 0)
            {
                var pooledInstance = _tastyTextPool.Dequeue();
                pooledInstance.SetActive(true);
                return pooledInstance;
            }

            return _diContainer.InstantiatePrefab(_tastyTextPrefab);
        }

        private void ReturnToPool(GameObject tastyTextInstance)
        {
            tastyTextInstance.SetActive(false);
            _tastyTextPool.Enqueue(tastyTextInstance);
        }
    }
}
