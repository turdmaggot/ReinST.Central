using ReinST.Central.Helpers;

namespace ReinST.Central.Extensions
{
    public static class StringExtensions
    {
        public static string StripHTML(this string value)
        {
            return StringHelper.StripHTML(value);
        }
    }
}
