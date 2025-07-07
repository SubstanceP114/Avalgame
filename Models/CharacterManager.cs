using Avalgame.Views;
using Avalonia.Platform;
using StoryTable;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Avalgame.Models
{
    public class CharacterManager
    {
        private static CharacterManager? instance;
        public static CharacterManager Instance => instance ??= new();

        private const string CONFIG_RES = "avares://Avalgame/Assets/Configs/characters.txt";

        private readonly Dictionary<string, Character> characters;

        public LRUCache<Character, int> current;

        private double Interval => MainWindow.ScreenWidth / current.Count;

        private CharacterManager()
        {
            characters = [];
            current = new(2);

            // 暂时先懒加载
            LoadCharacter();
            LoadImage();
        }

        public List<string> Serialize()
        {
            List<string> list = [current.Capacity.ToString()];
            current.Pairs.ToList().ForEach(p => list.Add($"{p.Value}::{p.Key.Serialize()}"));
            return list;
        }
        public void Deserialize(List<string> data)
        {
            Clear();
            Resize(int.Parse(data[0]));
            data[1..].ForEach(s =>
            {
                var token = s.Split("::");
                var character = characters[token[1]];
                character.Deserialize(token[2..]);
                current.Set(character, int.Parse(token[0]));
            });
            Update();
        }

        public void Register(Character character)
        {
            var idx = 0;
            for (; idx < current.Capacity; idx++)
                if (!current.Values.Contains(idx)) break;

            if (idx == current.Capacity)
            {
                var last = current.PeekLast();
                last.Item1.Hide();
                idx = last.Item2;
            }

            current.Set(character, idx);
            Update();
        }
        public bool Unregister(Character character) => current.Remove(character);

        public Character this[string name] => characters[name];

        public bool TryGet(string name, out Character? character) => characters.TryGetValue(name, out character);
        public Character? Get(string name)
        {
            if (characters.TryGetValue(name, out var character)) return character;
            Logger.Error($"找不到角色{name}");
            return null;
        }

        public void Clear()
        {
            current.Keys.ToList().ForEach(c => c.Hide());
            current.Clear();
        }

        public void Resize(int size)
        {
            current.Resize(size);
            Update();
        }

        public void Update()
        {
            var arr = new Character[current.Count];
            current.Pairs.ToList().ForEach(p => arr[p.Value] = p.Key);

            int pos = 0;
            void Process(int idx)
            {
                var character = arr[idx];
                if (character == null) return;

                var temp = character.Sprite!.Rect;
                character.Sprite.Rect = new(Interval * (.5 + pos++) - temp.Width, temp.Top, temp.Width, temp.Height);
                character.Sprite.Update();
            }

            for (int i = 0; i < current.Count; i += 2) Process(i);
            for (int i = current.Count - current.Count % 2 - 1; i > 0; i -= 2) Process(i);
        }

        #region Load
        /// <summary>
        /// 根据<see cref="CONFIG_RES"/>加载角色名称
        /// </summary>
        public void LoadCharacter()
        {
            using StreamReader sr = new(AssetLoader.Open(new(CONFIG_RES)));
            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                var cfg = line.Split(',');
                characters.Add(cfg[1], new Character(cfg[0], cfg[1]));
            }
        }

        /// <summary>
        /// 一次性加载所有角色的所有立绘
        /// </summary>
        public void LoadImage()
        {
            foreach (var chara in characters.Values) LoadImageByCharacter(chara);
        }
        /// <summary>
        /// 加载指定角色的所有立绘
        /// </summary>
        /// <param name="character">指定角色</param>
        public void LoadImageByCharacter(Character character) => character.LoadImage();
        #endregion
    }
}
