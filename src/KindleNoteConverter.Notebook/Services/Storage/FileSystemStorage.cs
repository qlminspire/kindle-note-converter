namespace KindleNoteConverter.Notebook.Services.Storage;

public sealed class FileSystemStorage : IStorage
{
    public Task Store(string path, string content, CancellationToken cancellationToken = default)
    {
        return File.WriteAllTextAsync(path, content, cancellationToken);
    }
}
