﻿using Unity.Entities;

namespace _Asteroids.Scripts.Data
{
    [GenerateAuthoringComponent]
    public struct PlayerShootingData : IComponentData
    {
        public Entity BulletEntity;
        public float BulletSpeed;
    }
}