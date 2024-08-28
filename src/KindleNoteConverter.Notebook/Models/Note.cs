using KindleNoteConverter.Notebook.Extensions;

namespace KindleNoteConverter.Notebook.Models;

public sealed class Note
{
    private static readonly Error NotCreated = new("Note not created", "Note must have content");
    
    public string? Location { get; private init; }
    public string Content { get; private init; }
    
    private Note(){}

    public static Result<Note> Create(string? location, string? content)
    {
        var trimmedLocation = location?.RemoveExtraSpacesAndTrim();
        
        var trimmedContent = content?.RemoveExtraSpacesAndTrim();
        if (string.IsNullOrWhiteSpace(trimmedContent))
            return Result.Failure<Note>(NotCreated);
        
        return new Note
        {
            Location = trimmedLocation,
            Content = trimmedContent
        };
    }
}
