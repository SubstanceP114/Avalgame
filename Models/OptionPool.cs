using Avalgame.Views;
using StoryTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalgame.Models
{
    public class OptionPool
    {
        public readonly List<OptionInfo> Infos;

        private readonly ExecutorBase executor;
        private readonly bool record;
        public OptionPool(ExecutorBase executor)
        {
            this.executor = executor;
            Infos = [];
            record = false;
        }
        public OptionPool(OptionInfo[] options, ExecutorBase executor)
        {
            this.executor = executor;
            Infos = [.. options];

            if (Infos.Count > 0)
            {
                record = true;
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
            Infos.Add(option);
            option.Show();
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

            if (!record) for (int i = 0; i < Infos.Count; i++) executor.Complete();

            Infos.Clear();
        }
    }
}
