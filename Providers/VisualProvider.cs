using Avalgame.ViewModels;
using Avalgame.Models;
using StoryTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalgame.Views;
using Avalonia.Controls;

namespace Avalgame.Providers
{
    internal class VisualProvider : IVisualProvider
    {
        private int optionCnt;
        public void Menu(string content, Locator target, ExecutorBase executor)
        {
            var v = GamePageView.Instance!;

            v.ScrBtn.IsEnabled = false;
            optionCnt++;

            var button = new Button
            {
                Width = MainWindow.ScreenWidth * .5,
                Height = MainWindow.ScreenHeight * .04,
                Content = content,
                HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalContentAlignment = Avalonia.Layout.VerticalAlignment.Center,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
            };
            button.Click += (s, e) =>
            {
                v.ScrBtn.IsEnabled = true;
                v.OptionPanel.Children.Clear();
                while (optionCnt-- > 0) executor.Complete();
            };
            v.OptionPanel.Children.Add(button);
        }
        public async void Say(string character, string sprite, string dialogue, ExecutorBase executor)
        {
            var vm = GamePageViewModel.Instance!;
            vm.Character = character;

            vm.Sprite = sprite;
            var info = new SpriteInfo(sprite, new Avalonia.Rect(0,
                -MainWindow.ScreenHeight * .2, MainWindow.ScreenWidth * .4, MainWindow.ScreenHeight * .8));
            if (!vm.Sprites.Replace(info)) vm.Sprites.Add(info);

            vm.Dialogue = "";
            foreach (var word in dialogue)
            {
                if (executor.Skip) break;
                await Task.Delay(Archive.Instance.Pref.TextInterval);
                vm.Dialogue += word;
            }
            vm.Dialogue = dialogue;

            executor.Complete();
        }
    }
}
