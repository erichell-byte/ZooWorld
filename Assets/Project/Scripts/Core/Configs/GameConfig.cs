using UnityEngine;

namespace ZooWorld.Core.Configs
{
    [CreateAssetMenu(menuName = "ZooWorld/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Spawning")]
        public float MinSpawnInterval;
        public float MaxSpawnInterval;

        [Header("World Bounds")]
        public float CameraDistance = 10f;
        public float BoundsPadding = 2f;

        [Header("Predator Combat")]
        [Range(0f, 1f)] public float PredatorWinProbability = 0.5f;

        [Header("UI")]
        public float TastyTextHeightOffset = 2f;
    }
}
