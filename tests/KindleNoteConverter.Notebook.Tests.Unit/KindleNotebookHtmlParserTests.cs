using FluentAssertions;
using KindleNoteConverter.Notebook.Services.Parsers;

namespace KindleNoteConverter.Notebook.Tests.Unit;

public sealed class KindleNotebookHtmlParserTests
{
    [Fact]
    public void Should_Return_EmptyNotebook_When_Html_Is_EmptyString()
    {
        // Arrange
        var html = string.Empty;
        var sut = new KindleNotebookHtmlParser();

        // Act
        var result = sut.Parse(html);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().BeNull();
        result.Author.Should().BeNull();
        result.Chapters.Should().BeEmpty();
        result.HasNoChapters.Should().BeTrue();
    }
    
    [Fact]
    public void Should_Return_NotebookWithoutChapters_When_Html_HasNoNotes()
    {
        // Arrange
        const string html = """
                            <?xml version="1.0" encoding="UTF-8"?>
                            <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "XHTML1-s.dtd" >
                            <html xmlns="http://www.w3.org/TR/1999/REC-html-in-xml" xml:lang="en" lang="en">
                            <body>
                            <div class='bodyContainer'>
                            <h1><div class='notebookFor'>Notes and highlights for</div><div class='bookTitle'>Атлант расправил плечи - Айн Рэнд
                            </div><div class='authors'>
                            Айн Рэнд
                            </div></h1><hr/>
                            <div class='noteText'>1</div><hr/>
                            </div> 
                            </body> 
                            </html> 

                            """;
        var sut = new KindleNotebookHtmlParser();

        // Act
        var result = sut.Parse(html);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("Атлант расправил плечи - Айн Рэнд");
        result.Author.Should().Be("Айн Рэнд");
        result.Chapters.Should().BeEmpty();
        result.HasNoChapters.Should().BeTrue();
    }
    
    [Fact]
    public void Should_Return_NotebookWithSingleChapterAndSingleNote_When_Html_Has_SingleChapterAndSingleNote()
    {
        // Arrange
        const string html = """
                            <?xml version="1.0" encoding="UTF-8"?>
                            <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "XHTML1-s.dtd" >
                            <html xmlns="http://www.w3.org/TR/1999/REC-html-in-xml" xml:lang="en" lang="en">
                            <body>
                            <div class='bodyContainer'>
                            <h1><div class='notebookFor'>Notes and highlights for</div><div class='bookTitle'>Черный обелиск
                            </div><div class='authors'>
                            Эрих Мария Ремарк
                            </div></h1><hr/>
                            
                            <h2 class='sectionHeading'>I</h2><h3 class='noteHeading'>Highlight (<span class='highlight_pink'>pink</span>) - Location 101</div><div class='noteText'>Он извлекает из кармана чудесно осмугленный дымом золотисто-коричневый мундштук из морской пенки, вставляет в него мою бразильскую сигару и продолжает ее курить.</h3>
                            </div> 
                            </body> 
                            </html> 

                            """;
        var sut = new KindleNotebookHtmlParser();

        // Act
        var result = sut.Parse(html);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("Черный обелиск");
        result.Author.Should().Be("Эрих Мария Ремарк");
        result.Chapters.Count.Should().Be(1);
        result.HasNoChapters.Should().BeFalse();

        var chapter = result.Chapters.First();
        chapter.Title.Should().Be("I");
        chapter.Notes.Count.Should().Be(1);

        var note = chapter.Notes.First();
        note.Location.Should().Be("Location 101");
        note.Content.Should().Be("Он извлекает из кармана чудесно осмугленный дымом золотисто-коричневый мундштук из морской пенки, вставляет в него мою бразильскую сигару и продолжает ее курить.");
    }
}