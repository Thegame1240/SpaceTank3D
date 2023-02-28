using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utilities
{
    
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        
        private static bool isShuttingDown = false;
        private static object lockObject = new object();
        private static T instance;

        
        public static T Instance
        {
            get
            {
                if (isShuttingDown)
                {
                    return null;
                }

                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = FindObjectOfType<T>();

                        if (instance == null)
                        {
                            var singletonObject = new GameObject();
                            instance = singletonObject.AddComponent<T>();
                            singletonObject.name = Instance.GetType().Name;
                        }
                    }

                    return instance;
                }
            }
        }

        protected virtual void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        private void OnApplicationQuit()
        {
            isShuttingDown = true;
        }


        private void OnDestroy()
        {
            isShuttingDown = true;
        }
    }
}