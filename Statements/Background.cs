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
    public class Background : Statement
    {
        private readonly string src;
        public Background(ArgParser parser) : base(parser)
        {
            src = parser.String();
        }
        public override ExecuteMode Mode => ExecuteMode.Wait;
        public override void Execute(Executor executor)
        {
            GamePageViewModel.Instance!.BgImg = ImageHelper.LoadFromResource(new Uri("avares://Avalgame/Assets/Images/Backgrounds/" + src));
            executor.Complete();
        }
    }
}
