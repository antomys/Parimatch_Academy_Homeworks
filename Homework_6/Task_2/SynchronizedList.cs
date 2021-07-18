using System.Collections.Generic;

# nullable enable

namespace Task_2
{
    public class SynchronizedList<T>
    {
        private readonly IList<T> _list = new List<T>();
        private readonly object _sync = new();

        public void Add(T item)
        {
            lock (_sync)
            {
                _list.Add(item);
            }
        }
        public int CountAsync()
        {
            lock (_sync)
            {
                return _list.Count;
            }
        }
        public List<T> Clone()
        {
            lock (_sync)
            {
                return new List<T>(_list);
            }
        }
    }
}