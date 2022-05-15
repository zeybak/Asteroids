using UnityEngine;

namespace _Asteroids.Scripts.Behaviours
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance => _instance;

        protected virtual bool ShouldDestroyOnLoad => true;

        private static T _instance = null;

        protected virtual void Awake()
        {
            DestroyInstance();

            _instance = this as T;

            if (!ShouldDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        public static void DestroyInstance()
        {
            if (_instance == null)
            {
                return;
            }

            Destroy(_instance);
        }
    }

}