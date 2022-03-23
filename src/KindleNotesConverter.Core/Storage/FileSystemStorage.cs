namespace KindleNotesConverter.Core.Storage;

public class FileSystemStorage : IStorage
{
    public void Save(string outputPath, string content)
    {
        File.WriteAllText(outputPath, content);
    }
}
