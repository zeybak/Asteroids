using System.Collections.Generic;
using UnityEngine;

namespace _Asteroids.Scripts.Behaviours
{
    public class PoolsManager : SingletonMonoBehaviour<PoolsManager>
    {
        [Header("Settings")] [SerializeField] private string[] resourcesList;
        
        private readonly Dictionary<string, GameObject> _resourceCache = new Dictionary<string, GameObject>();
        private readonly Dictionary<GameObject, Pool> _pools = new Dictionary<GameObject, Pool>();

        protected override void Awake()
        {
            base.Awake();

            for (var i = 0; i < _resourceCache.Count; i++)
            {
                _resourceCache.Add(resourcesList[i], GetResource(resourcesList[i]));
            }
        }
        
        public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
        {
            var resource = GetResource(prefabId);
            if (resource == null)
            {
                return null;
            }

            var instance = GetOrCreateInstance(resource);

            if (!instance) return null;
            
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.SetActive(true);

            return instance;
        }

        public GameObject Instantiate (GameObject prefab)
        {
            if (!prefab)
            {
                return null;
            }

            var instance = GetOrCreateInstance(prefab);
            if (instance && !instance.gameObject.activeSelf)
            {
                instance.gameObject.SetActive(true);
            }
            return instance;
        }

        public new T Instantiate<T> (T prefab) where T : Object
        {
            if (!prefab)
            {
                return null;
            }

            var prefabToGameObject = prefab as GameObject;
            if(!prefabToGameObject)
            {
                return null;
            }

            var instance = GetOrCreateInstance(prefabToGameObject);
            if (instance && !instance.gameObject.activeSelf)
                instance.gameObject.SetActive(true);

            return instance as T;
        }

        public GameObject Instantiate (GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (!prefab)
                return null;

            var instance = GetOrCreateInstance(prefab);

            if (!instance) return null;
            
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.SetActive(true);

            return instance;
        }

        public GameObject Instantiate (GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            if (!prefab)
                return null;

            var instance = GetOrCreateInstance(prefab);

            if (!instance) return null;
            
            instance.transform.parent = parent;
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.SetActive(true);

            return instance;
        }

        public void Destroy(GameObject gameObject)
        {
            var poolable = gameObject?.GetComponent<Poolable>();
            var pool = poolable?.Pool;
            if (pool != null)
            {
                pool.ReturnObject(gameObject);
                gameObject.SetActive(false);
            }
            else
                GameObject.Destroy(gameObject);
        }

        public void ClearPools()
        {
            foreach (var pool in _pools.Values)
            {
                pool.ClearPool();
            }
            _pools.Clear();
        }

        private GameObject GetOrCreateInstance (GameObject prefab)
        {
            GameObject instance = null;
            Pool pool = null;

            if (_pools.TryGetValue(prefab, out pool))
            {
                instance = pool.RetrieveObject();
            }
            else
            {
                instance = GameObject.Instantiate(prefab);

                var poolableComponent = instance.GetComponent<Poolable>();
                
                if (!poolableComponent) return instance;
                
                pool = new Pool(prefab);
                poolableComponent.Pool = pool;
                _pools.Add(prefab, pool);
            }

            return instance;
        }

        private GameObject GetResource(string prefabId)
        {
            GameObject res = null;
            var cached = _resourceCache.TryGetValue(prefabId, out res);
            
            if (cached) return res;
            
            res = (GameObject)Resources.Load(prefabId, typeof(GameObject));
            if (res == null)
                Debug.LogError("DefaultPool failed to load \"" + prefabId + "\" . Make sure it's in a \"Resources\" folder.");
            else
                _resourceCache.Add(prefabId, res);

            return res;
        }
    }

    public class Pool
    {
        private readonly GameObject _resource = null;
        private readonly Stack<GameObject> _availableObjects = new Stack<GameObject>();
        private readonly HashSet<GameObject> _notAvailableObjects = new HashSet<GameObject>();

        public Pool(GameObject resource)
        {
            _resource = resource;
        }

        public GameObject RetrieveObject()
        {
            if (_resource == null)
            {
                return null;
            }

            GameObject objectToRetrieve = null;
            objectToRetrieve = _availableObjects.Count > 0 ? _availableObjects.Pop() : CreatePoolObject();

            _notAvailableObjects.Add(objectToRetrieve);
            return objectToRetrieve;
        }

        public void ReturnObject(GameObject objectToReturn)
        {
            if (objectToReturn == null)
            {
                return;
            }

            _notAvailableObjects.Remove(objectToReturn);
            _availableObjects.Push(objectToReturn);
        }

        public void ClearPool()
        {
            var objectsToDestroy = new List<GameObject>();

            foreach (var availableObject in _availableObjects)
            {
                if (availableObject == null)
                {
                    continue;
                }
                objectsToDestroy.Add(availableObject);
            }
            foreach (var notAvailableObject in _notAvailableObjects)
            {
                if (notAvailableObject == null)
                {
                    continue;
                }
                objectsToDestroy.Add(notAvailableObject);
            }

            _availableObjects.Clear();
            _notAvailableObjects.Clear();

            for (var i = objectsToDestroy.Count - 1; i >= 0; i--)
            {
                var instance = objectsToDestroy[i];
                objectsToDestroy.RemoveAt(i);
                if (instance)
                {
                    GameObject.Destroy(instance);
                }
            }
        }

        private GameObject CreatePoolObject()
        {
            var newGameObject = GameObject.Instantiate(_resource) as GameObject;

            var poolableComponent = newGameObject?.GetComponent<Poolable>();
            if (poolableComponent != null)
            {
                poolableComponent.Pool = this;
            }
            return newGameObject;
        }
    }
}