using _Asteroids.Scripts.Data;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace _Asteroids.Scripts.Systems
{
    public class PlayerShootingSystem : ComponentSystem
    {
        // @TODO: move to data
        private const float SpawnForwardDistance = 0.5f;
        private const float ProjectileSpeed = 10.0f;
        
        protected override void OnUpdate()
        {
            Entities.ForEach((ref PlayerShootingData playerShootingData, ref PlayerInputData inputData, ref Translation translation) =>
            {
                if (!Input.GetMouseButtonDown(0)) return;
                
                var playerLocation = new Vector3(translation.Value.x, translation.Value.y, translation.Value.z);
                var forwardDirection = (new Vector3(inputData.MouseInput.x, inputData.MouseInput.y, inputData.MouseInput.z) - playerLocation).normalized;
                var spawnLocation = playerLocation + forwardDirection * SpawnForwardDistance;
                spawnLocation.y = playerLocation.y;
                    
                var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                var spawnedBulletEntity = entityManager.Instantiate(playerShootingData.bulletEntity);
                entityManager.SetComponentData(spawnedBulletEntity, new Translation { Value = new float3(spawnLocation.x, spawnLocation.y, spawnLocation.z)});
                entityManager.SetComponentData(spawnedBulletEntity, new PhysicsVelocity
                {
                    Linear = forwardDirection * ProjectileSpeed,
                    Angular = new float3(0, 0, 0)
                });
            });
        }
    }
}