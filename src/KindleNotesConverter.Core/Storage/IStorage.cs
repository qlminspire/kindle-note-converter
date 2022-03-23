namespace KindleNotesConverter.Core.Storage;

public interface IStorage
{
    void Save(string outputPath, string content);
}
