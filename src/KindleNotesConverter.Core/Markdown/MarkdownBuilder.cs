using System.Text;

namespace KindleNotesConverter.Core.Markdown;

public class MarkdownBuilder : IMarkdownBuilder
{
    private readonly StringBuilder _content = new();

    public IMarkdownBuilder AddCite(string? cite)
    {
        if (!string.IsNullOrWhiteSpace(cite))
            _content.AppendLine($"{Markdown.Cite}{cite}");

        return this;
    }

    public IMarkdownBuilder AddParagraph(string? text)
    {
        if (!string.IsNullOrWhiteSpace(text))
            _content.AppendLine(text);

        return this;
    }

    public IMarkdownBuilder AddDivider()
    {
        _content.AppendLine(Markdown.Divider);

        return this;
    }

    public IMarkdownBuilder AddHeading(HeadingLevel headingLevel, string? heading)
    {
        if (!string.IsNullOrWhiteSpace(heading) && Markdown.HeadingLevels.TryGetValue(headingLevel, out string? level))
            _content.AppendLine($"{level} {heading}");

        return this;
    }

    public IMarkdownBuilder AddLineBreak()
    {
        _content.AppendLine();

        return this;
    }

    public IMarkdownBuilder AddTag(string? tag)
    {
        if (!string.IsNullOrWhiteSpace(tag))
            _content.AppendLine(tag.StartsWith(Markdown.Tag) ? tag : $"{Markdown.Tag}{tag}");

        return this;
    }

    public string Build()
    {
        return _content.ToString();
    }
}
