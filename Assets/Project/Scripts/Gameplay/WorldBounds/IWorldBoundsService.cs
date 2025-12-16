using UnityEngine;

namespace ZooWorld.Gameplay.WorldBounds
{
    public interface IWorldBoundsService
    {
        bool IsWithinBounds(Vector3 position);
        Vector3 ClampPosition(Vector3 position);
        Vector3 GetRandomPositionWithinBounds();
        Vector3 Center { get; }
    }
}
