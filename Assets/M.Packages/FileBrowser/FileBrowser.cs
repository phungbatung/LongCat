using M.Packages.FileBrowser.Wrapper;

namespace M.Packages.FileBrowser
{
    public static class FileBrowser
    {
        private static BaseFileBrowser _browser;

        private static BaseFileBrowser Browser
        {
            get
            {
                if (_browser == null)
                {
#if UNITY_EDITOR
                    _browser = new FileBrowserEditor();
#elif UNITY_STANDALONE_WIN
                    _browser = new FileBrowserWindows();
#endif
                }
                return _browser;
            }
        }

        private const string _OPEN_FILE = "Open File";
        private const string _OPEN_FILES = "Open Files";
        private const string _OPEN_FOLDER = "Open Folder";
        private const string _SAVE_FILE = "Save File";


        private static string CurrentOpenSingleFile => Browser.CurrentOpenSingleFile;
        private static string[] CurrentOpenFiles => Browser.CurrentOpenFiles;
        private static string CurrentOpenSingleFolder => Browser.CurrentOpenSingleFolder;
        private static string CurrentSaveFile => Browser.CurrentSaveFile;


        public static bool LegacyFolderBrowser => false;
        public static bool AskOverwriteFile => true;


        /// <summary>
        /// Open a single file
        /// </summary>
        public static string OpenFile(string extension = "*")
        {
            Browser.OpenSingleFile(_OPEN_FILE, string.Empty, string.Empty, ExtensionFilter.GetFilter(extension));
            return CurrentOpenSingleFile;
        }

        /// <summary>
        /// Open multiple files
        /// </summary>
        public static string[] OpenFiles(string extension = "*")
        {
            Browser.OpenFiles(_OPEN_FILES, string.Empty, string.Empty, true, ExtensionFilter.GetFilter(extension));
            return CurrentOpenFiles;
        }

        public static string OpenFolder()
        {
            Browser.OpenSingleFolder(_OPEN_FOLDER, string.Empty);
            return CurrentOpenSingleFolder;
        }

        public static string SaveFile(string extension = "*")
        {
            Browser.SaveFile(_SAVE_FILE, string.Empty, string.Empty, ExtensionFilter.GetFilter(extension));
            return CurrentSaveFile;
        }
    }
}