using Avalgame.Models;
using Avalgame.ViewModels;
using StoryParser.Core.Input;
using System.Threading.Tasks;

namespace StoryParser.Extension
{
    public partial class Commands
    {
        static Commands()
        {
            state = LineState.Processed;
            Executor.LineProcessing += x => state = LineState.Processing;
            Executor.LineProcessed += x => state = LineState.Processed;
        }
        private static GamePageViewModel Current => GamePageViewModel.Instance;
        private enum LineState { Processing, Accelerating, Processed }
        private static LineState state;
        public static partial void Menu(string content, int target)
        {

        }
        public static async partial void Say(string? character, string? sprite, string dialogue)
        {
            Current.Character = character;
            Current.Sprite = sprite;
            Current.Dialogue = "";
            for (int i = 0; i < dialogue.Length; i++)
            {
                if (state == LineState.Accelerating) break;
                await Task.Delay(Archive.Instance.Pref.TextInterval);
                Current.Dialogue += dialogue[i];
            }
            Current.Dialogue = dialogue;
            Executor.Complete();
        }
        public static partial object GetValue(string key) => Archive.Instance[key];
        public static partial void SetValue(string key, int value) => Archive.Instance.Current.Data.Add(key, value);
    }
}
