using Avalgame.Models;
using Avalgame.ViewModels;
using StoryTable;
using System.Threading;
using System.Threading.Tasks;

namespace Avalgame.Statements
{
    [Statement("ANIM")]
    public partial class AnimStatement : Statement
    {
        private readonly Option option;
        private readonly string character, difference;
        public AnimStatement(ArgParser parser) : base(parser)
        {
            option = parser.Enum<Option>();
            character = parser.String();
            difference = parser.StringOr(string.Empty);
        }
        public override ExecuteMode Mode => ExecuteMode.Next;
        public async override void Execute(ExecutorBase executor)
        {
            executor.Complete();
            bool end = false;
            void End() => end = true;
            executor.OnExecuting += End;

            var vm = GamePageViewModel.Instance!;

            var character = CharacterManager.Instance[this.character];
            if (!string.IsNullOrEmpty(difference)) character.Show(difference);
            var sprite = character.Sprite!;

            var anim = Anims[option](sprite);
            var cts = new CancellationTokenSource();

            _ = anim.RunAsync(sprite.Img, cts.Token);
            int refreshTime = (executor as ExecutorImpl)!.RefreshTime;
            int countdown = (int)anim.IterationCount.Value * (int)anim.Duration.TotalMilliseconds;
            while ((countdown -= refreshTime) > 0 && !executor.Skip && !end) await Task.Delay(refreshTime);

            cts.Cancel();
            executor.OnExecuting -= End;
        }
    }
}
