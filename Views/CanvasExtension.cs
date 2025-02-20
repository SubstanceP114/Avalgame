using Avalonia.Controls;

namespace Avalgame.Views
{
    public static class CanvasExtension
    {
        public static void Full(this Canvas canvas)
        {
            canvas.Width = MainWindow.ScreenWidth;
            canvas.Height = MainWindow.ScreenHeight;
        }
    }
}
