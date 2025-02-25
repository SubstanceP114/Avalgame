using Avalgame.Helpers;
using Avalgame.Models;
using Avalgame.Providers;
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

            executor.Provider.Data = new DataProvider();
            executor.Provider.File = new FileProvider();
            executor.Provider.Visual = new VisualProvider();

            Sprites = new(2);

            executor.Locate("Test");
        }
        public override void Init() => executor.Execute();

        private readonly ExecutorImpl executor;

        [ObservableProperty]
        private Bitmap? bgImg;
        /// <summary>
        /// 背景图片路径
        /// </summary>
        public string? BgSrc {  get; set; }
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
        private string? dialogue;

        [RelayCommand]
        private void Next()
        {
            executor.Execute();
        }
    }
}