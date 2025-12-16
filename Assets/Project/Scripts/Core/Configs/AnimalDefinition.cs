using System;
using UnityEngine;
using ZooWorld.Animals;

namespace ZooWorld.Core.Configs
{
    public enum MovementType
    {
        Jump,
        Wander
    }

    [Serializable]
    public class JumpSettings
    {
        public float JumpForce;
        public float JumpInterval;
        public float JumpHeight;
        public float JumpCooldown = 0.5f;
    }

    [Serializable]
    public class WanderSettings
    {
        public float MoveSpeed;
        public float DirectionChangeInterval;
        public float RotationSpeed;
        public float MinMoveSpeedThreshold = 0.1f;
    }

    [CreateAssetMenu(menuName = "ZooWorld/AnimalDefinition")]
    public class AnimalDefinition : ScriptableObject
    {
        public string Id;
        public AnimalDiet Diet;
        public GameObject Prefab;
        [Min(0f)] public float SpawnWeight = 1f;
        public MovementType MovementType;
        public JumpSettings Jump = new();
        public WanderSettings Wander = new();
    }
}
