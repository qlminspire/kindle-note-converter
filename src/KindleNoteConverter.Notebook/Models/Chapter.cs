using KindleNoteConverter.Notebook.Extensions;

namespace KindleNoteConverter.Notebook.Models;

public sealed class Chapter
{
    private static readonly Error NotCreated = new("Chapter not created", "Chapter must have title");
    
    public string Title { get; private init; }
    public ICollection<Note> Notes { get; private init; }

    private Chapter(){}
    
    public static Result<Chapter> Create(string? title)
    {
        var trimmedTitle = title?.RemoveExtraSpacesAndTrim();
        if (string.IsNullOrWhiteSpace(trimmedTitle))
            return Result.Failure<Chapter>(NotCreated);
        
        return new Chapter
        {
            Title = trimmedTitle,
            Notes = new List<Note>()
        };
    }

    public bool IsSameChapter(string? title)
    {
        var trimmedTitle = title?.RemoveExtraSpacesAndTrim();
        return string.IsNullOrEmpty(trimmedTitle) || string.Equals(Title, trimmedTitle, StringComparison.OrdinalIgnoreCase);
    }

    public Result AddNote(string? location, string? content)
    {
        var result = Note.Create(location, content);
        if (result.IsSuccess)
            Notes.Add(result.Value);
        
        return result;
    }
}
