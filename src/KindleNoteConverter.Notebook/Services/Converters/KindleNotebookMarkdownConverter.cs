using KindleNoteConverter.Markdown.Builders;
using KindleNoteConverter.Markdown.Models;

using NotebookModel = KindleNoteConverter.Notebook.Models.Notebook;

namespace KindleNoteConverter.Notebook.Services.Converters;

public class KindleNotebookMarkdownConverter : IMarkdownConverter<NotebookModel>
{
    private readonly IMarkdownBuilder _markdownBuilder;

    public KindleNotebookMarkdownConverter(IMarkdownBuilder markdownBuilder)
    {
        _markdownBuilder = markdownBuilder;
    }

    public string Convert(NotebookModel notebook)
    {
        if (notebook?.Chapters is null)
            throw new ArgumentNullException(nameof(notebook));


        foreach (var chapter in notebook.Chapters)
        {
            if (chapter?.Notes is null)
                continue;

            _markdownBuilder.AddHeading(HeadingLevel.H4, chapter.Title);

            foreach (var note in chapter.Notes)
            {
                _markdownBuilder.AddParagraph(note.Location)
                    .AddCite(note.Content)
                    .AddDivider();
            }
        }

        return _markdownBuilder.Build();
    }
}
