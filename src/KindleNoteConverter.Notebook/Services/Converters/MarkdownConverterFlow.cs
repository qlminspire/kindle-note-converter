using KindleNoteConverter.Notebook.Services.Storage;
using KindleNoteConverter.Notebook.Services.Parsers;

using NotebookModel = KindleNoteConverter.Notebook.Models.Notebook;

namespace KindleNoteConverter.Notebook.Services.Converters
{
    public class MarkdownConverterFlow : IMarkdownConverterFlow
    {
        private readonly INotebookParser _parser;
        private readonly IMarkdownConverter<NotebookModel> _markdownConverter;
        private readonly IStorage _storage;

        public MarkdownConverterFlow(INotebookParser parser, IMarkdownConverter<NotebookModel> markdownConverter, IStorage storage)
        {
            _parser = parser;
            _markdownConverter = markdownConverter;
            _storage = storage;
        }

        public void Convert(string path, string? outputPath)
        {
            var notebook = _parser.Parse(path);
            var notebookMarkdown = _markdownConverter.Convert(notebook);

            var notebookPath = Path.ChangeExtension(outputPath ?? path, "md");

            _storage.Save(notebookPath, notebookMarkdown);
        }
    }
}
