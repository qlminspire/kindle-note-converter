using HtmlAgilityPack;

namespace KindleNoteConverter.Notebook.Extensions;

internal static class HtmlNodeExtensions
{
    public static string? GetNodeText(this HtmlNode? node, string xPath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(xPath);
        
        return HtmlEntity.DeEntitize(node?.SelectSingleNode(xPath)?.InnerText);
    }
}