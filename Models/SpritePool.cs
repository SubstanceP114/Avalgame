using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalgame.Models
{
    internal class SpritePool
    {
        private readonly int capacity;
        private SpriteInfo[] sprites;
        public SpritePool(int capacity)
        {
            this.capacity = capacity;
            sprites = new SpriteInfo[capacity];
        }
    }
}
