namespace QuranViewer.Models
{
    public class SurahItem
    {
        public int Number { get; set; }
        public string NameEn { get; set; } = "";

        public string Display => $"{Number:D3} {NameEn}";

        public override string ToString() => Display; // important for ComboBox display
    }
}
