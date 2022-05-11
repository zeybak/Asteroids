using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace _Asteroids.Scripts.Data
{
    [GenerateAuthoringComponent]
    public struct PlayerMovementData : IComponentData
    {
        [Header("Settings")] 
        public float Speed;
        
        [Header("Debug")]
        public float HorizontalInput;
        public float VerticalInput;
        public float3 MouseInput;
    }
}