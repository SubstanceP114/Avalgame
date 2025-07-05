using StoryTable;
using System;
using System.Collections.Generic;

namespace Avalgame.Helpers
{
    internal class FuncHelper
    {
        public static void Init(Dictionary<string, (ExecuteMode, Action<ExecutorBase, string[]>)> dict)
        {
            dict.Add("M", (
                ExecuteMode.Next,
                (executor, args) =>
                {
                    Logger.Message(args[0]);
                    executor.Complete();
                }
            ));
            dict.Add("W", (
                ExecuteMode.Next,
                (executor, args) =>
                {
                    Logger.Warning(args[0]);
                    executor.Complete();
                }
            ));
            dict.Add("E", (
                ExecuteMode.Next,
                (executor, args) =>
                {
                    Logger.Error(args[0]);
                    executor.Complete();
                }
            ));
        }
    }
}
