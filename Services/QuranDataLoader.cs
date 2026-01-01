using QuranViewer.Models;
using System.IO;
using System.Text.Json;

namespace QuranViewer.Services
{
    public static class QuranDataLoader
    {
        public static async Task<List<QuranAyah>> LoadAsync(string path)
        {
            if (!File.Exists(path))
                return new List<QuranAyah>();

            using var stream = File.OpenRead(path);
            var data = await JsonSerializer.DeserializeAsync<List<QuranAyah>>(
                stream,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return data ?? new List<QuranAyah>();
        }
    }
}
