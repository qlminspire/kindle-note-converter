namespace KindleNotesConverter.Core.Markdown;

internal class Markdown
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
}
