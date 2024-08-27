using HtmlAgilityPack;
using NotebookModel = KindleNoteConverter.Notebook.Models.Notebook;

namespace KindleNoteConverter.Notebook.Services.Parsers;

public sealed class KindleNotebookHtmlParser : INotebookParser
{
    public NotebookModel Parse(string html)
    {
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);
        
        var bookTitle = HtmlEntity.DeEntitize(htmlDocument.DocumentNode.SelectSingleNode($"//html/body/div/h1/div[contains(@class, '{KindleNotebookHtmlClasses.BookTitle}')]")?.InnerText);
        var bookAuthor = HtmlEntity.DeEntitize(htmlDocument.DocumentNode.SelectSingleNode($"//html/body/div/h1/div[contains(@class, '{KindleNotebookHtmlClasses.BookAuthors}')]")?.InnerText);
        var chapterTitle = HtmlEntity.DeEntitize(htmlDocument.DocumentNode.SelectSingleNode($"//html/body/div/h2[contains(@class, '{KindleNotebookHtmlClasses.BookChapterTitle}')]")?.InnerText);
        var noteLocation = GetNoteLocation(HtmlEntity.DeEntitize(htmlDocument.DocumentNode.SelectSingleNode($"//html/body/div/h3[contains(@class, '{KindleNotebookHtmlClasses.BookNoteLocation}')]")?.InnerText));
        
        var noteContentElements = htmlDocument.DocumentNode.SelectNodes($"//html/body/div[contains(@class, '{KindleNotebookHtmlClasses.BookNoteContent}')]");
        if (noteContentElements is null || noteContentElements.Count == 0)
        {
            // log empty document error
            return NotebookModel.Create(bookTitle, bookAuthor);
        }

        var notebook = NotebookModel.Create(bookTitle, bookAuthor);
        notebook.AddChapter(chapterTitle);

        foreach (var noteContentElement in noteContentElements)
        {
            var noteContentChildElements = noteContentElement.ChildNodes.ToArray();
            var noteContent = HtmlEntity.DeEntitize(noteContentChildElements[0]?.InnerText);

            notebook.AddNoteToLastChapter(chapterTitle, noteLocation, noteContent);
            
            noteLocation = GetNoteLocation(HtmlEntity.DeEntitize(noteContentChildElements.FirstOrDefault(x => x.Name == "h3")?.InnerText));
            chapterTitle = HtmlEntity.DeEntitize(noteContentChildElements.FirstOrDefault(x => x.Name == "h2")?.InnerText);
        }

        return notebook;
    }

    private static string? GetNoteLocation(string? location)
    {
        return location?[location.IndexOf("Location", StringComparison.Ordinal)..];
    }

    private static class KindleNotebookHtmlClasses
    {
        public const string BookTitle = "bookTitle";
        public const string BookAuthors = "authors";
        
        public const string BookChapterTitle = "sectionHeading";
            
        public const string BookNoteLocation = "noteHeading";
        public const string BookNoteContent = "noteText";
    }
}
