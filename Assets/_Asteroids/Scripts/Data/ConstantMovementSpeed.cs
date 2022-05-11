using Unity.Entities;

namespace _Asteroids.Scripts.Data
{
    [GenerateAuthoringComponent]
    public struct ConstantMovementSpeed : IComponentData
    {
        public float Speed;
    }
}