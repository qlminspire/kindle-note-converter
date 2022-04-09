namespace KindleNoteConverter.Notebook.Models;

public class Notebook
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public IEnumerable<Chapter>? Chapters { get; set; }
}
