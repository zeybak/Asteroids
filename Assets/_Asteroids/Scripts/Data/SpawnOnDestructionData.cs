using Unity.Entities;

namespace _Asteroids.Scripts.Data
{
    [GenerateAuthoringComponent]
    public struct SpawnOnDestructionData : IComponentData
    {
        public Entity EntityToSpawn;
        public int SpawnAmount;
    }
}