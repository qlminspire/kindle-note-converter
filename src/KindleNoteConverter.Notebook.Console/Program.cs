using Cocona;

using Microsoft.Extensions.DependencyInjection;

using KindleNoteConverter.Notebook.Services.Converters;
using KindleNoteConverter.Notebook.Services.Storage;
using KindleNoteConverter.Notebook.Services.Parsers;
using KindleNoteConverter.Markdown.Builders;

using NotebookModel = KindleNoteConverter.Notebook.Models.Notebook;

var builder = CoconaApp.CreateBuilder();

builder.Services.AddSingleton<IStorage, FileSystemStorage>();
builder.Services.AddSingleton<INotebookParser, KindleNotebookParser>();
builder.Services.AddSingleton<IMarkdownBuilder, MarkdownBuilder>();
builder.Services.AddSingleton<IMarkdownConverter<NotebookModel>, KindleNotebookMarkdownConverter>();
builder.Services.AddSingleton<IMarkdownConverterFlow, MarkdownConverterFlow>();

var app = builder.Build();

app.AddCommand((IMarkdownConverterFlow markdownConverterFlow,
   [Option('p', Description = "Path to kindle notebook in HTML format")] string path,
   [Option('o', Description = "Path to result file in markdown format")] string outputPath) =>
{
    markdownConverterFlow.Convert(path, outputPath);
});

app.Run();
