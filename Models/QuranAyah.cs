using System.Text.Json.Serialization;

namespace QuranViewer.Models
{
    public class QuranAyah
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("surah")]
        public int Surah { get; set; }

        [JsonPropertyName("ayah")]
        public int Ayah { get; set; }

        [JsonPropertyName("quran_text")]
        public string QuranText { get; set; } = "";

        [JsonPropertyName("maranao_translation")]
        public string MaranaoTranslation { get; set; } = "";

        [JsonPropertyName("tafsir_text_original")]
        public string TafsirTextOriginal { get; set; } = "";
    }
}
