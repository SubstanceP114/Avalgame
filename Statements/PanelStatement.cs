using Avalgame.Views;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Rendering.Composition.Animations;
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
    public class ShowStatement : PanelStatement
    {
        public ShowStatement(ArgParser parser) : base(parser) { Reverse = false; }
    }
    [Statement("HIDE")]
    public class HideStatement : PanelStatement
    {
        public HideStatement(ArgParser parser) : base(parser) { Reverse = true; }
    }

    public partial class PanelStatement : Statement
    {
        protected bool Reverse;

        private readonly Option option;
        public PanelStatement(ArgParser parser) : base(parser)
        {
            option = parser.Enum<Option>();
        }
        public override ExecuteMode Mode => ExecuteMode.Wait;
        public async override void Execute(ExecutorBase executor)
        {
            var panel = Panels[option];
            var info = Reverse ? panel.Hide : panel.Show;
            var anim = info.Anim;
            var cts = new CancellationTokenSource();

            info.Start(panel.Target);
            _ = anim.RunAsync(panel.Target, cts.Token);
            int refreshTime = (executor as ExecutorImpl)!.RefreshTime;
            int countdown = (int)anim.IterationCount.Value * (int)anim.Duration.TotalMilliseconds;
            while ((countdown -= refreshTime) > 0 && !executor.Skip) await Task.Delay(refreshTime);

            cts.Cancel();
            info.End(panel.Target);
            executor.Complete();
        }
    }

    public class PanelAnim
    {
        public Control Target;
        public AnimInfo Show;
        public AnimInfo Hide;
        public class AnimInfo
        {
            public Animation Anim;
            /// <summary>
            /// 预处理<see cref="Target"/防止结尾跳变
            /// </summary>
            public Action<Control> Start;
            /// <summary>
            /// 设置<see cref="Target"/>结束时状态
            /// </summary>
            public Action<Control> End;
        }
    }
}
