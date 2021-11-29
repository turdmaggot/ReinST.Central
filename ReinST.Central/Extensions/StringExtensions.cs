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
        /// <param name="decodeHTML">
        /// Optional parameter, default value is false. Determines if HTML entities are to be decoded.
        /// </param>
        /// <returns>
        /// Input string with HTML tags strpped off.
        /// </returns>
        public static string StripHTML(this string value, bool decodeHTML = false)
        {
            return StringHelper.StripHTML(value, decodeHTML);
        }

        /// <summary>
        /// Strips HTML tags off a given string. This uses RegEx matching.
        /// </summary>
        /// <param name="value">
        /// Input string.
        /// </param>
        /// <param name="decodeHTML">
        /// Optional parameter, default value is false. Determines if HTML entities are to be decoded.
        /// </param>
        /// <returns>
        /// Input string with HTML tags strpped off.
        /// </returns>
        public static string StripHTMLViaRegex(this string value, bool decodeHTML = false)
        {
            return StringHelper.StripHTMLViaRegex(value, decodeHTML);
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
