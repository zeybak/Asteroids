using Unity.Entities;

namespace _Asteroids.Scripts.Data
{
    [GenerateAuthoringComponent]
    public struct AddScoreOnDestructionData : IComponentData
    {
        public int ScoreToAdd;
    }
}