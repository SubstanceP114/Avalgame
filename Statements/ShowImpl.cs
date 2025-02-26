using Avalonia.Animation;
using StoryTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.Styling;
using Avalgame.Views;
using Avalonia.Controls;
using Avalonia.Animation.Easings;

namespace Avalgame.Statements
{
    public partial class ShowStatement : Statement
    {
        private static Animation ShowDialog => new Animation
        {
            Duration = TimeSpan.FromSeconds(3),
            IterationCount = new(1),
            Children =
            {
                new KeyFrame
                {
                    Cue = new(0),
                    Setters =
                    {
                        new Setter(TranslateTransform.YProperty, GamePageView.Instance!.DialogCanv.Height),
                        new Setter(Avalonia.Visual.OpacityProperty, 0.0),
                    }
                },
                new KeyFrame
                {
                    Cue= new(1),
                    Setters =
                    {
                        new Setter(TranslateTransform.YProperty, 0),
                        new Setter(Avalonia.Visual.OpacityProperty, 1.0),
                    },
                    KeySpline = new(.4, 0, .2, 1)
                }
            }
        };
    }
}
