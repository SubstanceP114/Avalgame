using Avalgame.ViewModels;
using Avalgame.Models;
using StoryTable;
using System.Threading.Tasks;

namespace Avalgame.Providers
{
    internal class VisualProvider : IVisualProvider
    {
        public void Menu(string content, Locator target, ExecutorBase executor)
        {
            GamePageViewModel.Instance!.Options.Add(content, target);
            executor.Complete();
        }
        public async void Say(string character, string sprite, string dialogue, ExecutorBase executor)
        {
            var vm = GamePageViewModel.Instance!;

            vm.Character = character;

            vm.AvatarSrc = sprite;

            CharacterManager.Instance[character].Show(sprite);

            vm.Dialogue = "";
            foreach (var word in dialogue)
            {
                if (executor.Skip) break;
                await Task.Delay(Archive.Instance.Pref.TextInterval);
                vm.Dialogue += word;
            }
            vm.Dialogue = dialogue;

            executor.Complete();

            if (vm.Records.Count == 256) vm.Records.RemoveAt(0);
            vm.Records.Add(Archive.Instance.Current = new()
            {
                Log = new()
                {
                    Position = executor.Position,
                    BgSrc = vm.BgSrc,
                    BgMsc = vm.BgMsc,
                    Characters = CharacterManager.Instance.Serialize(),
                    Options = [.. vm.Options.Infos],
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
