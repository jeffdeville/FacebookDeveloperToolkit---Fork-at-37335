using System.Collections.Generic;

namespace Facebook.BindingHelper
{
    class FacebookHashTable<TKey, TValue> : Dictionary<TKey, TValue>
    {
        object _lock = new object();
        public object Lock { get { return _lock;} }
        public TValue GetValue(TKey key)
        {
            lock (_lock)
            {
                if (this.ContainsKey(key))
                {
                    return this[key];
                }
                return default(TValue);
            }
        }

        public void SetValue(TKey key, TValue value)
        {
            lock (_lock)
            {
                if (this.ContainsKey(key))
                {
                    this.Remove(key);
                }

                this.Add(key, value);
            }
        }
    }
}
