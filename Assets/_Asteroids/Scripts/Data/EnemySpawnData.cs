using Unity.Entities;
using UnityEngine;

namespace _Asteroids.Scripts.Data
{
    [GenerateAuthoringComponent]
    public struct EnemySpawnData : IComponentData
    {
        [Header("Settings")]
        public Entity EnemyToSpawn;
        public float SpawnRate;
        
        [Header("Debug")]
        public double LastSpawnTime;
    }
}