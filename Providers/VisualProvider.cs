using Avalgame.ViewModels;
using Avalgame.Models;
using StoryTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
