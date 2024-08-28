namespace KindleNoteConverter.Markdown.Models;

public static class MarkdownSyntax
{
    public static readonly IDictionary<HeadingLevel, string> HeadingLevels = new Dictionary<HeadingLevel, string>
    {
        { HeadingLevel.H1, "#" },
        { HeadingLevel.H2, "##" },
        { HeadingLevel.H3, "###" },
        { HeadingLevel.H4, "####" },
        { HeadingLevel.H5, "#####" }
    };

    public const string Divider = "---";
    public const string Cite = ">";
    public const string Tag = "#";
    public const string ListItem = "- ";
    public const string TodoItem = "- [ ] ";
    public const string TodoItemCompleted = "- [x] ";

    public const string BacklinkTemplate = "[[placeholder]]";

    public const string HighlightTemplate = "==placeholder==";

    public const string ItalicTemplate = "*placeholder*";
    public const string BoldTemplate = "**placeholder**";
}
