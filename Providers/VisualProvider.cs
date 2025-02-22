using Avalgame.ViewModels;
using Avalgame.Models;
using StoryTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalgame.Views;

namespace Avalgame.Providers
{
    internal class VisualProvider : IVisualProvider
    {
        public void Menu(string content, int target, Executor executor)
        {

        }
        public async void Say(string character, string sprite, string dialogue, Executor executor)
        {
            GamePageViewModel.Instance!.Character = character;

            GamePageViewModel.Instance.Sprite = sprite;
            var info = new SpriteInfo(sprite, new Avalonia.Rect(0, 
                -MainWindow.ScreenHeight * .2, MainWindow.ScreenWidth * .3, MainWindow.ScreenHeight * .8));
            if (!GamePageViewModel.Instance.Sprites.Replace(info)) GamePageViewModel.Instance.Sprites.Add(info);

            GamePageViewModel.Instance.Dialogue = "";
            foreach (var word in dialogue)
            {
                if (executor.Skip) break;
                await Task.Delay(Archive.Instance.Pref.TextInterval);
                GamePageViewModel.Instance.Dialogue += word;
            }
            GamePageViewModel.Instance.Dialogue = dialogue;

            executor.Complete();
        }
    }
}
