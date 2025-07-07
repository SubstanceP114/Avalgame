using Avalgame.Helpers;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.Collections.Generic;
using System.Linq;

namespace Avalgame.Models
{
    public class Character
    {
        /// <summary>
        /// 唯一标识角色文件路径
        /// </summary>
        public string Path { get; init; }
        public string Name { get; init; }

        /// <summary>
        /// 当前差分名称
        /// </summary>
        public string? CurrentDifference { get; set; }
        /// <summary>
        /// 当前立绘信息
        /// </summary>
        public Sprite? Sprite { get; set; }

        private Dictionary<string, Bitmap> images;

        public Character(string path, string name)
        {
            Path = path;
            Name = name;
            images = [];
        }

        public Bitmap this[string differance] => images[differance];

        public string Serialize() => $"{Name}::{CurrentDifference}::{Sprite!.Serialize()}";
        public void Deserialize(string[] token)
        {
            CurrentDifference = token[0];
            Sprite = new(this[CurrentDifference]);
            Sprite.Deserialize(token[1]);
            Sprite.Show();
        }

        public void Show(string difference)
        {
            if (Sprite == null)
            {
                CurrentDifference = difference;
                Sprite = new(this[difference]);
                Sprite.Show();
                CharacterManager.Instance.Register(this);
            }
            else Sprite.SetSource(this[difference]);
        }
        public void Hide()
        {
            Sprite?.Hide();
            CharacterManager.Instance.Unregister(this);
            Sprite = null;
        }

        /// <summary>
        /// 加载该角色所有立绘
        /// </summary>
        public void LoadImage()
        {
            foreach (var path in AssetLoader.GetAssets(new($"{ImageHelper.SPRITE_PATH}/{Path}"), null))
            {
                var image = path.Segments.Last();
                images.Add(image.Split('.').First(), ImageHelper.LoadSprite(Path, image));
            }
        }
    }
}
