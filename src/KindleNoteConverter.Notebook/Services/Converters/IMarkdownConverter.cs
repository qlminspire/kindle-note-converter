namespace KindleNoteConverter.Notebook.Services.Converters;

public interface IMarkdownConverter<T> where T : class
{
    string Convert(T model);
}
