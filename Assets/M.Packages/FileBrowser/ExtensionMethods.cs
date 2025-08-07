
namespace M.Packages.FileBrowser
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Extension method for arrays.
        /// Dumps an array to a string.
        /// </summary>
        /// <param name="array">Array-instance to dump.</param>
        /// <param name="prefix">Prefix for every element (optional, default: empty).</param>
        /// <param name="postfix">Postfix for every element (optional, default: empty).</param>
        /// <param name="appendNewLine">Append new line, otherwise use the given delimiter (optional, default: false).</param>
        /// <param name="delimiter">Delimiter if appendNewLine is false (optional, default: "; ").</param>
        /// <returns>String with lines for all array entries.</returns>
        public static string CtDump<T>(this T[] array, string prefix = "", string postfix = "", bool appendNewLine = true, string delimiter = "; ")
        {
            if (array == null) // || array.Length <= 0)
                return null;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (T element in array)
            {
                if (0 < sb.Length)
                {
                    sb.Append(appendNewLine ? System.Environment.NewLine : delimiter);
                }

                sb.Append(prefix);
                sb.Append(element);
                sb.Append(postfix);
            }

            return sb.ToString();
        }
    }
}
