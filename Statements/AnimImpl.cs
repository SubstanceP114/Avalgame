using Avalgame.Models;
using Avalonia.Animation;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Collections.Generic;

namespace Avalgame.Statements
{
    public partial class AnimStatement
    {
        public enum Option
        {
            Jump,
            Nod,
            Swing,
        }
        private static Dictionary<Option, Func<Sprite, Animation>>? anims;
        private static Dictionary<Option, Func<Sprite, Animation>> Anims => anims ??= new()
        {
            { Option.Jump, Jump },
            { Option.Nod, Nod },
            { Option.Swing, Swing },
        };
        private static Animation Jump(Sprite target) => new()
        {
            Duration = TimeSpan.FromSeconds(.4),
            IterationCount = new(1),
            Children =
            {
                new KeyFrame
                {
                    Cue = new(0),
                    Setters =
                    {
                        new Setter(TranslateTransform.YProperty, target.Rect.Center.Y),
                    }
                },
                new KeyFrame
                {
                    Cue = new(.5),
                    Setters =
                    {
                        new Setter(TranslateTransform.YProperty, target.Rect.Center.Y - target.Rect.Height * .1),
                    }
                },
                new KeyFrame
                {
                    Cue = new(1),
                    Setters =
                    {
                        new Setter(TranslateTransform.YProperty, target.Rect.Center.Y),
                    }
                },
            }
        };
        private static Animation Nod(Sprite target) => new()
        {
            Duration = TimeSpan.FromSeconds(.4),
            IterationCount = new(2),
            Children =
            {
                new KeyFrame
                {
                    Cue = new(0),
                    Setters =
                    {
                        new Setter(TranslateTransform.YProperty, target.Rect.Center.Y),
                    }
                },
                new KeyFrame
                {
                    Cue = new(.5),
                    Setters =
                    {
                        new Setter(TranslateTransform.YProperty, target.Rect.Center.Y + target.Rect.Height * .1),
                    }
                },
                new KeyFrame
                {
                    Cue = new(1),
                    Setters =
                    {
                        new Setter(TranslateTransform.YProperty, target.Rect.Center.Y),
                    }
                },
            }
        };
        private static Animation Swing(Sprite target) => new()
        {
            Duration = TimeSpan.FromSeconds(.8),
            IterationCount = new(2),
            Children =
            {
                new KeyFrame
                {
                    Cue = new(0),
                    Setters =
                    {
                        new Setter(TranslateTransform.XProperty, target.Rect.Center.X),
                    }
                },
                new KeyFrame
                {
                    Cue = new(.25),
                    Setters =
                    {
                        new Setter(TranslateTransform.XProperty, target.Rect.Center.X + target.Rect.Width * .2),
                    }
                },
                new KeyFrame
                {
                    Cue = new(.75),
                    Setters =
                    {
                        new Setter(TranslateTransform.XProperty, target.Rect.Center.X - target.Rect.Width * .2),
                    }
                },
                new KeyFrame
                {
                    Cue = new(1),
                    Setters =
                    {
                        new Setter(TranslateTransform.XProperty, target.Rect.Center.X),
                    }
                },
            }
        };
    }
}
