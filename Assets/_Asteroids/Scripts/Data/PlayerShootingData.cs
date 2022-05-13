using Unity.Entities;
using UnityEngine;

namespace _Asteroids.Scripts.Data
{
    [GenerateAuthoringComponent]
    public struct PlayerShootingData : IComponentData
    {
        [Header("Settings")]
        public Entity BulletEntity;
        public float BulletSpeed;
        
        [Header("Debug")]
        public int BulletAnglesIndex;
        
        public static readonly Vector3[][] BulletAngles = new Vector3[][]
        {
            new Vector3[]
            {
                new Vector3(0f, 0f, 1f)
            },
            new Vector3[]
            {
                new Vector3(0.1f, 0f, 0.9f),
                new Vector3(-0.1f, 0f, 0.9f)
            },
            new Vector3[]
            {
                new Vector3(0f, 0f, 1f),
                new Vector3(0.5f, 0f, 0.5f),
                new Vector3(-0.5f, 0f, 0.5f)
            },
            new Vector3[]
            {
                new Vector3(0f, 0f, 1f),
                new Vector3(0.25f, 0f, 0.75f),
                new Vector3(-0.25f, 0f, 0.75f),
                new Vector3(0.5f, 0f, 0.5f),
                new Vector3(-0.5f, 0f, 0.5f)
            }
        };
    }
}