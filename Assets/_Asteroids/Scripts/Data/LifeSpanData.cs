using Unity.Entities;

namespace _Asteroids.Scripts.Data
{
    [GenerateAuthoringComponent]
    public struct LifeSpanData : IComponentData
    {
        public float TimeLeft;
    }
}