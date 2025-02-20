using Avalgame.Models;
using StoryTable;

namespace Avalgame.Providers
{
    internal class DataProvider : IDataProvider
    {
        public int GetInt(string key) => Archive.Instance.GetInt(key);
        public void SetInt(string key, int value) => Archive.Instance.Current.IntData[key] = value;
        public string GetString(string key) => Archive.Instance.GetString(key);
        public void SetString(string key, string value) => Archive.Instance.Current.StringData[key] = value;
    }
}
