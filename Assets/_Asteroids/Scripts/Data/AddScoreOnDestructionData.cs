using Unity.Entities;
using UnityEngine;

namespace _Asteroids.Scripts.Data
{
    [GenerateAuthoringComponent]
    public struct AddScoreOnDestructionData : IComponentData
    {
        public int ScoreToAdd;
    }
}