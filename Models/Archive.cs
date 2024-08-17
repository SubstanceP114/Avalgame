using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Avalgame.Models
{
    /// <summary>
    /// 存档相关
    /// </summary>
    public class Archive
    {
        private static string PATH => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "savedata.json");
        /// <summary>
        /// 从<see cref="PATH"/>反序列化存档
        /// </summary>
        /// <returns>存档信息，若读取失败则新建存档</returns>
        public static Archive Deserialize() => JsonSerializer.Deserialize<Archive>(File.ReadAllText(PATH)) ?? new();
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
        /// 记录全存档共通的数据
        /// </summary>
        public Dictionary<string, int> Global { get; set; }
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
            Global = new();
            Current = new Local();
            Datas = new();
        }
        /// <summary>
        /// <see cref="Global"/>的索引，用于更方便地访问到数据
        /// </summary>
        /// <param name="key">条件名称</param>
        /// <returns>条件对应状态，若查询不到则返回-1</returns>
        public int this[string key]
        {
            get
            {
                if (Global.TryGetValue(key, out int value)) return value;
                if (Current.Data.TryGetValue(key, out value)) return value;
                return -1;
            }
        }
        public struct Local
        {
            public LogInfo Log { get; set; }
            public Dictionary<string, int> Data { get; set; }
        }
    }
}
