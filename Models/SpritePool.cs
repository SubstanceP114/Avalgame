using Avalgame.Views;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalgame.Models
{
    internal class SpritePool
    {
        public int Capacity { get; init; }
        private readonly SpriteInfo[] sprites;
        public SpriteInfo this[int idx]
        {
            get => sprites[(head + idx) % Capacity];
            set => sprites[(head + idx) % Capacity] = value;
        }
        private int head, tail;
        private int Count => tail - head;
        private double Interval => MainWindow.ScreenWidth / Count;
        public SpritePool(int capacity)
        {
            Capacity = capacity;
            sprites = new SpriteInfo[capacity];
            head = tail = 0;
        }
        public void Clear()
        {
            for (int i = 0; i < Count; i++) this[i].Hide();
            head = tail = 0;
        }
        public bool Replace(SpriteInfo sprite)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Character == sprite.Character)
                {
                    this[i].Hide();
                    sprite.Rect = this[i].Rect;
                    sprite.Rotation = this[i].Rotation;
                    sprite.Transparency = this[i].Transparency;
                    this[i] = sprite;
                    sprite.Update();
                    sprite.Show();
                    return true;
                }
            }
            return false;
        }
        public void Add(SpriteInfo sprite)
        {
            if (Count == Capacity) sprites[head++ % Capacity].Hide();
            sprites[tail++ % Capacity] = sprite;
            sprite.Show();
            Rearrange();
        }
        private void Rearrange()
        {
            int pos = 0;
            void Process(int idx)
            {
                Rect temp = this[idx].Rect;
                this[idx].Rect = new(Interval * (.5 + pos++), temp.Center.Y, temp.Width, temp.Height);
                this[idx].Update();
            }
            for (int i = 0; i < Count; i += 2) Process(i);
            for (int i = Count - Count % 2 - 1; i > 0; i -= 2) Process(i);
        }
    }
}
