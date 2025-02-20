using StoryTable;
using System;

namespace Avalgame.Providers
{
    internal class LogProvider : ILogProvider
    {
        public void Message(string message) => Console.WriteLine($"Message: {message}");
        public void Warning(string warning) => Console.WriteLine($"Warning: {warning}");
        public void Error(string error) => Console.WriteLine($"Error: {error}");
    }
}
