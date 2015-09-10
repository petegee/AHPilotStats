using System.Collections.Generic;

namespace My2Cents.HTC.AHPilotStats.Collections
{
    public class CaseInsensitiveDictionary<TValue>
    {
        private readonly Dictionary<string, TValue> _internalDictionary = new Dictionary<string, TValue>();

        public int Count
        {
            get { return _internalDictionary.Count; }
        }

        public TValue this[string key]
        {
            get { return _internalDictionary[key.ToLower()]; }
            set { _internalDictionary[key.ToLower()] = value; }
        }

        public void Add(string key, TValue value)
        {
            _internalDictionary.Add(key.ToLower(), value);
        }

        public bool ContainsKey(string key)
        {
            return _internalDictionary.ContainsKey(key.ToLower());
        }

        public bool Remove(string key)
        {
            return _internalDictionary.Remove(key.ToLower());
        }
    }
}