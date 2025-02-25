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
        #region ctor
        public SpriteInfo(string src, float rotation = 0, float transparency = 0)
        {
            var temp = src.Split('/');
            Character = temp[0];
            Difference = temp[1];
            Init(new Rect(0,
                -MainWindow.ScreenHeight * .2,
                MainWindow.ScreenWidth * .4,
                MainWindow.ScreenHeight * .8),
                rotation, transparency);
        }
        public SpriteInfo(string character, string difference, float rotation = 0, float transparency = 0)
        {
            Character = character;
            Difference = difference;
            Init(new Rect(0,
                -MainWindow.ScreenHeight * .2,
                MainWindow.ScreenWidth * .4,
                MainWindow.ScreenHeight * .8),
                rotation, transparency);
        }
        public SpriteInfo(string src, Rect rect, float rotation = 0, float transparency = 0)
        {
            var temp = src.Split('/');
            Character = temp[0];
            Difference = temp[1];
            Init(rect, rotation, transparency);
        }
        public SpriteInfo(string character, string difference, Rect rect, float rotation = 0, float transparency = 0)
        {
            Character = character;
            Difference = difference;
            Init(rect, rotation, transparency);
        }
        #endregion
        private void Init(Rect rect, float rotation, float transparency)
        {
            Rect = rect;
            Rotation = rotation;
            Transparency = transparency;

            img = new Image
            {
                Source = ImageHelper.LoadSprite(Character, Difference),

                Width = rect.Width,
                Height = rect.Height,
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
        }

        public string Character { get; init; }
        public string Difference { get; init; }

        public Rect Rect { get; set; }
        public float Rotation { get; set; }
        public float Transparency { get; set; }

        private Image? img;

        public void Show() => GamePageView.Instance!.SpriteCanv.Children.Add(img!);
        public void Hide() => GamePageView.Instance!.SpriteCanv.Children.Remove(img!);
        public void Update()
        {
            img!.Opacity = 1 - Transparency;
            img.RenderTransform = new TransformGroup
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
