using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Avalgame.Models
{
    /// <summary>
    /// 用于记录选项、判断剧情分支的条件
    /// </summary>
    public class Conditions
    {
        /// <summary>
        /// 记录数据的容器
        /// </summary>
        public Dictionary<string, int> Data { get; set; }
        /// <summary>
        /// 创建空条件
        /// </summary>
        public Conditions() => Data = new Dictionary<string, int>();
        /// <summary>
        /// 根据现有条件构造新条件
        /// </summary>
        /// <param name="source">现有条件</param>
        public Conditions(Conditions source) => Data = new(source.Data);
        /// <summary>
        /// <see cref="Data"/>的索引，用于更方便地访问到数据
        /// </summary>
        /// <param name="key">条件名称</param>
        /// <returns>条件对应状态，若查询不到则返回-1</returns>
        int this[string key]
        {
            get => Data.ContainsKey(key) ? Data[key] : -1;
            set => Data.Add(key, value);
        }
        /// <summary>
        /// 判定当前条件与分支条件是否匹配
        /// </summary>
        /// <param name="source">当前条件</param>
        /// <param name="target">分支条件</param>
        public static bool Match(Conditions source, string target)
        {
            foreach (string condition in target.Split('&'))
                if (source[condition.Split(' ')[0]] != int.Parse(condition.Split(' ')[1]))
                    return false;
            return true;
        }
    }
}
