using Avalgame.Helpers;
using Avalgame.Models;
using Avalgame.Views;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StoryTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalgame.ViewModels
{
    public partial class GamePageViewModel : ViewModelBase
    {
        public static GamePageViewModel? Instance { get; private set; }
        public GamePageViewModel()
        {
            Instance = this;
            executor = new();
            Sprites = new(5);

            BgImg = ImageHelper.LoadFromResource(new Uri("avares://Avalgame/Assets/Images/Noa.jpg"));
            IntermediateFile.Load("Test", AssetLoader.Open(new Uri("avares://Avalgame/Assets/Scripts/Test.CSV")), Encoding.UTF8);
            executor.Locate("Test");
        }
        private readonly Executor executor;
        /// <summary>
        /// 位于脚本文件位置
        /// </summary>
        public Locator Position { get; set; }
        [ObservableProperty]
        private Bitmap? bgImg;
        /// <summary>
        /// 背景音乐路径
        /// </summary>
        public string? BgMsc { get; set; }
        /// <summary>
        /// 贴图信息
        /// </summary>
        public SpritePool Sprites { get; set; }
        [ObservableProperty]
        private string? character;
        [ObservableProperty]
        private string? sprite;
        [ObservableProperty]
        public string? dialogue;

        [RelayCommand]
        private void Next()
        {
            executor.Execute();
        }
    }
}