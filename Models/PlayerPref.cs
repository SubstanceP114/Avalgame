using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalgame.Models
{
    /// <summary>
    /// 玩家设置
    /// </summary>
    public struct PlayerPref
    {
        /// <summary>
        /// 文字显示时间间隔（毫秒）
        /// </summary>
        public int TextInterval { get; set; }
        /// <summary>
        /// 背景音乐音量
        /// </summary>
        public float MusicVolume { get; set; }
        /// <summary>
        /// 角色语音音量
        /// </summary>
        public float VoiceVolume { get; set; }
        /// <summary>
        /// 其余音效音量
        /// </summary>
        public float SoundVolume { get; set; }
    }
}
