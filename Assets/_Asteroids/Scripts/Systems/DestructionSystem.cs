using _Asteroids.Scripts.Tags;
using Unity.Collections;
using Unity.Entities;

namespace _Asteroids.Scripts.Systems
{
    public class DestructionSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.TempJob);

            Entities.ForEach((Entity entity, ref DestroyTag destroyTag) =>
            {
                entityCommandBuffer.DestroyEntity(entity);
            });
            
            entityCommandBuffer.Playback(World.DefaultGameObjectInjectionWorld.EntityManager);
            entityCommandBuffer.Dispose();
        }
    }
}