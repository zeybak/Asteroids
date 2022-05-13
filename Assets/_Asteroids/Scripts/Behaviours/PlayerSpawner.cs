using Unity.Entities;
using UnityEngine;

namespace _Asteroids.Scripts.Behaviours
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        private Entity _playerEntityToSpawn;

        private BlobAssetStore _blobAssetStore;

        public static Entity PlayerEntity { get; private set; }

        private void Awake()
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            _blobAssetStore = new BlobAssetStore();

            var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, _blobAssetStore);
            _playerEntityToSpawn = GameObjectConversionUtility.ConvertGameObjectHierarchy(_playerPrefab, settings);
            
            PlayerEntity = entityManager.Instantiate(_playerEntityToSpawn);
        }

        private void OnDestroy()
        {
            _blobAssetStore.Dispose();
        }
    }
}