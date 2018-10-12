using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ChristmasMothers.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string RemoveDiacritics(this string str)
        {
            var normalizedString = str.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string RemoveLigatures(this string str)
        {
            return str
                .Replace("Æ", "A")
                .Replace("Œ", "O")
                .Replace("æ", "a")
                .Replace("œ", "o");
        }

        public static string ReplaceIgnoreCase(this string str, string oldValue, string newValue)
        {
            var escaped = Regex.Escape(oldValue);
            return Regex.Replace(str, escaped, newValue, RegexOptions.IgnoreCase);
        }
    }
}