using Avalgame.Models;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private static Dictionary<Option, Func<SpriteInfo, Animation>>? anims;
        private static Dictionary<Option, Func<SpriteInfo, Animation>> Anims => anims ??= new()
        {
            { Option.Jump, Jump },
            //{ Option.Nod, Nod },
            //{ Option.Swing, Swing },
        };
        private static Animation Jump(SpriteInfo sprite)
        {
            return new Animation()
            {
                Duration = TimeSpan.FromSeconds(1),
                IterationCount = new(1),
                Children =
                {
                    new KeyFrame
                    {
                        Cue = new(0),
                        Setters =
                        {
                            new Setter(TranslateTransform.YProperty, sprite.Rect.Center.Y),
                        }
                    },
                    new KeyFrame
                    {
                        Cue = new(0.5),
                        Setters =
                        {
                            new Setter(TranslateTransform.YProperty, sprite.Rect.Center.Y - sprite.Rect.Height * .2),
                        }
                    },
                    new KeyFrame
                    {
                        Cue = new(1),
                        Setters =
                        {
                            new Setter(TranslateTransform.YProperty, sprite.Rect.Center.Y),
                        }
                    },
                }
            };
        }
        //private static Animation Nod(Control target)
        //{

        //}
        //private static Animation Swing(Control target)
        //{

        //}
    }
}
