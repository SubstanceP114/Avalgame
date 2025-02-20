using Avalonia.Controls;

namespace Avalgame.Views
{
    public static class ControlExtension
    {
        public static void Full(this Control control)
        {
            control.Width = MainWindow.ScreenWidth;
            control.Height = MainWindow.ScreenHeight;
        }
    }
}
