using Avalonia.Platform;
using StoryTable;
using System;
using System.Text;

namespace Avalgame.Providers
{
    internal class FileProvider : IFileProvider
    {
        private const string PATH = "avares://Avalgame/Assets/Scripts/";
        public bool Find(string name)
        {
            var uri = new Uri(PATH + name + ".CSV");
            if (AssetLoader.Exists(uri))
            {
                IntermediateFile.Load(name, AssetLoader.Open(uri), Encoding.UTF8);
                return true;
            }
            return false;
        }
    }
}
