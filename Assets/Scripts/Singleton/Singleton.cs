namespace Utils.Singleton
{
    /// <summary>
    /// Базовый класс для <b>одиночки</b> с ленивой инициализацией.
    /// </summary>
    public abstract class Singleton<T> where T : new()
    {
        private static T _instance;

        public static bool IsInstanceNull => _instance == null;

        public static T Instance
        {
            get
            {
                if (IsInstanceNull)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }
    }
}
