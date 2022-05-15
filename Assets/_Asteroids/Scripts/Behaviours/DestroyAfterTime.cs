using System;
using UnityEngine;

namespace _Asteroids.Scripts.Behaviours
{
    public class DestroyAfterTime : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] private float timeToDestroy = 1f;

        private void OnEnable()
        {
            Invoke(nameof(TryToDestroy), timeToDestroy);
        }

        private void TryToDestroy()
        {
            PoolsManager.Instance?.Destroy(gameObject);
        }
    }
}