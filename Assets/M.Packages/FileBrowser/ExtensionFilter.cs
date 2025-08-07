
namespace M.Packages.FileBrowser
{
    /// <summary>Filter for extensions.</summary>
    public readonly struct ExtensionFilter
    {
        private const string _NAME = "Name='";
        private const string _EXTENSION = "Extensions='";
        private const string _TEXT_START = "{";
        private const string _TEXT_END = "}";
        private const string _TEXT_DELIMITER = "', ";
        private const string _TEXT_DELIMITER_END = "'";
        public const string _TEXT_ALL_FILES = "All Files";

        

        public string Name { get; }
        public string[] Extensions { get; }

        public ExtensionFilter(string filterName, params string[] filterExtensions)
        {
            Name = filterName;
            Extensions = filterExtensions;
        }

        public override string ToString()
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();

            result.Append(GetType().Name);
            result.Append(_TEXT_START);

            result.Append(_NAME);
            result.Append(Name);
            result.Append(_TEXT_DELIMITER);

            result.Append(_EXTENSION);
            result.Append(Extensions.CtDump());
            result.Append(_TEXT_DELIMITER_END);

            result.Append(_TEXT_END);

            return result.ToString();
        }
        
        public static ExtensionFilter[] GetFilter(params string[] extensions)
        {
            if (extensions?.Length > 0)
            {
                if (extensions.Length == 1 && "*".Equals(extensions[0]))
                    return null;

                ExtensionFilter[] filter = new ExtensionFilter[extensions.Length];

                for (int ii = 0; ii < extensions.Length; ii++)
                {
                    string extension = string.IsNullOrEmpty(extensions[ii]) ? "*" : extensions[ii];

                    if (extension.Equals("*"))
                    {
                        filter[ii] = new ExtensionFilter(_TEXT_ALL_FILES, PlatformHelper.IsMacOSPlatform ? string.Empty : extension);
                    }
                    else
                    {
                        filter[ii] = new ExtensionFilter(extension, extension);
                    }
                }

                return filter;
            }

            return null;
        }
    }
}