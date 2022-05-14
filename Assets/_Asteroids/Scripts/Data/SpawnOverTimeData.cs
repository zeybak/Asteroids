using Unity.Entities;
using UnityEngine;

namespace _Asteroids.Scripts.Data
{
    [GenerateAuthoringComponent]
    public struct SpawnOverTimeData : IComponentData
    {
        [Header("Settings")]
        public Entity EntityToSpawn;
        public float SpawnRate;
        public bool bShouldSpawnOnEdge;
        public float SpawnRateDecreaseOverTime;
        public float MinSpawnRate;
        
        [Header("Debug")]
        public double LastSpawnTime;

        public double TimeAlive;
    }
}