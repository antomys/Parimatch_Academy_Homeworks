using System;
using System.Collections.Generic;

namespace DesignPatterns.IoC.Impl
{
    public class ServiceCollection : IServiceCollection
    {
        private readonly Dictionary<Type, object> _singleton = new Dictionary<Type, object>();
        private readonly Dictionary<Type, object> _transient = new Dictionary<Type, object>();
        

        public IServiceCollection AddTransient<T>()
        {
            _transient.Add(typeof(T),null);
            return this;
        }

        public IServiceCollection AddTransient<T>(Func<T> factory)
        {
            if (!ContainsValue(_transient, factory))
            {
                _transient.Add(typeof(T), factory);
            }
                
            return this;
        }

        public IServiceCollection AddTransient<T>(Func<IServiceProvider, T> factory)
        {
            if (!ContainsValue(_transient, factory))
            {
                _transient.Add(typeof(T), factory);
            }
                
            return this;
        }

        public IServiceCollection AddSingleton<T>()
        {
            _singleton.Add(typeof(T),null);
            return this;
        }

        public IServiceCollection AddSingleton<T>(T service)
        {

            if (!ContainsValue(_singleton, service))
            {
                _singleton.Add(typeof(T),service);
            }
                
            return this;
        }

        public IServiceCollection AddSingleton<T>(Func<T> factory)
        {
            var process = factory.Invoke();
            if (!ContainsValue(_singleton, process))
            {
                _singleton.Add(typeof(T),process);
            }
                
            return this;
        }

        public IServiceCollection AddSingleton<T>(Func<IServiceProvider, T> factory)
        {
            var process = factory.Invoke(BuildServiceProvider());
            if (!ContainsValue(_singleton, process))
            {
                _singleton.Add(typeof(T),process);
            }
            return this;
        }

        public IServiceProvider BuildServiceProvider()
        {
            return new ServiceProvider(_singleton,_transient);
        }

        private static bool ContainsValue(Dictionary<Type,object> dictionary, object value)
        {
            return dictionary.ContainsValue(value);
        }
    }
}