using _Asteroids.Scripts.Data;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Asteroids.Scripts.Systems
{
    public class EnemiesSpawnerSystem : ComponentSystem
    {
        private Camera _camera;
        
        protected override void OnCreate()
        {
            _camera = Camera.main;
        }
        
        protected override void OnUpdate()
        {
            var bScreenDataFound = false;
            var minHorizontalLocation = 0f;
            var maxHorizontalLocation = 0f;
            var minVerticalLocation = 0f;
            var maxVerticalLocation = 0f;

            if (_camera)
            {
                bScreenDataFound = true;

                var cameraHeight = _camera.transform.position.y;
                var bottomLeftCorner = _camera.ScreenToWorldPoint(new Vector3(0f, 0f, cameraHeight));
                var topRightCorner =
                    _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, cameraHeight));

                minHorizontalLocation = bottomLeftCorner.x;
                minVerticalLocation = bottomLeftCorner.z;

                maxHorizontalLocation = topRightCorner.x;
                maxVerticalLocation = topRightCorner.z;
            }
            else
                _camera = Camera.main;
            
            Entities.ForEach((ref SpawnOverTimeData spawnData, ref Translation translation) =>
            {
                spawnData.TimeAlive += Time.DeltaTime;
                
                var timeSinceLastSpawn = spawnData.TimeAlive - spawnData.LastSpawnTime;
                var bShouldSpawnNewEntity = timeSinceLastSpawn >= spawnData.SpawnRate;

                if (!bShouldSpawnNewEntity) return;
                
                var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                var newEntity = entityManager.Instantiate(spawnData.EntityToSpawn);
                
                spawnData.LastSpawnTime = spawnData.TimeAlive;

                var spawnTranslation = translation;

                if (spawnData.bShouldSpawnOnEdge && bScreenDataFound)
                {
                    var sideToSpawn = Random.Range(0, 4);
                    spawnTranslation.Value = sideToSpawn switch
                    {
                        // left-side
                        0 => new float3(minHorizontalLocation, 0f, Random.Range(minVerticalLocation, maxVerticalLocation)),
                        // right-side
                        1 => new float3(maxHorizontalLocation, 0f, Random.Range(minVerticalLocation, maxVerticalLocation)),
                        // top-side
                        2 => new float3(Random.Range(minHorizontalLocation, maxHorizontalLocation), 0f,
                            maxVerticalLocation),
                        // bottom-side
                        _ => new float3(Random.Range(minHorizontalLocation, maxHorizontalLocation), 0f, minVerticalLocation)
                    };
                }

                entityManager.SetComponentData(newEntity, spawnTranslation);
            });
        }
    }
}