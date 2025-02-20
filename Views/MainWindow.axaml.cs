using Avalgame.Providers;
using Avalgame.ViewModels;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using StoryTable;

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
            WindowState = WindowState.FullScreen;
            OnLoad();
        }
        public static int ScreenHeight { get; private set; }
        public static int ScreenWidth { get; private set; }
        private void OnLoad()
        {
            ScreenHeight = Screens.Primary!.WorkingArea.Height;
            ScreenWidth = Screens.Primary!.WorkingArea.Width;

            Provider.Data = new DataProvider();
            Provider.Log = new LogProvider();
        }
    }
}