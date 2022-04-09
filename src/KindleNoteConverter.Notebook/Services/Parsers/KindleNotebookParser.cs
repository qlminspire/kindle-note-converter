using HtmlAgilityPack;

using KindleNoteConverter.Notebook.Models;

using NotebookModel = KindleNoteConverter.Notebook.Models.Notebook;

namespace KindleNoteConverter.Notebook.Services.Parsers;

public class KindleNotebookParser : INotebookParser
{
    private readonly string[] _searchSelectors = {
                KindleClassSelectors.Notebook.Title,
                KindleClassSelectors.Notebook.Author,
                KindleClassSelectors.Notebook.Chapter,
                KindleClassSelectors.Notebook.Note.Title,
                KindleClassSelectors.Notebook.Note.Content
            };

    public NotebookModel Parse(string notebookPath)
    {
        var doc = new HtmlDocument();
        doc.Load(notebookPath);

        var documentContent = doc.DocumentNode.SelectNodes("/html/body/div/div");

        var documentNodes = documentContent.Where(x => _searchSelectors.Contains(x.Attributes["class"].Value)).ToArray();

        NotebookModel notebook = new();

        ICollection<Chapter> chapters = new List<Chapter>();

        Chapter? currentChapter = null;

        for (var i = 0; i < documentNodes.Length; i++)
        {
            var node = documentNodes[i];
            var nodeText = node.InnerText.Trim();

            var attribute = node.Attributes["class"].Value;

            if (attribute == KindleClassSelectors.Notebook.Title)
                notebook.Title = nodeText;
            else if (attribute == KindleClassSelectors.Notebook.Author)
                notebook.Author = nodeText;
            else if (attribute == KindleClassSelectors.Notebook.Chapter)
            {
                currentChapter = new Chapter()
                {
                    Title = nodeText,
                    Notes = new List<Note>()
                };

                if (chapters.LastOrDefault() != currentChapter)
                    chapters.Add(currentChapter);
            }
            else if (currentChapter?.Notes != null && attribute == KindleClassSelectors.Notebook.Note.Title)
                currentChapter.Notes.Add(new Note { Title = nodeText, Content = documentNodes[i + 1].InnerText.Trim() });
        }

        notebook.Chapters = chapters;

        return notebook;
    }
}
