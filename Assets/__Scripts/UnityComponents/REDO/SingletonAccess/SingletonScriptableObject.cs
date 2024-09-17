using UnityEngine;

namespace __Scripts.UnityComponents.REDO.SingletonAccess
{
    public class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        public static T Instance;

        public void CreateInstance()
        {
            Instance = this as T;
        }
    }
}
