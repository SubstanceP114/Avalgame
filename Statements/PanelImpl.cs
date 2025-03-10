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
    public partial class PanelStatement : Statement
    {
        public enum Option
        {
            Text,
        }
        private static Dictionary<Option, PanelAnim>? panels;
        private static Dictionary<Option, PanelAnim> Panels => panels ??= new()
        {
            { Option.Text, Text },
        };

        private static readonly PanelAnim Text = new()
        {
            Target = GamePageView.Instance!.TextCanv,
            #region ShowDialog
            Show = new()
            {
                Anim = new Animation
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
                                new Setter(TranslateTransform.YProperty,
                                    GamePageView.Instance !.NameCanv.Height +
                                    GamePageView.Instance !.DialogCanv.Height),
                                new Setter(Avalonia.Visual.OpacityProperty, 0.0),
                            }
                        },
                        new KeyFrame
                        {
                            Cue= new(1),
                            Setters =
                            {
                                new Setter(TranslateTransform.YProperty, 0.0),
                                new Setter(Avalonia.Visual.OpacityProperty, 1.0),
                            },
                            KeySpline = new(.4, 0, .2, 1)
                        }
                    }
                },
                Start = target =>
                {
                    target.IsEnabled = true;
                    target.RenderTransform = new TranslateTransform(0, 0);
                },
                End = target =>
                {
                    target.Opacity = 1.0;
                }
            },
            #endregion
            #region HideDialog
            Hide = new()
            {
                Anim = new Animation
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
                                new Setter(TranslateTransform.YProperty, 0.0),
                                new Setter(Avalonia.Visual.OpacityProperty, 1.0),
                            }
                        },
                        new KeyFrame
                        {
                            Cue= new(1),
                            Setters =
                            {
                                new Setter(TranslateTransform.YProperty,
                                    GamePageView.Instance!.NameCanv.Height +
                                    GamePageView.Instance!.DialogCanv.Height),
                                new Setter(Avalonia.Visual.OpacityProperty, 0.0),
                            },
                            KeySpline = new(.4, 0, .2, 1)
                        }
                    }
                },
                Start = target =>
                {
                    target.RenderTransform = new TranslateTransform(0,
                        GamePageView.Instance!.NameCanv.Height + GamePageView.Instance!.DialogCanv.Height);
                },
                End = target =>
                {
                    target.Opacity = 0.0;
                }
            }
            #endregion
        };
    }
}