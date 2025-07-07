using Avalgame.Views;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System.Text.Json.Serialization;

namespace Avalgame.Models
{
    public class Sprite
    {
        public Sprite(Character character, float rotation = 0, float transparency = 0)
            : this(character, DefaultRect, rotation, transparency) { }
        public Sprite(Bitmap source, float rotation = 0, float transparency = 0)
            : this(source, DefaultRect, rotation, transparency) { }
        public Sprite(Character character, Rect rect, float rotation = 0, float transparency = 0)
            : this(CharacterManager.Instance[character.Name][character.CurrentDifference!], rect, rotation, transparency) { }
        public Sprite(Bitmap source, Rect rect, float rotation, float transparency)
        {
            this.source = source;

            Rect = rect;
            Rotation = rotation;
            Transparency = transparency;
        }

        public static Rect DefaultRect =>
            new(0, -MainWindow.ScreenHeight * .2, MainWindow.ScreenWidth * .4, MainWindow.ScreenHeight * .8);

        private Bitmap? source;
        public void SetSource(Bitmap source) => Img.Source = this.source = source;

        public Rect Rect { get; set; }
        public float Rotation { get; set; }
        public float Transparency { get; set; }

        public string Serialize() => $"{Rotation}|{Transparency}";
        public void Deserialize(string data)
        {
            var token = data.Split('|');
            Rotation = float.Parse(token[0]);
            Transparency = float.Parse(token[1]);
        }

        private Image? img;
        public Image Img => img ??= new Image
        {
            Source = source,

            Width = Rect.Width,
            Height = Rect.Height,
            Stretch = Stretch.Fill,
            Opacity = 1 - Transparency,
            RenderTransform = new TransformGroup
            {
                Children =
                {
                    new TranslateTransform(Rect.Center.X, Rect.Center.Y),
                    new RotateTransform(Rotation),
                },
            },
        };

        public void Show() => GamePageView.Instance!.SpriteCanv.Children.Add(Img);
        public void Hide() => GamePageView.Instance!.SpriteCanv.Children.Remove(Img);
        public void Update()
        {
            Img.Opacity = 1 - Transparency;
            Img.RenderTransform = new TransformGroup
            {
                Children =
                {
                    new TranslateTransform(Rect.Center.X, Rect.Center.Y),
                    new RotateTransform(Rotation),
                },
            };
        }
    }
}
