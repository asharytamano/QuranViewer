using QuranViewer.Helpers;
using QuranViewer.Models;
using QuranViewer.ViewModels;
using QuranViewer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace QuranViewer.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<AyahViewModel> AllAyat { get; } = new();
        public ObservableCollection<AyahViewModel> FilteredAyat { get; } = new();

        public ObservableCollection<SurahItem> SurahList { get; } = new();

        private bool _isSurahMode = true;
        public bool IsSurahMode
        {
            get => _isSurahMode;
            set
            {
                if (_isSurahMode == value) return;
                _isSurahMode = value;
                OnPropertyChanged(nameof(IsSurahMode));
                OnPropertyChanged(nameof(IsPageMode));
            }
        }

        public bool IsPageMode => !IsSurahMode;

        private int _selectedPage = 1;
        public int SelectedPage
        {
            get => _selectedPage;
            set
            {
                if (_selectedPage == value) return;
                _selectedPage = value;
                OnPropertyChanged(nameof(SelectedPage));
                OnPropertyChanged(nameof(SelectedPageTitle));
                OnPropertyChanged(nameof(SelectedPageSubtitle));
            }
        }

        public string SelectedPageTitle => $"Page {SelectedPage:D3}";
        public string SelectedPageSubtitle => "Scroll to navigate • Tap an ayah to jump";

        // =====================
        // Surah metadata
        // =====================
        public SurahMeta? SelectedSurahMeta { get; private set; }

        private readonly Dictionary<int, SurahMeta> _surahMetaByNumber = new();

        private int _selectedSurah = 1;
        public int SelectedSurah
        {
            get => _selectedSurah;
            set
            {
                if (_selectedSurah == value) return;
                _selectedSurah = value;
                OnPropertyChanged(nameof(SelectedSurah));

                ApplySurahFilter();
                UpdateSelectedSurahMeta();

                // Update Surah Header Panel
                OnPropertyChanged(nameof(SelectedSurahTitle));
                OnPropertyChanged(nameof(SelectedSurahSubtitle));
            }
        }

        private AyahViewModel? _openTafsirAyah;

        public RelayCommand<AyahViewModel> ToggleTafsirCommand { get; }

        public MainViewModel()
        {
            ToggleTafsirCommand = new RelayCommand<AyahViewModel>(ToggleTafsir);

            // 1) Build Surah list with display "001 Al-Fatihah"
            // SurahNames.English assumed to be 1-based index: names[1..114]
            var names = SurahNames.English;
            for (int i = 1; i <= 114; i++)
            {
                SurahList.Add(new SurahItem
                {
                    Number = i,
                    NameEn = names[i]
                });
            }

            // 2) Build metadata dictionary (Meaning + RevelationPlace)
            BuildSurahMetaDictionary();

            // 3) Set initial meta for Surah 1
            UpdateSelectedSurahMeta();
        }

        // -------- Surah Header Panel (Right side top banner) --------
        public string SelectedSurahTitle
        {
            get
            {
                var s = SurahList.FirstOrDefault(x => x.Number == SelectedSurah);
                if (s == null) return $"{SelectedSurah:D3}";

                // Prefer meta dictionary
                if (SelectedSurahMeta != null && !string.IsNullOrWhiteSpace(SelectedSurahMeta.Meaning))
                    return $"{s.Number:D3} {s.NameEn} [{SelectedSurahMeta.Meaning}]";

                // Fallback if meta missing
                return $"{s.Number:D3} {s.NameEn}";
            }
        }

        public string SelectedSurahSubtitle
        {
            get
            {
                int verseCount = AllAyat.Count(a => a.Data.Surah == SelectedSurah);

                // Prefer meta dictionary
                if (SelectedSurahMeta != null && !string.IsNullOrWhiteSpace(SelectedSurahMeta.RevelationPlace))
                    return $"{verseCount} verses, Revealed at {SelectedSurahMeta.RevelationPlace}";

                // Fallback if meta missing
                return $"{verseCount} verses";
            }
        }

        public async System.Threading.Tasks.Task InitializeAsync()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Data", "quran_tafsir.json");
            var data = await QuranDataLoader.LoadAsync(path);

            AllAyat.Clear();
            foreach (var item in data)
                AllAyat.Add(new AyahViewModel(item));

            ApplySurahFilter();
            UpdateSelectedSurahMeta();

            // Notify header panel after load
            OnPropertyChanged(nameof(SelectedSurahTitle));
            OnPropertyChanged(nameof(SelectedSurahSubtitle));
        }

        private void ApplySurahFilter()
        {
            // Close open tafsir when switching surah
            if (_openTafsirAyah != null)
            {
                _openTafsirAyah.IsTafsirVisible = false;
                _openTafsirAyah = null;
            }

            FilteredAyat.Clear();
            foreach (var a in AllAyat.Where(x => x.Data.Surah == SelectedSurah).OrderBy(x => x.Data.Ayah))
                FilteredAyat.Add(a);
        }

        private void ToggleTafsir(AyahViewModel? ayahVm)
        {
            if (ayahVm == null) return;

            // Block Bismillah toggles except Surah 1
            if (!ayahVm.CanShowTafsir) return;

            // Clicking same open item -> close it
            if (_openTafsirAyah == ayahVm)
            {
                ayahVm.IsTafsirVisible = false;
                _openTafsirAyah = null;
                return;
            }

            // Close the previously open one
            if (_openTafsirAyah != null)
                _openTafsirAyah.IsTafsirVisible = false;

            // Open the new one
            ayahVm.IsTafsirVisible = true;
            _openTafsirAyah = ayahVm;
        }

        private void UpdateSelectedSurahMeta()
        {
            if (_surahMetaByNumber.TryGetValue(SelectedSurah, out var meta))
                SelectedSurahMeta = meta;
            else
                SelectedSurahMeta = null;

            OnPropertyChanged(nameof(SelectedSurahMeta));
        }

        private void BuildSurahMetaDictionary()
        {
            // Meanings (common English “meaning/title gloss”)
            // Index 1..114 (0 unused)
            string[] meaning = new string[115]
            {
                "",
                "The Opening",
                "The Cow",
                "Family of Imran",
                "The Women",
                "The Table",
                "The Cattle",
                "The Heights",
                "The Spoils of War",
                "The Repentance",
                "Jonah",
                "Hud",
                "Joseph",
                "The Thunder",
                "Abraham",
                "The Rocky Tract",
                "The Bee",
                "The Night Journey",
                "The Cave",
                "Mary",
                "Ta-Ha",
                "The Prophets",
                "The Pilgrimage",
                "The Believers",
                "The Light",
                "The Criterion",
                "The Poets",
                "The Ant",
                "The Stories",
                "The Spider",
                "The Romans",
                "Luqman",
                "The Prostration",
                "The Confederates",
                "Sheba",
                "The Originator",
                "Ya-Sin",
                "Those Who Set the Ranks",
                "The Letter Saad",
                "The Groups",
                "The Forgiver",
                "Explained in Detail",
                "Consultation",
                "Ornaments of Gold",
                "The Smoke",
                "The Kneeling",
                "The Dunes",
                "Muhammad",
                "The Victory",
                "The Rooms",
                "Qaf",
                "The Winnowing Winds",
                "The Mount",
                "The Star",
                "The Moon",
                "The Most Merciful",
                "The Inevitable",
                "Iron",
                "The Pleading Woman",
                "The Exile",
                "She Who Is Examined",
                "The Ranks",
                "Friday",
                "The Hypocrites",
                "Mutual Disillusion",
                "Divorce",
                "Prohibition",
                "The Sovereignty",
                "The Pen",
                "The Inevitable Reality",
                "The Ascending Stairways",
                "Noah",
                "The Jinn",
                "The Wrapped One",
                "The Cloaked One",
                "Resurrection",
                "Man",
                "The Emissaries",
                "The Great News",
                "Those Who Drag Forth",
                "He Frowned",
                "The Overthrowing",
                "The Cleaving",
                "Defrauding",
                "The Splitting Open",
                "The Constellations",
                "The Nightcomer",
                "The Most High",
                "The Overwhelming",
                "The Dawn",
                "The City",
                "The Sun",
                "The Night",
                "The Morning Brightness",
                "The Relief",
                "The Fig",
                "The Clot",
                "The Night of Decree",
                "The Clear Evidence",
                "The Earthquake",
                "The Courser",
                "The Calamity",
                "Rivalry in Worldly Increase",
                "The Declining Day",
                "The Slanderer",
                "The Elephant",
                "Quraysh",
                "Small Kindnesses",
                "Abundance",
                "The Disbelievers",
                "Divine Support",
                "The Palm Fiber",
                "Sincerity",
                "The Daybreak",
                "Mankind"
            };

            // Revelation place (common classification)
            // Note: if you already have a definitive source elsewhere, we can swap later.
            // Index 1..114 (0 unused)
            string[] place = 
            {
                "",
                "Makkah",
                "Madinah",
                "Madinah",
                "Madinah",
                "Madinah",
                "Makkah",
                "Makkah",
                "Madinah",
                "Madinah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Madinah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Madinah",
                "Makkah",
                "Madinah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Madinah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Madinah",
                "Madinah",
                "Madinah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Madinah",
                "Madinah",
                "Madinah",
                "Madinah",
                "Madinah",
                "Madinah",
                "Madinah",
                "Madinah",
                "Madinah",
                "Madinah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Madinah",
                "Madinah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah",
                "Makkah"
            };

            for (int i = 1; i <= 114; i++)
            {
                _surahMetaByNumber[i] = new SurahMeta
                {
                    Number = i,
                    Meaning = meaning[i],
                    RevelationPlace = place[i]
                };
            }
        }

        // -------- INotifyPropertyChanged implementation --------
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
