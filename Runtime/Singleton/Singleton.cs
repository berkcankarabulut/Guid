using System;
using UnityEngine;

namespace Project.Utility.Runtime
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        private static bool _isInitialized = false;
        
        [SerializeField] private bool dontDestroyOnLoad = true;
        [SerializeField] private bool autoInitialize = true;

        public event Action OnInitialize;

        public static T Instance
        {
            get
            {
                if (!_isInitialized)
                {
                    Debug.LogWarning($"[{typeof(T).Name}] Singleton has not been initialized.");
                    return null;
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            if (autoInitialize) Initialize();
        }

        protected virtual void Initialize()
        { 
            _instance = this as T;
            _isInitialized = true;

            if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);

            InitializeComplete();
        }

        protected virtual void InitializeComplete()
        {
            OnInitialize?.Invoke();
        }
    }
}