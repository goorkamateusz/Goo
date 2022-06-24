namespace Goo.Tools.Patterns
{
    public class Singleton<T> where T : Singleton<T>, new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new T();
                return _instance;
            }
        }

        public static bool Initialized => _instance != null;

        protected static void __NullSingleton()
        {
            /// Yea... It's a little hack... but it's needed for unit tests
            _instance = null;
        }
    }
}