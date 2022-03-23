namespace KindleNotesConverter.Core.Models;

public class Note
{
    public string? Title { get; set; }
    public string? Location => Title?[Title.IndexOf("Location")..];
    public string? Content { get; set; }
}
