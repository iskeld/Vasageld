using System;
using System.Collections;
using System.Collections.Generic;
using EldSharp.Vasageld.Common;

namespace EldSharp.Vasageld.Core
{
    public class SafeVersionsDictionary : IVersionsDictionary
    {
        private readonly Dictionary<AssemblyVersionAttributeType, string> _dictionary;

        public int Count
        {
            get { return _dictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public ICollection<AssemblyVersionAttributeType> Keys
        {
            get { return _dictionary.Keys; }
        }

        public ICollection<string> Values
        {
            get { return _dictionary.Values; }
        }

        public string this[AssemblyVersionAttributeType key]
        {
            get
            {
                string result;
                return _dictionary.TryGetValue(key, out result) ? result : null;
            }
            set { _dictionary[key] = value; }
        }

        public SafeVersionsDictionary()
        {
            _dictionary = new Dictionary<AssemblyVersionAttributeType, string>();
        }

        public IEnumerator<KeyValuePair<AssemblyVersionAttributeType, string>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<AssemblyVersionAttributeType, string> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<AssemblyVersionAttributeType, string> item)
        {
            return _dictionary.ContainsKey(item.Key);
        }

        public void CopyTo(KeyValuePair<AssemblyVersionAttributeType, string>[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        public bool Remove(KeyValuePair<AssemblyVersionAttributeType, string> item)
        {
            return Remove(item.Key);
        }

        public bool ContainsKey(AssemblyVersionAttributeType key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void Add(AssemblyVersionAttributeType key, string value)
        {
            _dictionary[key] = value;
        }

        public bool Remove(AssemblyVersionAttributeType key)
        {
            return _dictionary.Remove(key);
        }

        public bool TryGetValue(AssemblyVersionAttributeType key, out string value)
        {
            return _dictionary.TryGetValue(key, out value);
        }
    }
}