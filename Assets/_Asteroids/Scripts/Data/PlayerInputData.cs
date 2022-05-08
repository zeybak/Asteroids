using Unity.Entities;
using Unity.Mathematics;

namespace _Asteroids.Scripts.Data
{
    [GenerateAuthoringComponent]
    public struct PlayerInputData : IComponentData
    {
        public float HorizontalInput;
        public float VerticalInput;
        public float3 MouseInput;
    }
}