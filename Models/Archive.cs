using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Avalgame.Models
{
    /// <summary>
    /// 存档相关
    /// </summary>
    public class Archive
    {
        private static string PATH => Path.Combine(Environment.CurrentDirectory, "savedata.json");
        /// <summary>
        /// 存档单例
        /// </summary>
        public static Archive Instance { get; } = JsonSerializer.Deserialize<Archive>(File.ReadAllText(PATH)) ?? new();
        /// <summary>
        /// 向<see cref="PATH"/>序列化当前存档内容
        /// </summary>
        public void Serialize() => File.WriteAllText(PATH, JsonSerializer.Serialize(this));
        /// <summary>
        /// 读取指定存档
        /// </summary>
        /// <param name="index">存档序号</param>
        public void Load(int index) => Current = Datas[index];
        /// <summary>
        /// 保存当前存档
        /// </summary>
        public void Save() => Datas.Add(Current);
        /// <summary>
        /// 记录玩家设置
        /// </summary>
        public PlayerPref Pref { get; set; }
        /// <summary>
        /// 记录全存档共通的int数据
        /// </summary>
        public Dictionary<string, int> GlobalInt { get; set; }
        /// <summary>
        /// 记录全存档共通的string数据
        /// </summary>
        public Dictionary<string, string> GlobalString { get; set; }
        /// <summary>
        /// 当前存档数据
        /// </summary>
        public Local Current { get; set; }
        /// <summary>
        /// 记录存档数据
        /// </summary>
        public List<Local> Datas { get; set; }
        /// <summary>
        /// 创建空条件
        /// </summary>
        public Archive()
        {
            Pref = new PlayerPref();
            GlobalInt = new();
            GlobalString = new();
            Current = new Local();
            Datas = new();
        }
        public int GetInt(string key)
            => GlobalInt.TryGetValue(key, out int value) ||
            Current.IntData.TryGetValue(key, out value) ? value : -1;
        public string GetString(string key)
            => GlobalString.TryGetValue(key, out string value) ||
            Current.StringData.TryGetValue(key, out value) ? value : string.Empty;
        public struct Local
        {
            public LogInfo Log { get; set; }
            public Dictionary<string, int> IntData { get; set; }
            public Dictionary<string, string> StringData { get; set; }
        }
    }
}
