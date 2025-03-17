using Avalgame.ViewModels;
using Avalonia.Controls;
using StoryTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Avalgame.Statements
{
    [Statement("ANIM")]
    public partial class AnimStatement : Statement
    {
        private readonly Option option;
        private readonly string target;
        public AnimStatement(ArgParser parser) : base(parser)
        {
            option = parser.Enum<Option>();
            target = parser.String();
        }
        public override ExecuteMode Mode => ExecuteMode.Next;
        public async override void Execute(ExecutorBase executor)
        {
            var vm = GamePageViewModel.Instance!;

            if (!vm.Sprites.Replace(target)) vm.Sprites.Add(target);
            var sprite = vm.Sprites.Get(target);

            var anim = Anims[option](sprite);
            var cts = new CancellationTokenSource();

            _ = anim.RunAsync(sprite.Img, cts.Token);
            int refreshTime = (executor as ExecutorImpl)!.RefreshTime;
            int countdown = (int)anim.IterationCount.Value * (int)anim.Duration.TotalMilliseconds;
            while ((countdown -= refreshTime) > 0 && !executor.Skip) await Task.Delay(refreshTime);

            cts.Cancel();
            executor.Complete();
        }
    }
}
