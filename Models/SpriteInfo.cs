using Avalgame.Helpers;
using Avalgame.Views;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Styling;
using System;

namespace Avalgame.Models
{
    public class SpriteInfo
    {
        private const string PATH = "avares://Avalgame/Assets/Images/Characters";
        public SpriteInfo(string src, Rect rect, float rotation, float transparency)
        {
            var temp = src.Split('-');
            Character = temp[0];
            Difference = temp[1];
            Init(rect, rotation, transparency);
        }
        public SpriteInfo(string character, string difference, Rect rect, float rotation, float transparency)
        {
            Character = character;
            Difference = difference;
            Init(rect, rotation, transparency);
        }
        private void Init(Rect rect, float rotation, float transparency)
        {
            Rect = rect;
            Rotation = rotation;
            Transparency = transparency;

            img = new Image
            {
                Source = ImageHelper.LoadFromResource(new Uri($"{PATH}/{Character}/{Difference}")),
                Stretch = Stretch.Fill,
                RenderTransform = new TransformGroup
                {
                    Children =
                    {
                        new TranslateTransform(Rect.Center.X, Rect.Center.Y),
                        new RotateTransform(Rotation),
                    },
                },
            };
        }

        public string Character { get; init; }
        public string Difference { get; init; }

        public Rect Rect { get; set; }
        public float Rotation { get; set; }
        public float Transparency { get; set; }

        private Image? img;

        public void Show() => GamePageView.Instance!.SpriteCanv.Children.Add(img!);
        public void Hide() => GamePageView.Instance!.SpriteCanv.Children.Remove(img!);
    }
}
