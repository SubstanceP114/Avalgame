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

        public void Add(K key, V value)
        {
            if (dict.TryGetValue(key, out var node))
            {
                list.Remove(node);
                list.AddFirst(node);
            }
            else
            {
                dict.Add(key, list.AddFirst((key, value)));
                if (list.Count > Capacity) RemoveLast();
            }
        }

        public List<K> Keys => list.Select(v => v.Item1).ToList();
        public List<V> Values => list.Select(v => v.Item2).ToList();
        public List<KeyValuePair<K, V>> Pairs => list.Select(n => new KeyValuePair<K, V>(n.Item1, n.Item2)).ToList();

        public bool Contains(K key) => dict.ContainsKey(key);

        public bool TryGet(K key, out V? value)
        {
            var exist = dict.TryGetValue(key, out var node);
            value = exist ? node!.Value.Item2 : default;
            return exist;
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
                RemoveLast();
            }
        }

        private void RemoveLast()
        {
            dict.Remove(list.Last!.Value.Item1);
            list.RemoveLast();
        }
    }
}
