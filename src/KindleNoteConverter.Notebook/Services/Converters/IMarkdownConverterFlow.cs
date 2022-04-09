namespace KindleNoteConverter.Notebook.Services.Converters;

public interface IMarkdownConverterFlow
{
    void Convert(string path, string? outputPath);
}
