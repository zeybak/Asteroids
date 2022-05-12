using Unity.Entities;

namespace _Asteroids.Scripts.Data
{
    [GenerateAuthoringComponent]
    public struct PlayerLivesData : IComponentData
    {
        public int LivesLeft;
    }
}