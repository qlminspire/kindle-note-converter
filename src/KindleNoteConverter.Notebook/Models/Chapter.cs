namespace KindleNoteConverter.Notebook.Models;

public class Chapter
{
    public string Title { get; set; }
    public ICollection<Note> Notes { get; set; } = new List<Note>();
}
