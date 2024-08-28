using System.Text.RegularExpressions;

namespace KindleNoteConverter.Notebook.Extensions;

internal static class StringExtensions
{
    private static readonly Regex ExtraWhiteSpaceTrim = new(@"\s\s+", RegexOptions.Compiled);
    
    public static string RemoveExtraSpacesAndTrim(this string text)
    {
        return !string.IsNullOrEmpty(text) 
            ? ExtraWhiteSpaceTrim.Replace(text, " ").Trim() 
            : text;
    }
}