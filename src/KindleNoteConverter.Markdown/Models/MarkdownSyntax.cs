namespace KindleNoteConverter.Markdown.Models;

public static class MarkdownSyntax
{
    public static readonly IDictionary<HeadingLevel, string> HeadingLevels = new Dictionary<HeadingLevel, string>
    {
        { HeadingLevel.H1, "#" },
        { HeadingLevel.H2, "##" },
        { HeadingLevel.H3, "###" },
        { HeadingLevel.H4, "####" },
        { HeadingLevel.H5, "#####" },
    };

    public static readonly string Divider = "---";
    public static readonly string Cite = ">";
    public static readonly string Tag = "#";
    public static readonly string ListItem = "- ";
    public static readonly string TodoItem = "- [ ] ";
    public static readonly string TodoItemCompleted = "- [x] ";

    public static readonly string BacklinkTemplate = "[[placeholder]]";

    public static readonly string HighlightTemplate = "==placeholder==";

    public static readonly string ItalicTemplate = "*placeholder*";
    public static readonly string BoldTemplate = "**placeholder**";
}
