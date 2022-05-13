using Unity.Entities;
using UnityEngine;

namespace _Asteroids.Scripts.Data
{
    [GenerateAuthoringComponent]
    public struct PlayerLivesData : IComponentData
    {
        [Header("Settings")]
        public int LivesLeft;
        public float RespawnInvulnerabilityTime;
        public Entity ShieldEntity;
        
        [Header("Debug")]
        public float InvulnerabilityTimeRemaining;
    }
}