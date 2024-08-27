using HtmlAgilityPack;

using NotebookModel = KindleNoteConverter.Notebook.Models.Notebook;

namespace KindleNoteConverter.Notebook.Services.Parsers;

public sealed class KindleNotebookHtmlParser : INotebookParser
{
    public NotebookModel Parse(string html)
    {
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        const string container = "/html/body/div";
        
        var bookTitle = HtmlEntity.DeEntitize(htmlDocument.DocumentNode.SelectSingleNode($"{container}/h1/div[@class='{KindleNotebookHtmlClasses.BookTitle}']")?.InnerText);
        var bookAuthor = HtmlEntity.DeEntitize(htmlDocument.DocumentNode.SelectSingleNode($"{container}/h1/div[@class='{KindleNotebookHtmlClasses.BookAuthors}']")?.InnerText);
        var chapterTitle = HtmlEntity.DeEntitize(htmlDocument.DocumentNode.SelectSingleNode($"{container}/h2[@class='{KindleNotebookHtmlClasses.BookChapterTitle}']")?.InnerText);
        var noteLocation = GetNoteLocation(HtmlEntity.DeEntitize(htmlDocument.DocumentNode.SelectSingleNode($"{container}/h3[@class='{KindleNotebookHtmlClasses.BookNoteLocation}']")?.InnerText));
        
        var noteContentElements = htmlDocument.DocumentNode.SelectNodes($"{container}[@class='{KindleNotebookHtmlClasses.BookNoteContent}']");
        if (noteContentElements is null || noteContentElements.Count == 0)
        {
            // log empty document error
            return NotebookModel.Create(bookTitle, bookAuthor);
        }

        var notebook = NotebookModel.Create(bookTitle, bookAuthor);
        notebook.AddChapter(chapterTitle);

        foreach (var noteContentElement in noteContentElements)
        {
            var noteContent = HtmlEntity.DeEntitize(noteContentElement.FirstChild?.InnerText);

            notebook.AddNoteToLastChapter(chapterTitle, noteLocation, noteContent);
            
            noteLocation = GetNoteLocation(HtmlEntity.DeEntitize(noteContentElement.SelectSingleNode($"./h3[@class='{KindleNotebookHtmlClasses.BookNoteLocation}']")?.InnerText));
            chapterTitle = HtmlEntity.DeEntitize(noteContentElement.SelectSingleNode($"./h2[@class='{KindleNotebookHtmlClasses.BookChapterTitle}']")?.InnerText);
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
