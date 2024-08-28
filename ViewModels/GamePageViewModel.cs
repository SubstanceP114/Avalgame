using Avalgame.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using StoryParser.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalgame.ViewModels
{
    public partial class GamePageViewModel : ViewModelBase
    {
        private static GamePageViewModel? instance;
        public static GamePageViewModel Instance => instance!;
        public GamePageViewModel()
        {
            instance = this;
            Imgs = new();
        }
        /// <summary>
        /// 位于脚本文件位置
        /// </summary>
        public Locator Position { get; set; }
        [ObservableProperty]
        private string? _bgSrc;
        /// <summary>
        /// 背景音乐路径
        /// </summary>
        public string? BgMsc { get; set; }
        /// <summary>
        /// 贴图信息
        /// </summary>
        public List<ImageInfo>? Imgs { get; set; }
        [ObservableProperty]
        private string? character;
        [ObservableProperty]
        private string? sprite;
        [ObservableProperty]
        private string? dialogue;
    }
}