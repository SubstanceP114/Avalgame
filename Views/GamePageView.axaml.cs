using Avalgame.Helpers;
using Avalgame.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using StoryTable;
using System;

namespace Avalgame.Views
{
    public partial class GamePageView : UserControl
    {
        public static GamePageView? Instance { get; private set; }
        public GamePageView()
        {
            Instance = this;
            InitializeComponent();
            InitView();
        }

        #region Parameters
        private readonly double screenWidth = MainWindow.ScreenWidth;
        private readonly double nameWidth = MainWindow.ScreenWidth * .2;

        private readonly double btnSize = MainWindow.ScreenWidth * .02;

        private readonly double screenHeight = MainWindow.ScreenHeight;
        private readonly double nameHeight = MainWindow.ScreenHeight * .03;
        private readonly double dialogHeight = MainWindow.ScreenHeight * .3;

        private readonly int ZIdxLow = 86;
        private readonly int ZIdxMid = 100;
        private readonly int ZIdxHigh = 114;
        private readonly int ZIdxLevel = 100;

        private readonly int dialogMarginX = 32;
        private readonly int dialogMarginY = 32;
        #endregion

        private void InitView()
        {
            #region RootCanv
            RootCanv.Full();
            #endregion
            #region PlainCanv
            PlainCanv.Full();
            #endregion
            #region BackgroundImg
            BackgroundImg.Width = screenWidth;
            BackgroundImg.Height = screenHeight;
            BackgroundImg.Stretch = Stretch.Fill;
            BackgroundImg.ZIndex = ZIdxLow;
            #endregion
            #region TextCanv
            TextCanv.Full();
            TextCanv.ZIndex = ZIdxHigh;
            #endregion
            #region NameCanv
            Canvas.SetBottom(NameCanv, dialogHeight);
            NameCanv.Width = nameWidth;
            NameCanv.Height = nameHeight;
            NameCanv.Opacity = .5;
            NameCanv.ZIndex = ZIdxHigh;
            #endregion
            #region NameText
            NameText.Width = nameWidth;
            NameText.Height = nameHeight;
            NameText.Opacity = 2;
            NameText.FontSize = 24;
            NameText.TextAlignment = TextAlignment.Center;
            NameText.FontWeight = FontWeight.Bold;
            #endregion
            #region DialogCanv
            Canvas.SetBottom(DialogCanv, 0);
            DialogCanv.Width = screenWidth;
            DialogCanv.Height = dialogHeight;
            DialogCanv.Opacity = .25;
            DialogCanv.ZIndex = ZIdxHigh;
            #endregion
            #region DialogText
            DialogText.Width = screenWidth - 2 * dialogMarginX;
            DialogText.Height = dialogHeight - 2 * dialogMarginY;
            DialogText.Opacity = 4;
            DialogText.FontSize = 20;
            DialogText.Margin = new Thickness(dialogMarginX, dialogMarginY);
            DialogText.TextAlignment = TextAlignment.Start;
            DialogText.FontWeight = FontWeight.Normal;
            #endregion
            #region SpriteCanv
            SpriteCanv.Full();
            SpriteCanv.ZIndex = ZIdxMid;
            #endregion
            #region ScrBtn
            ScrBtn.Full();
            ScrBtn.Opacity = 0;
            ScrBtn.ZIndex = ZIdxLow + ZIdxLevel;
            #endregion
            #region OptionPanel
            OptionPanel.Height = screenHeight - nameHeight - dialogHeight;
            OptionPanel.Width = screenWidth;
            OptionPanel.Children.Add(new Rectangle
            {
                Height = 0,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
            });
            OptionPanel.ZIndex = ZIdxLow + ZIdxLevel;
            #endregion
            #region BtnPanel
            BtnPanel.Width = btnSize * BtnPanel.Children.Count;
            BtnPanel.Height = btnSize;
            BtnPanel.Opacity = .5;
            BtnPanel.Margin = new Thickness
                (MainWindow.ScreenWidth - BtnPanel.Width,
                MainWindow.ScreenHeight - btnSize, 0, 0);
            BtnPanel.ZIndex = ZIdxMid + ZIdxLevel;
            #endregion
            #region LogBtnShow
            LogBtnShow.Width = btnSize;
            LogBtnShow.Height = btnSize;
            LogBtnShow.HotKey = new KeyGesture(Key.B);
            #endregion
            #region LogBtnHide
            LogBtnHide.Width = btnSize;
            LogBtnHide.Height = btnSize;
            LogBtnHide.Margin = new Thickness
                (MainWindow.ScreenWidth - btnSize, 0, 0, btnSize);
            LogBtnHide.HotKey = new KeyGesture(Key.Escape);
            LogBtnHide.HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center;
            LogBtnHide.VerticalContentAlignment = Avalonia.Layout.VerticalAlignment.Center;
            LogBtnHide.Content = "X";
            LogBtnHide.FontSize = btnSize * .5;
            LogBtnHide.ZIndex = ZIdxHigh + ZIdxLevel;
            #endregion
            #region LogView
            LogView.Full();
            LogView.Opacity = .25;
            LogView.AllowAutoHide = false;
            LogView.IsVisible = LogView.IsEnabled = false;
            LogView.ZIndex = ZIdxMid + ZIdxLevel;
            #endregion
            #region LogList
            LogList.Opacity = 4;
            #endregion

            #region Debug
            DebugView.Width = screenWidth * .2;
            DebugView.Height = screenHeight * .2;
            Canvas.SetRight(DebugView, 0);
            DebugView.AllowAutoHide = false;
            DebugView.Opacity = .25;
            DebugView.ZIndex = ZIdxHigh + ZIdxLevel * 2;

            var vm = GamePageViewModel.Instance!;
            vm.DebugInfos = new();
            Logger.Error = s => vm.DebugInfos.Add(s);
            Logger.Warning = s => vm.DebugInfos.Add(s);
            Logger.Message = s => vm.DebugInfos.Add(s);
            #endregion
        }
    }
}