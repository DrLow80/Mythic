using Spring.Context;
using Spring.Context.Support;

namespace Mythic.Foundation.DependencyInjection
{
    public class SpringContext
    {
        private static volatile SpringContext _instance;
        private static readonly object SyncRoot = new object();
        private readonly IApplicationContext _applicationContext;

        private SpringContext()
        {
            _applicationContext = ContextRegistry.GetContext();
        }

        public static SpringContext Instance
        {
            get
            {
                if (_instance != null) return _instance;

                lock (SyncRoot)
                {
                    if (_instance == null)
                        _instance = new SpringContext();
                }

                return _instance;
            }
        }

        public static T GetObject<T>(string name)
        {
            return Instance._applicationContext.GetObject<T>(name);
        }
    }
}