using QuranViewer.Models;
using System.ComponentModel;
using System.Windows;

namespace QuranViewer.ViewModels
{
    public class AyahViewModel : INotifyPropertyChanged
    {
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
