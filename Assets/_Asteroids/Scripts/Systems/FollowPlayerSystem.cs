using _Asteroids.Scripts.Data;
using _Asteroids.Scripts.Tags;
using Unity.Entities;
using Unity.Transforms;

namespace _Asteroids.Scripts.Systems
{
    public class FollowPlayerSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var player = Entity.Null;
            var playerTranslation = new Translation();
            
            Entities.ForEach((Entity entity, ref PlayerMovementData playerMovementData, ref Translation translation) =>
            {
                player = entity;
                playerTranslation = translation;
            });

            Entities.ForEach((Entity entity, ref FollowPlayerTag followPlayerTag, ref Translation translation) =>
            {
                if (player.Equals(Entity.Null)) return;

                translation.Value = playerTranslation.Value;
            });
        }
    }
}