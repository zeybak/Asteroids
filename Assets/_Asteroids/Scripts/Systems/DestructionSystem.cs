using _Asteroids.Scripts.Behaviours;
using _Asteroids.Scripts.Data;
using _Asteroids.Scripts.Tags;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Asteroids.Scripts.Systems
{
    public class DestructionSystem : ComponentSystem
    {
        private const string EnemyDestructionSfxName = "P_EnemyExplosionSFX";
        
        protected override void OnUpdate()
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var bGameOver = false;
            
            Entities.ForEach((Entity entity, ref DestroyTag destroyTag, ref PlayerLivesData playerLivesData, ref Translation translation) =>
            {
                if (playerLivesData.InvulnerabilityTimeRemaining > 0f)
                {
                    entityManager.RemoveComponent<DestroyTag>(entity);
                    return;
                }
                
                playerLivesData.LivesLeft--;

                if (playerLivesData.LivesLeft <= 0) 
                    bGameOver = true;
                else
                {
                    entityManager.RemoveComponent<DestroyTag>(entity);

                    translation.Value = new float3(0, 0, 0);

                    playerLivesData.InvulnerabilityTimeRemaining += playerLivesData.RespawnInvulnerabilityTime;
                }
            });
            
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.TempJob);

            if (bGameOver)
            {
                Entities.ForEach((Entity entity) =>
                {
                    entityCommandBuffer.DestroyEntity(entity);
                });
            }
            else
            {
                Entities.ForEach((ref DestroyTag destroyTag, ref AddScoreOnDestructionData addScoreOnDestructionData) =>
                {
                    Score.CurrentScore += addScoreOnDestructionData.ScoreToAdd;

                    PoolsManager.Instance?.Instantiate(EnemyDestructionSfxName, Vector3.zero, Quaternion.identity);
                });
                
                Entities.ForEach((Entity entity, ref DestroyTag destroyTag) =>
                {
                    entityCommandBuffer.DestroyEntity(entity);
                });
            }
            
            entityCommandBuffer.Playback(World.DefaultGameObjectInjectionWorld.EntityManager);
            entityCommandBuffer.Dispose();

            if (bGameOver)
            {
                if (Score.CurrentScore > Score.HighScore)
                    Score.HighScore = Score.CurrentScore;

                Score.CurrentScore = 0;
                    
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
            }
        }
    }
}