using System;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils.Singleton
{
    internal static class SingletonUtility
    {
        internal static T FindExistingInstance<T>() where T : MonoBehaviour
        {
            var targets = Object.FindObjectsOfType<T>();

            if (targets.Length == 0) return null;
            if (targets.Length == 1) return targets[0];

            Debug.LogWarning($"[Singleton] На сцене присутствует более одного экземпляра {typeof(T).Name}");

            for (int i = 0; i < targets.Length; i++)
            {
                Debug.LogWarning($"[Singleton] <b>Дублирующий</b> экземпляр - {targets[i].name}.", targets[i]);
            }

            return targets[0];
        }

        internal static T CreateNewInstance<T>() where T : MonoBehaviour
        {
            var go = new GameObject($"[Singleton] {typeof(T).Name}");
            return go.AddComponent<T>();
        }

        internal static TTarget Resolve<TTarget>() where TTarget : MonoBehaviour
        {
            var singletonType = typeof(TTarget);
            var mode = singletonType.GetCustomAttribute<SingletonBehaviourAttribute>()?.BehaviourMode ?? SingletonBehaviourMode.FindExistingOrNull;

            switch (mode)
            {
                case SingletonBehaviourMode.FindExistingOrNull:
                    return FindExistingInstance<TTarget>();
                case SingletonBehaviourMode.CreateIfDoesntExists:
                    return FindExistingInstance<TTarget>() ?? CreateNewInstance<TTarget>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, "Unknown singleton mode.");
            }
        }
    }
}
