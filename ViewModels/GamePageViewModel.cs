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
using System.Collections.ObjectModel;
using System.Diagnostics;
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

            executor.Locate("Test");

            Sprites = new(2);
            Options = new(executor);

            Records = [];
        }
        public override void Init() => executor.Execute();

        private readonly ExecutorImpl executor;

        [ObservableProperty]
        public ObservableCollection<Archive.Local> records;
        [ObservableProperty]
        private int selectedRecordIdx;
        partial void OnSelectedRecordIdxChanged(int value)
        {
            if (!GamePageView.Instance!.LogView.IsEnabled || value < 0 || value >= Records.Count) return;
            HideLog();
            Goto((Archive.Instance.Current = new(Records[value])).Log);
            Records = new(Records.Take(value + 1));
        }
        private void Goto(LogInfo info)
        {
            Sprites.Clear();
            Options.Clear();

            executor.Locate(info.Position);
            BgSrc = info.BgSrc;
            BgMsc = info.BgMsc;
            Sprites = new(info.Imgs);
            Options = new(info.Options, executor);
            AvatarSrc = info.AvatarSrc;
            Character = info.Character;
            Dialogue = info.Dialogue;
        }
        [RelayCommand]
        private void ShowLog()
        {
            GamePageView.Instance!.LogView.IsVisible = GamePageView.Instance.LogView.IsEnabled = true;
            SelectedRecordIdx = -1;
        }
        [RelayCommand]
        private void HideLog() => GamePageView.Instance!.LogView.IsVisible = GamePageView.Instance.LogView.IsEnabled = false;


        [ObservableProperty]
        private Bitmap? bgImg;
        /// <summary>
        /// 背景图片路径
        /// </summary>
        public string? BgSrc { get; set; }
        /// <summary>
        /// 背景音乐路径
        /// </summary>
        public string? BgMsc { get; set; }

        /// <summary>
        /// 贴图信息
        /// </summary>
        public SpritePool Sprites { get; set; }
        /// <summary>
        /// 选项信息
        /// </summary>
        public OptionPool Options { get; set; }

        [ObservableProperty]
        private string? character;
        [ObservableProperty]
        private Bitmap? avatar;
        [ObservableProperty]
        private string? dialogue;

        /// <summary>
        /// 头像路径
        /// </summary>
        public string? AvatarSrc { get; set; }

        [RelayCommand]
        private void Next() => executor.Execute();
    }
}