using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Avalgame.Helpers
{
    public class ImageHelper
    {
        public static Bitmap LoadFromResource(Uri resourceUri)
        {
            if (AssetLoader.Exists(resourceUri)) return new Bitmap(AssetLoader.Open(resourceUri));
            throw new FileNotFoundException($"找不到文件{resourceUri}");
        }

        private const string BACKGROUND_PATH = "avares://Avalgame/Assets/Images/Backgrounds";
        private const string SPRITE_PATH = "avares://Avalgame/Assets/Images/Characters";

        public static Bitmap LoadBackground(string src) =>
            LoadFromResource(new Uri($"{BACKGROUND_PATH}/{src}"));
        public static Bitmap LoadSprite(string src) =>
            LoadFromResource(new Uri($"{SPRITE_PATH}/{src}"));
        public static Bitmap LoadSprite(string character, string difference) =>
            LoadFromResource(new Uri($"{SPRITE_PATH}/{character}/{difference}"));

        public static async Task<Bitmap?> LoadFromWeb(Uri url)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsByteArrayAsync();
                return new Bitmap(new MemoryStream(data));
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occurred while downloading image '{url}' : {ex.Message}");
                return null;
            }
        }
    }
}
