using KindleNotesConverter.Core.Converters;
using KindleNotesConverter.Core.Models;
using KindleNotesConverter.Core.Parsers;
using KindleNotesConverter.Core.Storage;

namespace KindleNotesConverter.Core
{
    public class KindleNotebookConverter
    {
        private readonly INotebookParser _parser;
        private readonly IMarkdownConverter<KindleNotebook> _markdownConverter;

        public KindleNotebookConverter(INotebookParser parser, IMarkdownConverter<KindleNotebook> markdownConverter)
        {
            _parser = parser;
            _markdownConverter = markdownConverter;
        }

        public string Convert(string path)
        {
            var kindleNotebook = _parser.Parse(path);
            return _markdownConverter.Convert(kindleNotebook);
        }
    }
}
