using System.Text;
using KindleNoteConverter.Notebook.Services.Markdown;
using KindleNoteConverter.Notebook.Services.Storage;
using KindleNoteConverter.Notebook.Services.Parsers;
using Microsoft.Extensions.Logging;
using NotebookModel = KindleNoteConverter.Notebook.Models.Notebook;

namespace KindleNoteConverter.Notebook.Services.Converters;

public sealed class KindleNotebookConverter : IKindleNotebookConverter
{
    private readonly INotebookParser _parser;
    private readonly IMarkdownGenerator<NotebookModel> _markdownGenerator;
    private readonly IStorage _storage;
    private readonly ILogger<KindleNotebookConverter> _logger;

    public KindleNotebookConverter(INotebookParser parser, IMarkdownGenerator<NotebookModel> markdownGenerator, IStorage storage, ILogger<KindleNotebookConverter> logger)
    {
        _parser = parser;
        _markdownGenerator = markdownGenerator;
        _storage = storage;
        _logger = logger;
    }

    public async Task Convert(string path, string? outputPath, CancellationToken cancellationToken = default)
    {
        var targetDirectory = Path.GetDirectoryName(outputPath);
        if (targetDirectory is not null && !Directory.Exists(targetDirectory))
        {
            Directory.CreateDirectory(targetDirectory);
            _logger.LogTrace("The output path directory '{path}' does not exist. The directory created automatically", outputPath);
        }
        
        var content = await File.ReadAllTextAsync(path, Encoding.UTF8, cancellationToken);
        
        var notebook = _parser.Parse(content);
        if (notebook.HasNoChapters)
        {
            _logger.LogWarning("The passed notebook '{path}' does not have chapters. The MD file will not be generated", path);
            return;
        }

        var notebookMarkdownPath = Path.ChangeExtension(outputPath ?? path, "md");
        var notebookMarkdown = _markdownGenerator.Generate(notebook);

        await _storage.Store(notebookMarkdownPath, notebookMarkdown, cancellationToken);
    }
}