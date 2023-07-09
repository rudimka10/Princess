namespace Utils.Singleton
{
    public enum SingletonBehaviourMode
    {
        /// <summary>
        /// Using as instance first <see cref="UnityEngine.MonoBehaviour"/> activated or found by <see cref="UnityEngine.GameObject.FindObjectsOfType"/>.
        /// </summary>
        FindExistingOrNull,
        /// <summary>
        /// If instance is <i>null</i> create and mark as <see cref="UnityEngine.GameObject.DontDestroyOnLoad"/>.
        /// </summary>
        CreateIfDoesntExists,
    }
}
