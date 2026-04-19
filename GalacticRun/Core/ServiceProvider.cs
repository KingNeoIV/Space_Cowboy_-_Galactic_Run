using System;
using System.Collections.Generic;

namespace GalacticRun.Core
{
    /*  
        Lightweight dependency container for sharing services across the game.

        The ServiceProvider stores service instances keyed by their type,
        allowing screens and systems to request dependencies without needing
        direct references. This keeps the architecture modular and reduces
        coupling between engine components.
    */
    public class ServiceProvider
    {
        // Internal registry of services mapped by their concrete type.
        private readonly Dictionary<Type, object> services = new();

        // Registers a service instance of type T.
        public void AddService<T>(T service)
        {
            services[typeof(T)] = service!;
        }

        // Retrieves a previously registered service of type T.
        public T Get<T>()
        {
            return (T)services[typeof(T)];
        }
    }
}
