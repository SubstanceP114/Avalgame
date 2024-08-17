using Avalonia;

namespace Avalgame.Models
{
    public class ImageInfo
    {
        public ImageInfo(string src, Rect rect, float rotation, float transparency)
        {
            Src = src;
            Rect = rect;
            Rotation = rotation;
            Transparency = transparency;
        }
        public readonly string Src;
        public Rect Rect { get; set; }
        public float Rotation { get; set; }
        public float Transparency { get; set; }
    }
}
