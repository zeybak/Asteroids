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
        private const string FireInputName = "Fire1";
        
        protected override void OnUpdate()
        {
            Entities.ForEach((ref PlayerShootingData playerShootingData, ref PlayerMovementData movementData, ref Translation translation) =>
            {
                if (!Input.GetButtonDown(FireInputName)) return;
                
                var playerLocation = new Vector3(translation.Value.x, translation.Value.y, translation.Value.z);
                var forwardDirection = (new Vector3(movementData.MouseInput.x, movementData.MouseInput.y, movementData.MouseInput.z) - playerLocation).normalized;
                    
                var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                var spawnedBulletEntity = entityManager.Instantiate(playerShootingData.BulletEntity);
                entityManager.SetComponentData(spawnedBulletEntity, new Translation { Value = playerLocation });
                entityManager.SetComponentData(spawnedBulletEntity, new PhysicsVelocity
                {
                    Linear = forwardDirection * playerShootingData.BulletSpeed,
                    Angular = new float3(0, 0, 0)
                });
            });
        }
    }
}