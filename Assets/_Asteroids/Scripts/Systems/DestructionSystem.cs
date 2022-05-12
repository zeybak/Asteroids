using _Asteroids.Scripts.Data;
using _Asteroids.Scripts.Tags;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.SceneManagement;

namespace _Asteroids.Scripts.Systems
{
    public class DestructionSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var bGameOver = false;
            
            // @TODO: Implement lives system and end game screen
            Entities.ForEach((Entity entity, ref DestroyTag destroyTag, ref PlayerLivesData playerLivesData, ref Translation translation) =>
            {
                playerLivesData.LivesLeft--;

                if (playerLivesData.LivesLeft <= 0) 
                    bGameOver = true;
                else
                {
                    var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

                    entityManager.RemoveComponent<DestroyTag>(entity);

                    translation.Value = new float3(0, 0, 0);
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