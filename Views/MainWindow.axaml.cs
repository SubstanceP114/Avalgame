using Avalgame.ViewModels;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalgame.Views
{
    public partial class MainWindow : Window
    {
        private static MainWindow? instance;
        public static MainWindow Instance => instance!;
        public MainWindow()
        {
            instance = this;
            InitializeComponent();
            OnLoad();
            WindowState = WindowState.FullScreen;
        }
        public static int ScreenHeight { get; private set; }
        public static int ScreenWidth { get; private set; }
        private void OnLoad()
        {
            ScreenHeight = Screens.All[0].WorkingArea.Height;
            ScreenWidth = Screens.All[0].WorkingArea.Width;
        }
    }
}