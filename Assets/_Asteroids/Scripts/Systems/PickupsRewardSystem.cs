using _Asteroids.Scripts.Behaviours;
using _Asteroids.Scripts.Data;
using _Asteroids.Scripts.Tags;
using Unity.Entities;

namespace _Asteroids.Scripts.Systems
{
    [UpdateBefore(typeof(DestructionSystem))]
    public class PickupsRewardSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var playerEntity = PlayerSpawner.PlayerEntity;
            
            if (playerEntity.Equals(Entity.Null)) return;

            Entities.ForEach((ref DestroyTag destroyTag,
                ref InvulnerabilityShieldPickupData invulnerabilityShieldPickupData, ref LifeSpanData lifeSpanData) =>
            {
                if (lifeSpanData.TimeLeft <= 0f) return;

                if (!entityManager.HasComponent<PlayerLivesData>(playerEntity)) return;

                var playerLivesData = entityManager.GetComponentData<PlayerLivesData>(playerEntity);
                playerLivesData.InvulnerabilityTimeRemaining += invulnerabilityShieldPickupData.Duration;
                entityManager.SetComponentData(playerEntity, playerLivesData);
            });

            Entities.ForEach((ref DestroyTag destroyTag, ref ProjectileSplitPickupTag projectileSplitPickupTag,
                ref LifeSpanData lifeSpanData) =>
            {
                if (lifeSpanData.TimeLeft <= 0f) return;

                if (!entityManager.HasComponent<PlayerShootingData>(playerEntity)) return;

                var playerShootingData = entityManager.GetComponentData<PlayerShootingData>(playerEntity);
                playerShootingData.BulletAnglesIndex++;

                if (playerShootingData.BulletAnglesIndex >= PlayerShootingData.BulletAngles.Length)
                    playerShootingData.BulletAnglesIndex = PlayerShootingData.BulletAngles.Length - 1;
                
                entityManager.SetComponentData(playerEntity, playerShootingData);
            });
            
            Entities.ForEach((ref DestroyTag destroyTag, ref LifePickupTag lifePickupTag, ref LifeSpanData lifeSpanData) =>
            {
                if (lifeSpanData.TimeLeft <= 0f) return;
                
                if (!entityManager.HasComponent<PlayerLivesData>(playerEntity)) return;

                var playerLivesData = entityManager.GetComponentData<PlayerLivesData>(playerEntity);
                playerLivesData.LivesLeft++;
                entityManager.SetComponentData(playerEntity, playerLivesData);
            });
        }
    }
}