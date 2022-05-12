using Unity.Entities;
using UnityEngine;

namespace _Asteroids.Scripts.Data
{
    [GenerateAuthoringComponent]
    public struct PlayerMovementData : IComponentData
    {
        [Header("Settings")] 
        public float Speed;
    }
}