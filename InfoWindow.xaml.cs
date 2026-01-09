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
                "distraction-free reading experience of the Holy Qur’an.\r\n\r\nThis application is currently in its IPR v.1.0 " +
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
                Body = "Current Version: IPR v.1.0\r\n\r\nThis release focuses on establishing the foundation of the Qur’an " +
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
                Body = "Qur’an Viewer is being developed by Ashary Tamano, an IT specialist, programmer, planner, and content developer " +
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
                Body = "The Qur’anic text and related compilation work referenced in this application " +
                "are associated with the efforts of Abdulbasit Tamano, whose work emphasizes clarity, " +
                "careful organization, and respect for the sacred text.\r\n\r\nThe role of the compiler " +
                "in a Qur’anic project is not to alter or reinterpret the Qur’an, but to ensure that the " +
                "material is organized, formatted, and presented in a way that supports accurate reading " +
                "and study. This includes attention to structure, consistency, and the integrity of the " +
                "source material.\r\n\r\nPrinciples guiding the compilation work reflected in this " +
                "application include:\r\n• Faithful presentation of the Qur’anic text\r\n• Respect " +
                "for established scholarly traditions\r\n• Clear organization to support readers and " +
                "learners\r\n• Careful handling of translations and explanatory materials\r\n\r\nThis " +
                "release does not attempt to introduce new interpretations or authoritative " +
                "claims. Instead, it aims to provide a stable and respectful platform where compiled " +
                "Qur’anic content can be accessed in a clear and readable digital form.\r\n\r\nFuture " +
                "versions of the Qur’an Viewer may further refine how compiled materials, translations, " +
                "and explanatory notes are presented, guided by feedback from readers, scholars, and " +
                "testers.\"\r\n\r\n"
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
                Body = "This release of the Qur’an Viewer is shared strictly for testing and " +
                "feedback purposes.\r\n\r\nDistribution Guidelines\r\n• Please do not re-upload this " +
                "build to public websites, app stores, or file-sharing platforms.\r\n• Please do not " +
                "modify or repackage the application for redistribution.\r\n• Sharing this build is " +
                "limited to trusted testers who understand that this is a pre-release version.\r\n\r\n" +
                "Content and Respect\r\nThe Qur’an is treated in this software with the intention of " +
                "respectful presentation. If you notice any issue related to text accuracy, formatting, " +
                "or rendering, please report it as a priority.\r\n\r\nFuture Releases\r\nLater releases " +
                "may include clearer licensing notes, formal acknowledgments, and packaging improvements " +
                "as the project approaches Beta and stable versions.\"\r\n"
            },
            ["compilation"] = new InfoPageVm
            {
                Title = "Compilation of the Holy Qur’an",
                Body = "This page provides a brief, respectful overview of how the Qur’an was preserved " +
                "and transmitted.\r\n\r\nThe Qur’an was revealed to the Prophet Muhammad (peace be upon " +
                "him) over time and was memorized by many companions and also recorded in written form. " +
                "After the Prophet’s passing, the Qur’anic text was carefully compiled to preserve it " +
                "in a unified written copy.\r\n\r\nAcross generations, the Qur’an has been preserved " +
                "through:\r\n• Memorization (hifz) by continuous chains of reciters\r\n• Written " +
                "manuscripts and careful copying traditions\r\n• Scholarly verification and teaching " +
                "transmission\r\n\r\nNote for this software\r\nThe Qur’an Viewer is a presentation " +
                "tool. It does not claim authority over the Qur’anic text. The project’s responsibility " +
                "is to display the content accurately and respectfully, and to make the reading " +
                "experience stable and clear.\"\r\n"
            },
            ["memorizing"] = new InfoPageVm
            {
                Title = "Memorizing the Holy Qur’an",
                Body = "Memorizing the Qur’an (hifz) is a long-standing tradition that combines " +
                "discipline, consistency, and sincerity.\r\n\r\nPractical reminders for " +
                "learners:\r\n• Begin with manageable portions and a steady schedule\r\n• Focus on " +
                "accuracy before speed\r\n• Review is essential; memorization strengthens through " +
                "repetition\r\n• Recitation with a qualified teacher helps protect correctness\r\n\r\n" +
                "Supportive role of the Qur’an Viewer\r\nThis application is being developed to " +
                "support reading and study. Future versions may include features that help with " +
                "memorization practice, such as:\r\n• Quick navigation by Surah and Ayah\r\n• Bookmarks " +
                "and progress tracking\r\n• Optional recitation audio (offline-friendly where " +
                "possible)\r\n\r\nThis release prioritizes the reading interface and core " +
                "navigation stability.\"\r\n"
            },
            ["bookmarks"] = new InfoPageVm
            {
                Title = "Bookmarks",
                Body = "Bookmarks are planned as a core feature for serious readers and professional " +
                "users.\r\n\r\nPurpose\r\nA bookmark should allow the reader to return quickly to a " +
                "specific location in the Qur’an, typically represented as:\r\n• Surah number\r\n• Ayah " +
                "number\r\n\r\nPlanned capabilities (future versions)\r\n• Add and remove bookmarks\r\n• " +
                "Rename bookmarks (example: “Study – Ayah on patience”)\r\n• Quick jump to a saved " +
                "Surah/Ayah location\r\n• Optional notes (short reflections or references)\r\n\r\nAlpha " +
                "status\r\nBookmarks may appear in the interface, but the feature is still under " +
                "development. This Alpha release is focused on verifying the reading experience and " +
                "ensuring the app remains stable as new features are introduced.\"\r\n"
            },
            ["search"] = new InfoPageVm
            {
                Title = "Search",
                Body = "Search is planned to support both everyday reading and professional " +
                "study.\r\n\r\nPossible search modes (future versions)\r\n• Search by Surah and " +
                "Ayah reference (example: 2:174)\r\n• Search within translation text\r\n• Optional " +
                "keyword search (with careful performance considerations)\r\n• Filter results by " +
                "Surah or theme (later expansion)\r\n\r\nAlpha status\r\nSearch is not finalized " +
                "in this release. The goal for Alpha is to validate the foundation—layout, typography, " +
                "navigation behavior, and overall stability—before adding full text search capability.\"\r\n}\r\n"
            },
            ["help"] = new InfoPageVm
            {
                Title = "Help: Using the Qur’an Viewer",
                Body =
@"This Alpha release is designed for reading, study, and feedback testing. Below are quick tips to help you use the viewer comfortably.

Window and Layout
• Maximize the window for the best reading experience.
• You can resize the window by dragging the edges or corners.
• The left panel is for navigation; the right panel is the reading area.

Navigation
• Use the Surah Index dropdown to select a Surah.
• Scroll on the right panel to read the ayahs.
• The header shows the Surah name and basic information.

Show / Hide Tafsir
• Each ayah card may include a Show button.
• Click Show to reveal Tafsir under that ayah.
• Click Hide to collapse the Tafsir section and continue reading.

Reading Tips
• For Arabic text, focus on the right-aligned verse lines.
• Translation appears under the Arabic.
• Tafsir is displayed in a distinct panel to separate it from translation.

Sending Feedback (Recommended)
When reporting issues, please include:
• Surah name and ayah number (if applicable)
• What you expected vs. what happened
• A screenshot if possible
• Your device and Windows version (optional but helpful)

Thank you for helping improve this early release."
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
