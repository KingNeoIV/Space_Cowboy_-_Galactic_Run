namespace GalacticRun.Core
{
    public class ServiceProvider
    {
        private readonly Dictionary<Type, object> services = new();

        public void AddService<T>(T service)
        {
            services[typeof(T)] = service!;
        }

        public T Get<T>()
        {
            return (T)services[typeof(T)];
        }
    }
}
