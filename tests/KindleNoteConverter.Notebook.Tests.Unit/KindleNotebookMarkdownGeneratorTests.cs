using FluentAssertions;
using KindleNoteConverter.Markdown.Builders;
using KindleNoteConverter.Notebook.Models;
using KindleNoteConverter.Notebook.Services.Markdown;

namespace KindleNoteConverter.Notebook.Tests.Unit;

public sealed class KindleNotebookMarkdownGeneratorTests
{
    [Fact]
    public void Should_GenerateMarkdown_From_NotebookModel()
    {
        // Arrange
        const string expected = """
                         #### 11
                         Location 1260
                         > day in an day out.
                         #### 15
                         Location 1669
                         > stuck out in the middle of nowhere.
                         #### 18
                         Location 1939
                         > She say life has not exactly been a bowl of cherries for her either durin the past few years.
                         Location 2776
                         > lay low
                         #### 19
                         
                         """;
        
        var notebook = Models.Notebook.Create(string.Empty, string.Empty, new List<Chapter>
        {
            Chapter.Create("11", new List<Note>
            {
                Note.Create("Location 1260", "day in an day out.").Value,
            }).Value,
            Chapter.Create("15", new List<Note>()
            {
                Note.Create("Location 1669", "stuck out in the middle of nowhere.").Value,
            }).Value,
            Chapter.Create("18", new List<Note>()
            {
                Note.Create("Location 1939", "She say life has not exactly been a bowl of cherries for her either durin the past few years.").Value,
                Note.Create("Location 2776", "lay low").Value,
            }).Value,
            Chapter.Create("19").Value
        });
        
        var markdownBuilder = new MarkdownBuilder();
        var sut = new KindleNotebookMarkdownGenerator(markdownBuilder);

        // Act
        var result = sut.Generate(notebook);

        // Assert
        result.Should().Be(expected);
    }
}