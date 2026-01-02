using System.Collections.Generic;
using System.Windows;

namespace QuranViewer
{
    public partial class InfoWindow : Window
    {
        public InfoWindow(string key)
        {
            InitializeComponent();
            DataContext = InfoPageContent.Get(key);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class InfoPageVm
    {
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
    }

    public static class InfoPageContent
    {
        private static readonly Dictionary<string, InfoPageVm> Pages = new()
        {
            ["intro"] = new InfoPageVm
            {
                Title = "Introduction to the Qur’an Viewer",
                Body = "The Qur’an Viewer is a developing desktop application designed to provide a clear, respectful, and " +
                "distraction-free reading experience of the Holy Qur’an.\r\n\r\nThis application is currently in its Alpha stage " +
                "and is being shared with a small group of trusted testers. The primary purpose of this release is to validate the " +
                "core viewing experience, confirm correct rendering of Arabic text, and gather feedback that will guide future " +
                "design and feature decisions.\r\n\r\nAt this stage, the focus is on readability, correctness of layout, and " +
                "basic navigation. Some features may be incomplete, experimental, or subject to change. The interface and " +
                "behavior you see now should be treated as provisional rather than final.\r\n\r\nYour feedback—whether " +
                "technical, usability-related, or conceptual—is highly valued. Observations from this Alpha release will " +
                "directly influence how the Qur’an Viewer evolves in subsequent versions.\"\r\n"
            },
            ["features"] = new InfoPageVm
            {
                Title = "Software Features, Versioning",
                Body = "Current Version: v0.1-alpha\r\n\r\nThis Alpha release focuses on establishing the foundation of the Qur’an " +
                "Viewer. The following features are either implemented or under active evaluation:\r\n\r\n• " +
                "Surah-based navigation using a left-panel index\r\n• Ayah-by-ayah display with Arabic text rendered in a " +
                "traditional, readable font\r\n• Right-to-left (RTL) layout handling for Arabic script\r\n• Basic translation " +
                "display alongside the Qur’anic text\r\n• Scroll-based reading experience for continuous study\r\n\r\nLimitations " +
                "of this Alpha version:\r\n\r\n• Feature set is incomplete and subject to change\r\n• Advanced navigation, " +
                "bookmarks, search, and settings are not finalized\r\n• User interface elements may be refined or reorganized\r\n• " +
                "Performance optimizations are ongoing\r\n\r\nVersioning Note:\r\n\r\nThe v0.1-alpha designation indicates an " +
                "early, experimental release. Backward compatibility is not guaranteed at this stage. Feedback gathered " +
                "during this phase will inform both feature priorities and architectural decisions for future Beta and stable " +
                "releases.\"\r\n\r\n"
            },
            ["about_developer"] = new InfoPageVm
            {
                Title = "The Developer, Ashary Tamano",
                Body = "Qur’an Viewer is being developed by Ashary Tamano, an IT specialist, planner, and content developer " +
                "with a long-standing commitment to building practical systems and meaningful digital projects.\r\n\r\nThis " +
                "application is part of a broader personal mission: to contribute to accessible Islamic learning resources " +
                "through respectful design, careful presentation, and sustainable software development.\r\n\r\nDevelopment " +
                "priorities for this project include:\r\n\r\n• Readability and clarity of the Qur’anic text\r\n• Proper " +
                "right-to-left rendering and typography for Arabic\r\n• A clean interface that supports reflection " +
                "and study\r\n• A stable architecture that can grow into bookmarks, search, and tafsir support\r\n• " +
                "Offline-friendly distribution for wider accessibility\r\n\r\nThis project is being built step-by-step, " +
                "with a preference for correctness and stability over rushed feature expansion. Feedback from trusted testers " +
                "during the Alpha phase will help shape the direction of improvements and the roadmap toward a more complete " +
                "release.\"\r\n\r\n"
            },
            ["about_compiler"] = new InfoPageVm
            {
                Title = "The Compiler, Abdulbasit Tamano",
                Body = "Write your compiler profile here..."
            },
            ["tafsir_explained"] = new InfoPageVm
            {
                Title = "Translation and Tafsir Explained",
                Body = "A Qur’an translation and a tafsir serve different purposes. Understanding the distinction helps readers " +
                "benefit properly from both.\r\n\r\n1) Translation\r\nA translation attempts to convey the meaning of the " +
                "Qur’anic text into another language. Because languages differ in structure and depth, a translation is best " +
                "understood as an interpretation of meaning, not a replacement for the Arabic original.\r\n\r\nIn the " +
                "Qur’an Viewer, translations are presented to help readers understand the message, but they should always be " +
                "read alongside the Arabic text whenever possible.\r\n\r\n2) Tafsir\r\nTafsir is explanation and commentary " +
                "that provides context, clarifies linguistic meaning, and connects verses to historical background, themes, " +
                "and scholarly interpretation. Tafsir may address:\r\n• The reason a verse was revealed\r\n• Linguistic nuances " +
                "and word meanings\r\n• Connections to other verses\r\n• Practical lessons and reflections\r\n• Scholarly " +
                "interpretations across schools of thought\r\n\r\nWhy both matter\r\n• The Arabic text is the primary " +
                "source.\r\n• Translation supports accessibility and basic understanding.\r\n• Tafsir deepens comprehension " +
                "by adding context and guided explanation.\r\n\r\nNote for this Alpha release\r\nThis Alpha version focuses " +
                "on validating the core reading experience. Integration and presentation of additional commentary, tafsir notes, " +
                "and advanced study features are planned for later versions."
            },
            ["copyright"] = new InfoPageVm
            {
                Title = "Copyright and Distribution",
                Body = "Write your copyright/distribution text here..."
            },
            ["compilation"] = new InfoPageVm
            {
                Title = "Compilation of the Holy Qur’an",
                Body = "Write your overview here..."
            },
            ["memorizing"] = new InfoPageVm
            {
                Title = "Memorizing the Holy Qur’an",
                Body = "Write your memorization guidance here..."
            },
            ["bookmarks"] = new InfoPageVm
            {
                Title = "Bookmarks",
                Body = "Bookmarks page (coming soon)."
            },
            ["search"] = new InfoPageVm
            {
                Title = "Search",
                Body = "Search page (coming soon)."
            }
        };

        public static InfoPageVm Get(string key)
        {
            return Pages.TryGetValue(key, out var vm)
                ? vm
                : new InfoPageVm { Title = "Page", Body = "Content not found." };
        }
    }
}
