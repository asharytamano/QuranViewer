using QuranViewer.Models;
using System.ComponentModel;
using System.Windows;

namespace QuranViewer.ViewModels
{
    public class AyahViewModel : INotifyPropertyChanged
    {
        public int SurahNumber { get; set; }
        public int AyahNumber { get; set; }   // 0 for Bismillah row (except Surah 9)
        public bool IsAyah0 => AyahNumber == 0;

        public bool ShowToggleVisible =>
            AyahNumber > 0                 // hide for ayah 0
            && (AyahNumber != 1 || SurahNumber == 1); // your existing rule: hide ayah 1 except Al-Fatihah

        public QuranAyah Data { get; }

        public string AyahHeader => $"Surah {Data.Surah} • Ayah {Data.Ayah}";

        private const string Bismillah = "بِسْمِ اللَّهِ الرَّحْمَـٰنِ الرَّحِيمِ";

        public bool IsBismillah =>
            (Data.QuranText ?? "").Trim() == Bismillah;

        // Hide toggle for Bismillah in all surahs except Surah 1
        public bool CanShowTafsir =>
            !IsBismillah || Data.Surah == 1;

        // Center standalone Bismillah: Surah != 1 and Ayah == 0
        // Keep Surah 1 Bismillah right-aligned because it is part of Al-Fatihah.
        public TextAlignment ArabicTextAlignment =>
    (IsBismillah && Data.Ayah == 0 && Data.Surah != 1)
        ? TextAlignment.Center
        : TextAlignment.Left;   // <-- YES, Left for RTL gives visual right alignment

        private bool _isTafsirVisible;
        public bool IsTafsirVisible
        {
            get => _isTafsirVisible;
            set
            {
                if (_isTafsirVisible == value) return;
                _isTafsirVisible = value;
                OnPropertyChanged(nameof(IsTafsirVisible));
                OnPropertyChanged(nameof(TafsirButtonText));
            }
        }

        public string TafsirButtonText => IsTafsirVisible ? "Hide" : "Show";

        public AyahViewModel(QuranAyah data) => Data = data;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
