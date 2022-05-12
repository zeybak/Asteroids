using _Asteroids.Scripts.Data;
using _Asteroids.Scripts.Tags;
using Unity.Entities;

namespace _Asteroids.Scripts.Systems
{
    public class LifeSpanSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities
                .WithNone<DestroyTag>()
                .ForEach((Entity entity, ref LifeSpanData lifeSpanData) =>
                {
                    lifeSpanData.TimeLeft -= Time.DeltaTime;

                    if (lifeSpanData.TimeLeft <= 0f)
                        EntityManager.AddComponentData(entity, new DestroyTag());
                });
        }
    }
}