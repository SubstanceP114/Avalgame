using StoryTable;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Avalgame.Models
{
    internal class LRUCache<K, V> where K : notnull
    {
        private Dictionary<K, LinkedListNode<(K, V)>> dict;
        private LinkedList<(K, V)> list;

        public int Capacity { get; private set; }

        public int Count => list.Count;

        public LRUCache(int capacity)
        {
            dict = [];
            list = [];

            Capacity = capacity;
        }

        public V? Get(K key)
        {
            if (dict.TryGetValue(key, out var node))
            {
                list.Remove(node);
                list.AddFirst(node);
                return node.Value.Item2;
            }

            Logger.Error($"未找到{key}");
            return default;
        }

        public void Set(K key, V value)
        {
            if (dict.TryGetValue(key, out var node))
            {
                list.Remove(node);
                list.AddFirst(node);
            }
            else
            {
                dict.Add(key, list.AddFirst((key, value)));
                if (list.Count > Capacity) PopLast();
            }
        }

        public List<K> Keys => list.Select(v => v.Item1).ToList();
        public List<V> Values => list.Select(v => v.Item2).ToList();
        public List<KeyValuePair<K, V>> Pairs => list.Select(n => new KeyValuePair<K, V>(n.Item1, n.Item2)).ToList();

        public bool Contains(K key) => dict.ContainsKey(key);

        public bool TryGet(K key, out V? value)
        {
            if (dict.TryGetValue(key, out var node))
            {
                value = node!.Value.Item2;
                list.Remove(node);
                list.AddFirst(node);
                return true;
            }
            value = default;
            return false;
        }

        public void Clear()
        {
            dict.Clear();
            list.Clear();
        }

        public void Resize(int capacity)
        {
            if (Capacity < capacity) Capacity = capacity;
            while (Capacity > capacity)
            {
                Capacity--;
                PopLast();
            }
        }

        public (K, V) PopLast()
        {
            dict.Remove(list.Last!.Value.Item1);
            var last = list.Last.Value;
            list.RemoveLast();
            return last;
        }

        public bool Remove(K key)
        {
            if (!dict.TryGetValue(key, out var node)) return false;
            list.Remove(node);
            dict.Remove(key);
            return true;
        }
    }
}
