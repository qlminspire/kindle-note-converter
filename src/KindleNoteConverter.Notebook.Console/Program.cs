using Cocona;

using Microsoft.Extensions.DependencyInjection;

using KindleNoteConverter.Notebook.Services.Converters;
using KindleNoteConverter.Notebook.Services.Storage;
using KindleNoteConverter.Notebook.Services.Parsers;
using KindleNoteConverter.Markdown.Builders;
using KindleNoteConverter.Notebook.Services.Markdown;
using Microsoft.Extensions.Logging;

using NotebookModel = KindleNoteConverter.Notebook.Models.Notebook;

var builder = CoconaApp.CreateBuilder();

builder.Logging.AddConsole();

builder.Services.AddSingleton<IStorage, FileSystemStorage>();
builder.Services.AddSingleton<INotebookParser, KindleNotebookHtmlParser>();
builder.Services.AddSingleton<IMarkdownBuilder, MarkdownBuilder>();
builder.Services.AddSingleton<IMarkdownGenerator<NotebookModel>, KindleNotebookMarkdownGenerator>();
builder.Services.AddSingleton<IKindleNotebookConverter, KindleNotebookConverter>();

var app = builder.Build();

app.AddCommand((IKindleNotebookConverter markdownConverterFlow,
   [Option('p', Description = "Path to kindle notebook in HTML format")] string path,
   [Option('o', Description = "Path to result file in markdown format")] string? outputPath) =>
{
    markdownConverterFlow.Convert(path, outputPath).GetAwaiter().GetResult();
});

app.Run();
