using Unity.Entities;

namespace _Asteroids.Scripts.Data
{
    [GenerateAuthoringComponent]
    public struct ConstantMovementSpeedData : IComponentData
    {
        public float Speed;
    }
}