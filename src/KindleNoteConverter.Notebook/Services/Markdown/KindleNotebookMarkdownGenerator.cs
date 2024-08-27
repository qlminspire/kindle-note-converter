using KindleNoteConverter.Markdown.Builders;
using KindleNoteConverter.Markdown.Models;
using NotebookModel = KindleNoteConverter.Notebook.Models.Notebook;

namespace KindleNoteConverter.Notebook.Services.Markdown;

public sealed class KindleNotebookMarkdownGenerator : IMarkdownGenerator<NotebookModel>
{
    private readonly IMarkdownBuilder _markdownBuilder;

    public KindleNotebookMarkdownGenerator(IMarkdownBuilder markdownBuilder)
    {
        _markdownBuilder = markdownBuilder;
    }

    public string Generate(NotebookModel notebook)
    {
        if (notebook.Chapters is null)
            throw new ArgumentNullException(nameof(notebook.Chapters));

        foreach (var chapter in notebook.Chapters)
        {
            _markdownBuilder.AddHeading(HeadingLevel.H4, chapter.Title);

            foreach (var note in chapter.Notes)
            {
                _markdownBuilder.AddParagraph(note.Location)
                    .AddCite(note.Content);
            }
        }

        return _markdownBuilder.Build();
    }
}
