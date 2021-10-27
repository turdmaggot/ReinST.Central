using ReinST.Central.Helpers;

namespace ReinST.Central.Extensions
{
    /// <summary>
    /// Extension for strings.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Strips HTML tags off a given string. This uses the HTMLAgilityLibrary.
        /// </summary>
        /// <param name="value">
        /// Input string.
        /// </param>
        /// <returns>
        /// Input string with HTML tags strpped off.
        /// </returns>
        public static string StripHTML(this string value)
        {
            return StringHelper.StripHTML(value);
        }

        /// <summary>
        /// Detects if a given string has HTML tags. This uses regex matching.
        /// </summary>
        /// <param name="value">
        /// Input string to check.
        /// </param>
        /// <returns>
        /// Returns true if there's HTML tags in the string.
        /// </returns>
        public static bool ContainsHTML(this string value)
        {
            return StringHelper.ContainsHTML(value);
        }
    }
}
