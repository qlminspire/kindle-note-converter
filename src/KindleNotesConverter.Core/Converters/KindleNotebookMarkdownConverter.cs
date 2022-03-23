using KindleNotesConverter.Core.Markdown;
using KindleNotesConverter.Core.Models;
using System.Runtime.CompilerServices;

namespace KindleNotesConverter.Core.Converters;

public class KindleNotebookMarkdownConverter : IMarkdownConverter<KindleNotebook>
{
    private readonly IMarkdownBuilder _markdownBuilder;

    public KindleNotebookMarkdownConverter(IMarkdownBuilder markdownBuilder)
    {
        _markdownBuilder = markdownBuilder;
    }

    public string Convert(KindleNotebook notebook, [CallerArgumentExpression("notebook")] string message = "")
    {
        if (notebook?.Chapters is null)
            throw new ArgumentNullException(message);

 
        foreach (var chapter in notebook.Chapters)
        {
            if (chapter?.Notes is null)
            {
                continue;
            }

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
