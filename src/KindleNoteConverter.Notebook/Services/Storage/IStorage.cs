namespace KindleNoteConverter.Notebook.Services.Storage;

public interface IStorage
{
    Task Store(string path, string content, CancellationToken cancellationToken = default);
}
