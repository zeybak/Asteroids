using System.ComponentModel;
using _Asteroids.Scripts.Data;
using _Asteroids.Scripts.Tags;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace _Asteroids.Scripts.Systems
{
    public class CollisionsDetectionSystem : JobComponentSystem
    {
        private struct CollisionDetectionJob : ITriggerEventsJob
        {
            [ReadOnly(true)] public EntityCommandBuffer EntityCommandBuffer;
            [ReadOnly(true)] public ComponentDataFromEntity<SpawnOnDestructionData> SpawnOnDestructionEntities;
            [ReadOnly(true)] public ComponentDataFromEntity<Translation> TranslationEntities;
            [ReadOnly(true)] public ComponentDataFromEntity<EnemyTag> EnemyEntities;
            [ReadOnly(true)] public ComponentDataFromEntity<AllyTag> AllyEntities;
            [ReadOnly(true)] public ComponentDataFromEntity<DestroyTag> EntitiesToBeDestroyed;

            public void Execute(TriggerEvent triggerEvent)
            {
                if (!TranslationEntities.HasComponent(triggerEvent.EntityA)) return;
                if (!TranslationEntities.HasComponent(triggerEvent.EntityB)) return;
                
                if (EntitiesToBeDestroyed.HasComponent(triggerEvent.EntityA)) return;
                if (EntitiesToBeDestroyed.HasComponent(triggerEvent.EntityB)) return;
                
                if (EnemyEntities.HasComponent(triggerEvent.EntityA) &&
                    EnemyEntities.HasComponent(triggerEvent.EntityB)) return;
                if (AllyEntities.HasComponent(triggerEvent.EntityA) &&
                    AllyEntities.HasComponent(triggerEvent.EntityB)) return;
                
                void CheckSpawnOnDestruction(ComponentDataFromEntity<SpawnOnDestructionData> spawnOnDestructionEntities, ComponentDataFromEntity<Translation> translationEntities, EntityCommandBuffer entityCommandBuffer, Entity entity)
                {
                    if (!spawnOnDestructionEntities.HasComponent(entity)) return;
                    if (!translationEntities.HasComponent(entity)) return;

                    for (var i = 0; i < spawnOnDestructionEntities[entity].SpawnAmount; i++)
                    {
                        var newEntity = entityCommandBuffer.Instantiate(spawnOnDestructionEntities[entity].EntityToSpawn);
                    
                        entityCommandBuffer.AddComponent(newEntity, new Translation() { Value = translationEntities[entity].Value});
                    }
                }
                
                CheckSpawnOnDestruction(SpawnOnDestructionEntities, TranslationEntities, EntityCommandBuffer, triggerEvent.EntityA);
                CheckSpawnOnDestruction(SpawnOnDestructionEntities, TranslationEntities, EntityCommandBuffer, triggerEvent.EntityB);
                
                EntityCommandBuffer.AddComponent(triggerEvent.EntityA, new DestroyTag());
                EntityCommandBuffer.AddComponent(triggerEvent.EntityB, new DestroyTag());
            }
        }

        private BuildPhysicsWorld _buildPhysicsWorld;
        private StepPhysicsWorld _stepPhysicsWorld;
        private EndSimulationEntityCommandBufferSystem _endSimulationEntityCommandBufferSystem;
        
        protected override void OnCreate()
        {
            _buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
            _stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
            _endSimulationEntityCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var job = new CollisionDetectionJob
            {
                EntityCommandBuffer = _endSimulationEntityCommandBufferSystem.CreateCommandBuffer(),
                SpawnOnDestructionEntities = GetComponentDataFromEntity<SpawnOnDestructionData>(),
                TranslationEntities = GetComponentDataFromEntity<Translation>(),
                EnemyEntities = GetComponentDataFromEntity<EnemyTag>(),
                AllyEntities = GetComponentDataFromEntity<AllyTag>(),
                EntitiesToBeDestroyed = GetComponentDataFromEntity<DestroyTag>()
            };

            var jobHandle = job.Schedule(_stepPhysicsWorld.Simulation, ref _buildPhysicsWorld.PhysicsWorld, inputDeps);
            
            _endSimulationEntityCommandBufferSystem.AddJobHandleForProducer(jobHandle);

            return jobHandle;
        }
    }
}