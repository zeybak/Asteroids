using System;
using _Asteroids.Scripts.Data;
using Unity.Entities;
using UnityEngine;

namespace _Asteroids.Scripts.Behaviours
{
    public class LivesUI : MonoBehaviour
    {
        private TMPro.TextMeshProUGUI _textRef;
        private PlayerSpawner _playerSpawner;
        
        private void Start()
        {
            _textRef = GetComponent<TMPro.TextMeshProUGUI>();
            _playerSpawner = FindObjectOfType<PlayerSpawner>();
        }

        private void Update()
        {
            UpdateLives();
        }

        private void UpdateLives()
        {
            if (!_textRef) return;

            if (!_playerSpawner) return;

            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            if (!entityManager.HasComponent<PlayerLivesData>(_playerSpawner.PlayerEntity)) return;

            _textRef.text = "Lives: " + entityManager.GetComponentData<PlayerLivesData>(_playerSpawner.PlayerEntity).LivesLeft;
        }
    }
}