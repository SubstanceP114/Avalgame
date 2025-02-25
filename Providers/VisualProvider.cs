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
        public int OptionCnt { get; set; }
        public void Menu(string content, Locator target, ExecutorBase executor)
        {
            var v = GamePageView.Instance!;

            v.ScrBtn.IsEnabled = false;
            OptionCnt++;

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
                v.OptionPanel.Children.Add(new Avalonia.Controls.Shapes.Rectangle
                {
                    Height = 0,
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
                });

                while (OptionCnt > 0)
                {
                    executor.Complete();
                    OptionCnt--;
                }

                executor.Locate(target);
                executor.Execute();
            };
            v.OptionPanel.Children.Add(button);

            var totalSpacing = v.OptionPanel.Height - button.Height * OptionCnt;
            v.OptionPanel.Spacing = totalSpacing / (OptionCnt + 1);
        }
        public async void Say(string character, string sprite, string dialogue, ExecutorBase executor)
        {
            var vm = GamePageViewModel.Instance!;
            vm.Character = character;

            vm.AvatarSrc = sprite;
            if (!vm.Sprites.Replace(sprite)) vm.Sprites.Add(sprite);

            vm.Dialogue = "";
            foreach (var word in dialogue)
            {
                if (executor.Skip) break;
                await Task.Delay(Archive.Instance.Pref.TextInterval);
                vm.Dialogue += word;
            }
            vm.Dialogue = dialogue;

            executor.Complete();

            if (OptionCnt > 0) return;
            if (vm.Records.Count == 256) vm.Records.RemoveAt(0);
            vm.Records.Add(Archive.Instance.Current = new()
            {
                Log = new()
                {
                    Position = executor.Position,
                    BgSrc = vm.BgSrc,
                    BgMsc = vm.BgMsc,
                    Imgs = vm.Sprites.GetSrcs(),
                    AvatarSrc = vm.AvatarSrc,
                    Character = vm.Character,
                    Dialogue = vm.Dialogue,
                },
                IntData = new(Archive.Instance.Current.IntData),
                StringData = new(Archive.Instance.Current.StringData)
            });
        }
    }
}
