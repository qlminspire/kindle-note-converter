using KindleNoteConverter.Notebook.Extensions;

namespace KindleNoteConverter.Notebook.Models;

public sealed class Notebook
{
    public string? Title { get; private set; }
    public string? Author { get; private set; }
    public ICollection<Chapter> Chapters { get; private init; }
    
    private Notebook(){}

    public static Notebook Create(string? title, string? author)
    {
        return new Notebook
        {
            Title = title is not null ? title.RemoveExtraSpacesAndTrim() : title,
            Author = author is not null ? author.RemoveExtraSpacesAndTrim() : author,
            Chapters = new List<Chapter>()
        };
    }

    public Result AddChapter(string? title)
    {
        var result = Chapter.Create(title);
        if(result.IsSuccess)
            Chapters.Add(result.Value);
        
        return result;
    }

    public Result AddNoteToLastChapter(string? chapterTitle, string? location, string? content)
    {
        var lastChapter = Chapters.LastOrDefault();
        if (lastChapter is not null && lastChapter.IsSameChapter(chapterTitle))
            return lastChapter.AddNote(location, content);
        
        var chapter = Chapter.Create(chapterTitle);
        if (chapter.IsFailure)
            return chapter;
        
        Chapters.Add(chapter.Value);
        
        return chapter.Value.AddNote(location, content);
    }
}
