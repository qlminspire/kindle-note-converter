using KindleNoteConverter.Markdown.Models;

namespace KindleNoteConverter.Markdown.Builders;

public interface IMarkdownBuilder
{
    IMarkdownBuilder AddHeading(HeadingLevel level, string? heading);
    IMarkdownBuilder AddParagraph(string? text);
    IMarkdownBuilder AddCite(string? cite);
    IMarkdownBuilder AddTag(string? tag);
    IMarkdownBuilder AddLineBreak();
    IMarkdownBuilder AddDivider();
    IMarkdownBuilder AddSymbol(string? symbol);

    string Build();
}
