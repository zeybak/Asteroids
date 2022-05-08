using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;

namespace _Asteroids.Scripts.Systems
{
    public class CollisionsDetectionSystem : JobComponentSystem
    {
        [BurstCompile]
        private struct CollisionDetectionJob : ITriggerEventsJob
        {
            public EntityCommandBuffer EntityCommandBuffer;
            
            public void Execute(TriggerEvent triggerEvent)
            {
                EntityCommandBuffer.DestroyEntity(triggerEvent.EntityA);
                EntityCommandBuffer.DestroyEntity(triggerEvent.EntityB);
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
            var job = new CollisionDetectionJob { EntityCommandBuffer = _endSimulationEntityCommandBufferSystem.CreateCommandBuffer() };

            var jobHandle = job.Schedule(_stepPhysicsWorld.Simulation, ref _buildPhysicsWorld.PhysicsWorld, inputDeps);
            
            _endSimulationEntityCommandBufferSystem.AddJobHandleForProducer(jobHandle);

            return jobHandle;
        }
    }
}