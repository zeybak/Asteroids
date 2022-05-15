using _Asteroids.Scripts.Behaviours;
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
        private Camera _camera;

        protected override void OnStartRunning()
        {
            _camera = Camera.main;
        }

        protected override void OnUpdate()
        {
            Entities.ForEach((ref PlayerShootingData playerShootingData, ref PlayerMovementData movementData, ref Translation translation) =>
            {
                if (!Input.GetButton(FireInputName)) return;
                
                if (!_camera)
                {
                    _camera = Camera.main;
                    if (!_camera) return;
                }

                if (Time.ElapsedTime - playerShootingData.LastShootingTime < playerShootingData.Ratio)
                    return;

                playerShootingData.LastShootingTime = Time.ElapsedTime;

                var playerLocation = new Vector3(translation.Value.x, translation.Value.y, translation.Value.z);
                var mouseLocation = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.transform.position.y));
                var forwardDirection = (new Vector3(mouseLocation.x, mouseLocation.y, mouseLocation.z) - playerLocation).normalized;
                var rightDirection = Vector3.Cross(forwardDirection, Vector3.down);
                    
                var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

                for (var i = 0; i < PlayerShootingData.BulletAngles[playerShootingData.BulletAnglesIndex].Length; i++)
                {
                    var linearDirection =
                        forwardDirection * PlayerShootingData.BulletAngles[playerShootingData.BulletAnglesIndex][i].z +
                        rightDirection * PlayerShootingData.BulletAngles[playerShootingData.BulletAnglesIndex][i].x;
                    var spawnedBulletEntity = entityManager.Instantiate(playerShootingData.BulletEntity);
                    
                    entityManager.SetComponentData(spawnedBulletEntity, new Translation { Value = playerLocation });
                    entityManager.SetComponentData(spawnedBulletEntity, new PhysicsVelocity
                    {
                        Linear =  linearDirection * playerShootingData.BulletSpeed,
                        Angular = new float3(0, 0, 0)
                    });
                }

                PoolsManager.Instance?.Instantiate("P_PlayerShootSFX",
                    new Vector3(playerLocation.x, playerLocation.y, playerLocation.z), Quaternion.identity);
            });
        }
    }
}