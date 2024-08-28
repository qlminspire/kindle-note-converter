using System.Text;
using KindleNoteConverter.Notebook.Services.Markdown;
using KindleNoteConverter.Notebook.Services.Storage;
using KindleNoteConverter.Notebook.Services.Parsers;

using NotebookModel = KindleNoteConverter.Notebook.Models.Notebook;

namespace KindleNoteConverter.Notebook.Services.Converters;

public sealed class KindleNotebookConverter : IKindleNotebookConverter
{
    private readonly INotebookParser _parser;
    private readonly IMarkdownGenerator<NotebookModel> _markdownGenerator;
    private readonly IStorage _storage;

    public KindleNotebookConverter(INotebookParser parser, IMarkdownGenerator<NotebookModel> markdownGenerator, IStorage storage)
    {
        _parser = parser;
        _markdownGenerator = markdownGenerator;
        _storage = storage;
    }

    public async Task Convert(string path, string? outputPath, CancellationToken cancellationToken = default)
    {
        if (!File.Exists(path))
        {
            // logger: error - the source file is not exists
            return;
        }

        var targetDirectory = Path.GetDirectoryName(outputPath);
        if (targetDirectory is not null && !Directory.Exists(targetDirectory))
        {
            Directory.CreateDirectory(targetDirectory);
            // logger: trace - the target directory created
        }
        
        var content = await File.ReadAllTextAsync(path, Encoding.UTF8, cancellationToken);
        
        var notebook = _parser.Parse(content);
        var notebookMarkdown = _markdownGenerator.Generate(notebook);

        var notebookMarkdownPath = Path.ChangeExtension(outputPath ?? path, "md");

        await _storage.Store(notebookMarkdownPath, notebookMarkdown, cancellationToken);
    }
}