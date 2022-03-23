namespace KindleNotesConverter.Core.Models;

public class KindleNotebook
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public IEnumerable<Chapter>? Chapters { get; set; }
}

