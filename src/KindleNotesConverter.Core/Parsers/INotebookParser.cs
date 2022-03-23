using KindleNotesConverter.Core.Models;

namespace KindleNotesConverter.Core.Parsers;
public interface INotebookParser
{
    KindleNotebook Parse(string notebookPath);
}
