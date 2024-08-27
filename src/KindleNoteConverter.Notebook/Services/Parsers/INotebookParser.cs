using NotebookModel = KindleNoteConverter.Notebook.Models.Notebook;

namespace KindleNoteConverter.Notebook.Services.Parsers;

public interface INotebookParser
{
    NotebookModel Parse(string content);
}
