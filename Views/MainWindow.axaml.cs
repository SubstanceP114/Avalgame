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
            WindowState = WindowState.Maximized;
            OnLoad();
        }
        public static double ScreenHeight { get; private set; }
        public static double ScreenWidth { get; private set; }
        private void OnLoad()
        {
            var screen = Screens.Primary;
            ScreenHeight = screen!.WorkingArea.Height / screen!.Scaling;
            ScreenWidth = screen!.WorkingArea.Width / screen!.Scaling;

            MinHeight = MaxHeight = ScreenHeight;
            MinWidth = MaxWidth = ScreenWidth;
        }
    }
}