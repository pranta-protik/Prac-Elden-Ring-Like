using UnityEngine;

namespace ERL
{
    public class DestroyOnLoadSingleton<T> : MonoBehaviour where T : Component 
    {
        public static T Instance {get; private set;}

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this as T;
        }
    }
}
