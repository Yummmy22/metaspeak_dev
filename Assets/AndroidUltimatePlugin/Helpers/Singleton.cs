using UnityEngine;

namespace Gigadrillgames.GameFrameWork.Utils
{
    public class Singleton<T> where T : class, new()
    {
        class SingletonCreator
        {
            static SingletonCreator()
            {
            }

            internal static readonly T instance_ = new T();
        }

        protected static T instance_;

        public static T Instance
        {
            get { return SingletonCreator.instance_; }
            protected set { instance_ = value; }
        }
    }

    public class SingletonOnDemand<T> where T : class, new()
    {
        protected static T instance_;

        public static T Instance
        {
            get { return instance_; }
            protected set { instance_ = value; }
        }
    }

    public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        private static object _lock = new object();

        public static T Instance
        {
            get
            {
                if (applicationIsQuitting)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                     "' already destroyed on application quit." +
                                     " Won't create again - returning null.");
                    return null;
                }

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (T) FindObjectOfType(typeof(T));

                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            Debug.LogError("[Singleton] Something went really wrong " +
                                           " - there should never be more than 1 singleton!" +
                                           " Reopening the scene might fix it.");
                            return _instance;
                        }

                        if (_instance == null)
                        {
                            GameObject singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();
                            string className = typeof(T).ToString();
                            string[] classPackage = className.Split('.');
                            singleton.name = "(Singleton)" + classPackage[classPackage.Length - 1];
                            
                            Debug.Log("[Singleton] An instance of " + singleton.name + "' was created.");
                        }
                        else
                        {
                            Debug.Log("[Singleton] Using instance already created: " +
                                      _instance.gameObject.name);
                        }
                    }

                    DontDestroyOnLoad(_instance.gameObject);

                    return _instance;
                }
            }
        }

        private static bool applicationIsQuitting = false;

        /// <summary>
        /// When Unity quits, it destroys objects in a random order.
        /// In principle, a Singleton is only destroyed when application quits.
        /// If any script calls Instance after it have been destroyed, 
        ///   it will create a buggy ghost object that will stay on the Editor scene
        ///   even after stopping playing the Application. Really bad!
        /// So, this was made to be sure we're not creating that buggy ghost object.
        /// </summary>
        public void OnDestroy()
        {
            applicationIsQuitting = true;
        }
    }
}