using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DesignPatterns.IoC.Impl
{
    public class ServiceProvider: IServiceProvider
    {
        private readonly Dictionary<Type, object> _singleton;
        private readonly Dictionary<Type, object> _transient;

        public ServiceProvider(
            Dictionary<Type, object> singleton,
            Dictionary<Type, object> transient)
        {
            _singleton = singleton;
            _transient = transient;
        }
        public T GetService<T>()
        {
            return HandleResult<T>();
        }
        
        private T SearchInSingleton<T>()
        {
            try
            {
                if (!_singleton.TryGetValue(typeof(T), out var value) || value != null)
                    return (T) _singleton[typeof(T)];
                
                value = (T) Activator.CreateInstance(typeof(T));
                _singleton[typeof(T)] = value;

                return (T) value;

            }
            catch (Exception exception)
            {
                Debug.Write($"Occured error: {exception.Message}\n");
                return default;
            }
        }

        private T SearchTransient<T>()
        {
            foreach (var (key, value) in _transient)
            {
                if (key == typeof(T) && value == null)
                {
                    return (T) Activator.CreateInstance(typeof(T));
                }
                
                switch (value)
                {
                    case Func<T> factory:
                        return factory.Invoke();
                    case Func<IServiceProvider, T> factory:
                        return factory.Invoke(this);
                }
            }
            return default;
            
        }
        private bool IsSingleton<T>()
        {
            return _singleton.ContainsKey(typeof(T));
        }

        private T HandleResult<T>()
        {
            return IsSingleton<T>() ? SearchInSingleton<T>() : SearchTransient<T>();
        }
        
    }
}