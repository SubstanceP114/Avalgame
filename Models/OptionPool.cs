using Avalgame.Views;
using StoryTable;
using System.Collections.Generic;

namespace Avalgame.Models
{
    public class OptionPool
    {
        public readonly List<OptionInfo> Infos;
        private readonly ExecutorBase executor;
        public OptionPool(ExecutorBase executor)
        {
            this.executor = executor;
            Infos = [];
        }
        public OptionPool(OptionInfo[] options, ExecutorBase executor)
        {
            this.executor = executor;
            Infos = [.. options];

            if (Infos.Count > 0)
            {
                Arrange();
                Infos.ForEach(o => o.Show());
            }
        }

        public void Select(Locator target)
        {
            Clear();
            executor.Locate(target);
            executor.Execute();
        }

        public void Add(string content, Locator target) => Add(new(content, target));
        public void Add(OptionInfo option)
        {
            option.Show();
            Infos.Add(option);
            Arrange();
        }

        public void Arrange()
        {
            GamePageView.Instance!.ScrBtn.IsEnabled = false;
            var op = GamePageView.Instance.OptionPanel!;
            op.Spacing = (op.Height - OptionInfo.DefaultHeight * Infos.Count) / (Infos.Count + 1);
        }

        public void Clear()
        {
            GamePageView.Instance!.ScrBtn.IsEnabled = true;
            Infos.ForEach(o => o.Hide());
            Infos.Clear();
        }
    }
}
