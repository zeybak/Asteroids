using _Asteroids.Scripts.Data;
using _Asteroids.Scripts.Tags;
using Unity.Collections;
using Unity.Entities;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Asteroids.Scripts.Systems
{
    public class DestructionSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var bGameOver = false;
            
            // @TODO: Implement lives system and end game screen
            Entities.ForEach((ref DestroyTag destroyTag, ref PlayerMovementData playerMovementData) =>
            {
                bGameOver = true;
            });
            
            Entities.ForEach((ref DestroyTag destroyTag, ref AddScoreOnDestructionData addScoreOnDestructionData) =>
            {
                Score.AddScore(addScoreOnDestructionData.ScoreToAdd);
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
                Entities.ForEach((Entity entity, ref DestroyTag destroyTag) =>
                {
                    entityCommandBuffer.DestroyEntity(entity);
                });   
            }
            
            entityCommandBuffer.Playback(World.DefaultGameObjectInjectionWorld.EntityManager);
            entityCommandBuffer.Dispose();

            if (bGameOver) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }
}