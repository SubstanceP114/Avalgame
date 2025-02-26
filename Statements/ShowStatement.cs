using Avalgame.Views;
using Avalonia.Animation;
using Avalonia.Threading;
using StoryTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Avalgame.Statements
{
    [Statement("SHOW")]
    public partial class ShowStatement : Statement
    {
        private static readonly Dictionary<Option, Animation> operations = [];

        private readonly Option option;
        public ShowStatement(ArgParser parser) : base(parser)
        {
            option = parser.Enum<Option>();
        }
        public override ExecuteMode Mode => ExecuteMode.Wait;
        public async override void Execute(ExecutorBase executor)
        {
            if (!operations.TryGetValue(option, out var anim))
                operations.Add(option, anim = ShowDialog);
            var cts = new CancellationTokenSource();
            _ = anim.RunAsync(GamePageView.Instance!.TextCanv, cts.Token);

            int refreshTime = (executor as ExecutorImpl)!.RefreshTime;
            int countdown = (int)anim.IterationCount.Value * (int)anim.Duration.TotalMilliseconds;
            while ((countdown -= refreshTime) > 0 && !executor.Skip) await Task.Delay(refreshTime);

            cts.Cancel();
            executor.Complete();
        }
        public enum Option
        {
            Dialog,
        }
    }
}
