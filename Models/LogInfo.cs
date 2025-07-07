using StoryTable;
using System.Collections.Generic;

namespace Avalgame.Models
{
    /// <summary>
    /// 剧情回溯信息
    /// </summary>
    public struct LogInfo
    {
        /// <summary>
        /// 位于脚本文件位置
        /// </summary>
        public Locator Position { get; set; }
        /// <summary>
        /// 背景图片路径
        /// </summary>
        public string? BgSrc { get; set; }
        /// <summary>
        /// 背景音乐路径
        /// </summary>
        public string? BgMsc { get; set; }
        /// <summary>
        /// 当前出场人物信息
        /// </summary>
        public List<string> Characters { get; set; }
        /// <summary>
        /// 当前选项信息
        /// </summary>
        public OptionInfo[] Options { get; set; }
        /// <summary>
        /// 当前讲话人物名称
        /// </summary>
        public string? Character { get; set; }
        /// <summary>
        /// 当前讲话人物头像
        /// </summary>
        public string? AvatarSrc { get; set; }
        /// <summary>
        /// 当前台词
        /// </summary>
        public string? Dialogue { get; set; }
    }
}
