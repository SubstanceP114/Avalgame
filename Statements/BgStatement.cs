using Avalgame.Helpers;
using Avalgame.ViewModels;
using StoryTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalgame.Statements
{
    [Statement("BG")]
    public class BgStatement : Statement
    {
        private readonly string src;
        public BgStatement(ArgParser parser) : base(parser)
        {
            src = parser.String();
        }
        public override ExecuteMode Mode => ExecuteMode.Wait;
        public override void Execute(ExecutorBase executor)
        {
            GamePageViewModel.Instance!.BgImg = ImageHelper.LoadBackground(src);
            GamePageViewModel.Instance.BgSrc = src;
            executor.Complete();
        }
    }
}
