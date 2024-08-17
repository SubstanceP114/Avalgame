using StoryParser.Core.Input;

namespace StoryParser.Core.Statement
{
    public class Pause : IStatement, IDispatcher
    {
        public void Execute()
        {
            Executor.Pause();
            Executor.Complete();
        }
        public IStatement Dispatch(string[] parameters) => new Pause();
    }
    public class End : IStatement, IDispatcher
    {
        public End(int value)
        {
            this.value = value;
        }
        public void Execute()
        {
            Executor.EndWith(value);
            Executor.Pause();
            Executor.Complete();
        }
        public IStatement Dispatch(string[] parameters)
        {
            if (parameters.Length != 2)
                throw new ArgumentException(string.Format("{0}数组长度有误", parameters), nameof(parameters));
            return new End(int.Parse(parameters[1]));
        }
        private readonly int value;
    }
}
