namespace KindleNoteConverter.Notebook.Services.Markdown;

public interface IMarkdownGenerator<in T> where T : class
{
    string Generate(T model);
}
