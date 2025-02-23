using Avalonia.Platform;
using StoryTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalgame.Providers
{
    internal class FileProvider : IFileProvider
    {
        private const string PATH = "avares://Avalgame/Assets/Scripts/";
        public bool FindFile(string name)
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
