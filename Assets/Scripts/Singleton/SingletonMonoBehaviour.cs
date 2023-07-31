using UnityEngine;

namespace Utils.Singleton
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static bool _quiting;
        private static T _instance;

        public static bool IsInstanceNull => _instance == null;

        public static T Instance
        {
            get
            {
                if (IsInstanceNull)
                {
                    if (_quiting)
                    {
                        Debug.LogWarning($"[Singleton] Экзмемпляр \'{typeof(T)}\' не будет создан. Приложение закрывается.");
                        return default;
                    }

                    _instance = SingletonUtility.Resolve<T>();
                }

                return _instance;
            }
        }

#if UNITY_EDITOR
        protected virtual void Reset()
        {
            if (gameObject.name == "GameObject")
            {
                gameObject.name = $"[Singletone] {GetType().Name}";
            }

            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif

        private void Awake()
        {
            var current = GetComponent<T>();

            if (_instance == null)
            {
                _instance = current;
                OnSingletonAwakened();
            }
            else if (_instance != current)
            {
                Debug.LogWarning($"Обнаружен дублирующий экземпляр: {current}", current);
            }
            OnAwakened();
        }

        /// <summary>
        /// Вызывется в момент задания <see cref="Instance"/> в базовом <see cref="Awake"/>.
        /// </summary>
        protected virtual void OnSingletonAwakened() { }

        /// <summary>
        /// Вызывется в <see cref="Awake"/> по умолчанию.
        /// </summary>
        protected virtual void OnAwakened() { }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
                OnSingletonDestroy();
            }

            OnDestroyed();
        }

        /// <summary>
        /// Вызывается в <see cref="OnDestroy"/> по умолчанию, если уничтожается <see cref="Instance"/>.
        /// <para/>
        /// Вызывается до <see cref="OnDestroyed"/>.
        /// </summary>
        protected virtual void OnSingletonDestroy() { }

        /// <summary>
        /// Вызывается в <see cref="OnDestroy"/> по умолчанию.
        /// </summary>
        protected virtual void OnDestroyed() { }

        protected void OnApplicationQuit()
        {
            _quiting = true;
            OnApplicationQuiting();
        }

        /// <summary>
        /// Вызывается в <see cref="OnApplicationQuit"/> по умолчанию.
        /// </summary>
        protected virtual void OnApplicationQuiting() { }
    }
}
