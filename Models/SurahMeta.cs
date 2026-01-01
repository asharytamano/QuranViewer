namespace QuranViewer.Models
{
    public class SurahMeta
    {
        public int Number { get; set; }
        public string ArabicName { get; set; } = "";
        public string EnglishName { get; set; } = "";
        public string Meaning { get; set; } = "";
        public int AyahCount { get; set; }
        public string RevelationPlace { get; set; } = ""; // "Makkah" / "Madinah"
    }
}
