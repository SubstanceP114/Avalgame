using Avalgame.Views;
using Avalonia;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalgame.Models
{
    public class SpritePool
    {
        public int Capacity { get; init; }

        private readonly SpriteInfo[] sprites;
        public static implicit operator SpriteInfo[](SpritePool pool) => pool.sprites;

        private int head, tail;
        private int Count => tail - head;
        private double Interval => MainWindow.ScreenWidth / Count;

        public string[] GetSrcs()
        {
            var srcs = new string[Capacity];
            for (int i = 0; i < Count; i++)
                srcs[i] = $"{sprites[i].Character}/{sprites[i].Difference}";
            return srcs;
        }

        public SpritePool(int capacity)
        {
            Capacity = capacity;
            sprites = new SpriteInfo[capacity];
            head = tail = 0;
        }
        public SpritePool(string[] srcs)
        {
            Capacity = srcs.Length;
            sprites = new SpriteInfo[Capacity];
            head = tail = 0;
            foreach (var src in srcs)
                if (src != null) Add(src);
        }

        public void Clear()
        {
            for (int i = 0; i < Count; i++) sprites[i].Hide();
            head = tail = 0;
        }

        public bool Replace(string src) => Replace(new SpriteInfo(src));
        public bool Replace(SpriteInfo sprite)
        {
            for (int i = 0; i < Count; i++)
            {
                if (sprites[i].Character == sprite.Character)
                {
                    if (sprites[i].Difference == sprite.Difference) return true;

                    sprites[i].Hide();

                    sprite.Rect = sprites[i].Rect;
                    sprite.Rotation = sprites[i].Rotation;
                    sprite.Transparency = sprites[i].Transparency;

                    sprites[i] = sprite;
                    sprite.Update();
                    sprite.Show();

                    return true;
                }
            }
            return false;
        }

        public void Add(string src) => Add(new SpriteInfo(src));
        public void Add(SpriteInfo sprite)
        {
            if (Count == Capacity) sprites[head++ % Capacity].Hide();
            sprites[tail++ % Capacity] = sprite;
            sprite.Show();
            Rearrange();
        }

        public SpriteInfo Get(string src)
        {
            var sprite = src.Split(['/']);
            return sprites.First(s => s.Character == sprite[0] && s.Difference == sprite[1]);
        }

        private void Rearrange()
        {
            int pos = 0;
            void Process(int idx)
            {
                Rect temp = sprites[idx].Rect;
                sprites[idx].Rect = new(Interval * (.5 + pos++) - temp.Width, temp.Top, temp.Width, temp.Height);
                sprites[idx].Update();
            }
            for (int i = 0; i < Count; i += 2) Process(i);
            for (int i = Count - Count % 2 - 1; i > 0; i -= 2) Process(i);
        }
    }
}
