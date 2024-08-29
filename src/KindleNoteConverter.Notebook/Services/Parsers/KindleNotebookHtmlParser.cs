using HtmlAgilityPack;
using KindleNoteConverter.Notebook.Extensions;

using NotebookModel = KindleNoteConverter.Notebook.Models.Notebook;

namespace KindleNoteConverter.Notebook.Services.Parsers;

public sealed class KindleNotebookHtmlParser : INotebookParser
{
    public NotebookModel Parse(string html)
    {
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);
        
        var documentNode = htmlDocument.DocumentNode;
        
        var bookTitle = documentNode.GetNodeText(KindleNotebookHtml.XPath.BookTitle);
        var bookAuthor = documentNode.GetNodeText(KindleNotebookHtml.XPath.BookAuthors);
        var chapterTitle = documentNode.GetNodeText(KindleNotebookHtml.XPath.FirstChapterTitle);
        var noteLocation = GetNoteLocation(documentNode.GetNodeText(KindleNotebookHtml.XPath.FirstNoteLocation));
        
        var notebook = NotebookModel.Create(bookTitle, bookAuthor);
        notebook.AddChapter(chapterTitle);
        
        var noteContentElements = documentNode.SelectNodes(KindleNotebookHtml.XPath.NoteContent)?.ToArray() ?? [];
        foreach (var noteContentElement in noteContentElements)
        {
            var noteContent = noteContentElement.GetNodeText(KindleNotebookHtml.XPath.NoteContentText);

            notebook.AddNoteToLastChapter(chapterTitle, noteLocation, noteContent);
            
            noteLocation = GetNoteLocation(noteContentElement.GetNodeText(KindleNotebookHtml.XPath.NoteLocation));
            chapterTitle = noteContentElement.GetNodeText(KindleNotebookHtml.XPath.ChapterTitle);
        }

        return notebook;
    }

    private static string? GetNoteLocation(string? location)
    {
        return location?[location.IndexOf("Location", StringComparison.Ordinal)..];
    }

    private static class KindleNotebookHtml
    {
        private static class CssClasses
        {
            public const string BookTitle = "bookTitle";
            public const string BookAuthors = "authors";
        
            public const string ChapterTitle = "sectionHeading";
            
            public const string NoteLocation = "noteHeading";
            public const string NoteContent = "noteText";
        }

        internal static class XPath
        {
            private const string Container = "/html/body/div";

            public const string BookTitle = $"{Container}/h1/div[@class='{CssClasses.BookTitle}']";

            public const string BookAuthors =
                $"{Container}/h1/div[@class='{CssClasses.BookAuthors}']";

            public const string FirstChapterTitle =
                $"{Container}/h2[@class='{CssClasses.ChapterTitle}']";

            public const string ChapterTitle = $"./h2[@class='{CssClasses.ChapterTitle}']";
            
            public const string FirstNoteLocation =
                $"{Container}/h3[@class='{CssClasses.NoteLocation}']";

            public const string NoteLocation = $"./h3[@class='{CssClasses.NoteLocation}']";

            public const string NoteContent = $"{Container}[@class='{CssClasses.NoteContent}']";

            public const string NoteContentText = "./text()";
        }
    }
}
