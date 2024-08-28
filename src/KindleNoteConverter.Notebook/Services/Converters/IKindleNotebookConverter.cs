namespace KindleNoteConverter.Notebook.Services.Converters;

public interface IKindleNotebookConverter
{
    Task Convert(string path, string? outputPath, CancellationToken cancellationToken = default);
}
