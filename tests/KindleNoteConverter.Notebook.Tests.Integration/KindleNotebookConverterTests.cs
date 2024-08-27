using KindleNoteConverter.Markdown.Builders;
using KindleNoteConverter.Notebook.Services.Converters;
using KindleNoteConverter.Notebook.Services.Markdown;
using KindleNoteConverter.Notebook.Services.Parsers;
using KindleNoteConverter.Notebook.Services.Storage;

namespace KindleNoteConverter.Notebook.Tests.Integration;

public sealed class KindleNotebookConverterTests
{
    private static string ProjectDirectory => Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
    
    private static string SourcesPath => Path.Combine(ProjectDirectory, "sources");
    private static string TargetsPath => Path.Combine(ProjectDirectory, "targets");

    public KindleNotebookConverterTests()
    {
        Cleanup();
    }

    [Fact]
    public async Task Should_Generate_Valid_Markdown_For_MultiNote_Notebook()
    {
        // Arrange
        const string expectedMarkdown = """
                                        #### 1
                                        Location 30
                                        > My mama is a real fine person. Everbody says that. My daddy, he got kilt just after I's born, so I never known him. He worked down to the docks as a longshoreman an one day a crane was takin a big net load of bananas off one of them United Fruit Company boats an somethin broke an the bananas fell down on my daddy an squashed him flat as a pancake.
                                        Location 36
                                        > Then she'd set there an talk to me, jus talk on an on bout nothin in particular, like a person'll talk to a dog or cat, but I got used to it an liked it cause her voice made me feel real safe an nice.
                                        #### 3
                                        Location 278
                                        > have about as much brains as a eggplant.
                                        Location 283
                                        > scare me haf to death!
                                        Location 289
                                        > My tongue hangin down like it was a necktie or somethin,
                                        #### 6
                                        Location 713
                                        > The weeks go by so slow I almost think time passin backwards.
                                        #### 7
                                        Location 790
                                        > I think that settin there talkin to Dan was a thing that had a great impression on my life. I know that bein a idiot an all, I ain't sposed to have no philosophy of my own, but maybe it's just because nobody never took the time to talk to me bout it. It were Dan's philosophy that everythin that happen to us, or for that matter, to anythin anywhere, is controlled by natural laws that govern the universe. His views on the subject was extremely complicated, but the gist of what he say begun to change my whole outlook on things.
                                        #### 11
                                        Location 1260
                                        > day in an day out.
                                        Location 1275
                                        > "Keep away from me, you shithead! You men is all alike, jus like dogs or somethin— you got no respect for anybody!"
                                        #### 15
                                        Location 1669
                                        > stuck out in the middle of nowhere.
                                        #### 18
                                        Location 1939
                                        > She say life has not exactly been a bowl of cherries for her either durin the past few years.
                                        #### 25
                                        Location 2776
                                        > lay low
                                        #### 26
                                        Location 2915
                                        > sometimes at night, when I look up at the stars, an see the whole sky jus laid out there, don't you think I ain't rememberin it all. I still got dreams like anybody else, an ever so often, I am thinkin about how things might of been. An then, all of a sudden, I'm forty, fifty, sixty years ole, you know? Well, so what? I may be a idiot, but most of the time, anyway, I tried to do the right thing— an dreams is jus dreams, ain't they?

                                        """;
        
        const string fileName = "Forrest Gump";

        var sut = CreateKindleNotebookConverter();
        
        var path = GetSourceFilePath(fileName);
        var outputPath = GetOutputFilePath(fileName);
        
        // Act
        await sut.Convert(path, outputPath);

        // Assert

        var actualMarkdown = await File.ReadAllTextAsync(outputPath);
        
        Assert.Equal(expectedMarkdown, actualMarkdown);
    }

    [Fact]
    public async Task Should_Generate_Valid_Markdown_For_SingleNote_Notebook()
    {
        const string expectedMarkdown = """
                                        #### 81. Белая невеста для сына проводника спальных вагонов
                                        Location 2697
                                        > Я поднял голову. Первый помер пластинки уже кончился, игла медленно прокладывала себе дорожку к следующему номеру. Как я прочел на обложке, следующий назывался «Блюз „Дракон“». Мид Люкс Льюис сыграл первые такты соло – и тут вступила Анджела Хониккер. Глаза у нее закрылись. Я был потрясен. Она играла блестяще. Она импровизировала под музыку сына проводника; она переходила от ласковой лирики и хриплой страсти к звенящим вскрикам испуганного ребенка, к бреду наркомана. Ее переходы, глиссандо, вели из рая в ад через все, что лежит между ними. Так играть могла только шизофреничка или одержимая. Волосы у меня встали дыбом, как будто Анджела каталась по полу с пеной у рта и бегло болтала по-древневавилонски. Когда музыка оборвалась, я закричал Джулиану Каслу, тоже пронзенному этими звуками: – Господи, вот вам жизнь! Да разве ее хоть чуточку поймешь? – А вы и не старайтесь, – сказал Касл. – Просто сделайте вид, что вы все понимаете.
                                        
                                        """;
        
        const string fileName = "Колыбель для кошки";

        var sut = CreateKindleNotebookConverter();
        
        var path = GetSourceFilePath(fileName);
        var outputPath = GetOutputFilePath(fileName);
        
        // Act
        await sut.Convert(path, outputPath);

        // Assert

        var actualMarkdown = await File.ReadAllTextAsync(outputPath);
        
        Assert.Equal(expectedMarkdown, actualMarkdown);
    }

    [Theory]
    [InlineData("Forrest Gump")]
    [InlineData("Атлант расправил плечи")]
    [InlineData("Колыбель для кошки")]
    [InlineData("Мужчина без женщины")]
    public async Task Should_Generate_Markdown_File_For_Specified_Notebook(string fileName)
    {
        await ConvertNotebook(fileName);
    }
    
    private static Task ConvertNotebook(string fileName)
    {
        var sut = CreateKindleNotebookConverter();
        
        var path = GetSourceFilePath(fileName);
        var outputPath = GetOutputFilePath(fileName);
        
        return sut.Convert(path, outputPath);
    }
    
    private static string GetSourceFilePath(string fileName) =>
        Path.Combine(SourcesPath, $"{fileName}.html");

    private static string GetOutputFilePath(string fileName) =>
        Path.Combine(TargetsPath, $"{fileName}.md");

    private static KindleNotebookConverter CreateKindleNotebookConverter()
    {
        var notebookParser = new KindleNotebookHtmlParser();
        
        var markdownBuilder = new MarkdownBuilder();
        var markdownGenerator = new KindleNotebookMarkdownGenerator(markdownBuilder);

        var fileSystemStorage = new FileSystemStorage();

        return new KindleNotebookConverter(notebookParser, markdownGenerator, fileSystemStorage);
    }
    
    private static void Cleanup()
    {
        if(Directory.Exists(TargetsPath))
            Directory.Delete(TargetsPath, true);
    }
}