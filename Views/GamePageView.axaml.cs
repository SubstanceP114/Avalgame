using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Avalgame.Views
{
    public partial class GamePageView : UserControl
    {
        public GamePageView()
        {
            InitializeComponent();
            InitView();
        }
        #region Parameters
        private readonly double screenWidth = MainWindow.ScreenWidth;
        private readonly double nameWidth = MainWindow.ScreenWidth * .36;
        private readonly double screenHeight = MainWindow.ScreenHeight;
        private readonly double nameHeight = MainWindow.ScreenHeight * .03;
        private readonly double dialogHeight = MainWindow.ScreenHeight * .3;
        private readonly int ZIdxLow = 86;
        private readonly int ZIdxMid = 100;
        private readonly int ZIdxHigh = 114;
        private readonly int dialogMarginX = 32;
        private readonly int dialogMarginY = 32;
        #endregion
        private void InitView()
        {
            #region RootCanv
            RootCanv.Width = screenWidth;
            RootCanv.Height = screenHeight;
            #endregion
            #region BackgroundImg
            BackgroundImg.Width = screenWidth;
            BackgroundImg.Height = screenHeight;
            BackgroundImg.Stretch = Avalonia.Media.Stretch.Fill;
            BackgroundImg.ZIndex = ZIdxLow;
            #endregion
            #region NameCanv
            Canvas.SetBottom(NameCanv, dialogHeight);
            NameCanv.Width = nameWidth;
            NameCanv.Height = nameHeight;
            NameCanv.Opacity = .5;
            NameCanv.ZIndex = ZIdxHigh;
            #endregion
            #region NameText
            NameText.Width = screenWidth;
            NameText.Height = nameHeight;
            NameText.Opacity = 2;
            NameText.FontSize = 24;
            NameText.TextAlignment = Avalonia.Media.TextAlignment.Center;
            NameText.FontWeight = Avalonia.Media.FontWeight.Bold;
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
            #endregion
        }
    }
}