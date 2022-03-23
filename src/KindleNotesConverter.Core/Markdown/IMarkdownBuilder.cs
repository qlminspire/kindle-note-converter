namespace KindleNotesConverter.Core.Markdown;

public interface IMarkdownBuilder
{
    IMarkdownBuilder AddHeading(HeadingLevel level, string? heading);
    IMarkdownBuilder AddParagraph(string? text);
    IMarkdownBuilder AddCite(string? cite);
    IMarkdownBuilder AddTag(string? tag);
    IMarkdownBuilder AddLineBreak();
    IMarkdownBuilder AddDivider();

    string Build();
}
