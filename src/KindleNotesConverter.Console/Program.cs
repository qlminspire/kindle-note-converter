using Cocona;
using KindleNotesConverter.Core.Converters;
using KindleNotesConverter.Core.Markdown;
using KindleNotesConverter.Core.Models;
using KindleNotesConverter.Core.Parsers;
using KindleNotesConverter.Core.Storage;

CoconaApp.Run(([Option('p', Description = "Path to kindle notebook in HTML format")]string path, 
    [Option('o', Description = "Path to result file in markdown format")]string? outputPath) =>
{
    INotebookParser kindleNotebookParser = new KindleNotebookParser();
    var kindleNotebook = kindleNotebookParser.Parse(path);

    IMarkdownBuilder markdownBuilder = new MarkdownBuilder();
    IMarkdownConverter<KindleNotebook> markdownConverter = new KindleNotebookMarkdownConverter(markdownBuilder);
    var notebookMarkdown = markdownConverter.Convert(kindleNotebook);
 
    var fileName = Path.GetFileName(path);
    Console.WriteLine($"{fileName} converted to markdown.");

    IStorage storage = new FileSystemStorage();
    var notebookPath = Path.ChangeExtension(outputPath ?? path, "md");
    storage.Save(notebookPath, notebookMarkdown);

    Console.WriteLine($"{fileName} saved as {notebookPath}.");
});
