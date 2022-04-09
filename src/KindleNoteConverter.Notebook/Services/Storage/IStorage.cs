namespace KindleNoteConverter.Notebook.Services.Storage;

public interface IStorage
{
    void Save(string outputPath, string content);
}
