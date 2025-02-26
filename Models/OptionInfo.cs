using Avalgame.ViewModels;
using Avalgame.Views;
using Avalonia.Controls;
using StoryTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Avalgame.Models
{
    public class OptionInfo
    {
        public string Content { get; init; }
        public Locator Target { get; init; }

        private Button? btn;

        public static double DefaultWidth => MainWindow.ScreenWidth * .5;
        public static double DefaultHeight => MainWindow.ScreenHeight * .04;

        public OptionInfo(string content, Locator target)
        {
            Content = content;
            Target = target;
        }

        private Button MakeButton()
        {
            var btn = new Button
            {
                Width = DefaultWidth,
                Height = DefaultHeight,
                Content = Content,
                HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalContentAlignment = Avalonia.Layout.VerticalAlignment.Center,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
            };
            btn.Click += (s, e) => GamePageViewModel.Instance!.Options.Select(Target);
            return btn;
        }

        public void Show() => GamePageView.Instance!.OptionPanel.Children.Add(btn ??= MakeButton());
        public void Hide() => GamePageView.Instance!.OptionPanel.Children.Remove(btn!);
    }
}
