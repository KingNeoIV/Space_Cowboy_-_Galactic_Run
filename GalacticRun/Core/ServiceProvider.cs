using System;
using System.Collections.Generic;

namespace GalacticRun.Core
{
    /// <summary>
    /// Lightweight dependency container for sharing services across the game.
    ///
    /// The ServiceProvider stores service instances keyed by their type,
    /// allowing screens and systems to request dependencies without needing
    /// direct references. This keeps the architecture modular and reduces
    /// coupling between engine components.
    /// </summary>
    public class ServiceProvider
    {
        // Internal registry of services mapped by their concrete type.
        private readonly Dictionary<Type, object> services = new();

        /// <summary>
        /// Registers a service instance of type T.
        /// If a service of this type already exists, it is replaced.
        /// </summary>
        public void AddService<T>(T service)
        {
            services[typeof(T)] = service!;
        }

        /// <summary>
        /// Retrieves a previously registered service of type T.
        /// Throws a KeyNotFoundException if the service has not been registered.
        /// </summary>
        public T Get<T>()
        {
            return (T)services[typeof(T)];
        }
    }
}
