using _Asteroids.Scripts.Data;
using Unity.Entities;
using UnityEngine;

namespace _Asteroids.Scripts.Behaviours
{
    public class LivesUI : MonoBehaviour
    {
        private TMPro.TextMeshProUGUI _textRef;
        
        private void Start()
        {
            _textRef = GetComponent<TMPro.TextMeshProUGUI>();
        }

        private void Update()
        {
            UpdateLives();
        }

        private void UpdateLives()
        {
            if (!_textRef) return;

            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var playerEntity = PlayerSpawner.PlayerEntity;
            
            if (playerEntity.Equals(Entity.Null)) return;
            if (!entityManager.HasComponent<PlayerLivesData>(playerEntity)) return;

            _textRef.text = "Lives: " + entityManager.GetComponentData<PlayerLivesData>(playerEntity).LivesLeft;
        }
    }
}